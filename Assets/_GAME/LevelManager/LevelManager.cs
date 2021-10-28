using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [Header("")]
    [Header("STATUS________________________________________________________________________________________________________________")]
    #region StatusVariables
    [SerializeField]
    [Tooltip("Actual hearthrate value")]
    private float _hearthrate;
    public float HearthRate
    { get { return _hearthrate; } set { _hearthrate = value; } }

    [SerializeField]
    [Tooltip("Actual pupil dilatation")]
    private float _pupilDilatation;
    public float PupilDilatation
    { get { return _pupilDilatation; } set { _pupilDilatation = value; } }

    [SerializeField]
    [Tooltip("GameObject du dessin du souffle en cours")]
    private Transform breathMesh;

    [SerializeField]
    [Tooltip("GameObject du dessin du souffle demande")]
    private Transform breathAskedMesh;

    [SerializeField]
    [Tooltip("game over font sprite")]
    private Image gameOverSprite; 
    #endregion

    [Header("")]
    [Header("TIME________________________________________________________________________________________________________________")]
    #region TimeVariables
    [SerializeField]
    [Tooltip("timer that will determine question deadline")]
    [Range(0, 30)]
    private float _questionTimer = 20;

    [SerializeField]
    [Tooltip("timer that will decrease trough time")]
    private float _timer = 0;

    [SerializeField]
    [Tooltip("timer speed to decrease")]
    [Range(0, 1)]
    private float _questionTimerDecreaseSpeed = 0.91f;

    [SerializeField]
    [Tooltip("timer lower point")]
    [Range(0, 20)]
    private float _questionTimerLimit = 1f;

    [SerializeField]
    [Tooltip("timer slider")]
    private SliderController _timerSlider;

    [SerializeField]
    [Tooltip("if timer decrease")]
    private bool _timerIsDecrease = false;

    [SerializeField, Tooltip("GameObject AlertPoint")]
    private GameObject alertPoint;

    [SerializeField, Tooltip("Sprite rouge")]
    private GameObject spriteRouge; 
    #endregion

    [Header("")]
    [Header("LEVEL________________________________________________________________________________________________________________")]
    #region LevelVariables
    [SerializeField, Tooltip("Questions number before game end"), Range(0, 100)]
    private int _questionNumber = 30;

    [SerializeField, Tooltip("Niveau de tolerence entre les valeurs actuelles et celles demandees")]
    private float _breathTolerance = 1;

    [SerializeField, Tooltip("Niveau de tolérence des BPMs")]
    private int _hearthTolerance = 10;

    [SerializeField, Tooltip("Niveau de tolérence de la dilatation de la pupille")]
    private float _pupilTolerance = 1;

    [SerializeField, Tooltip("Liste des effets de bugs")]
    private List<GameObject> glitchList; 
    #endregion

    private AutoSider effectSlider;

    [Header("")]
    [Header("QUESTIONS________________________________________________________________________________________________________________")]

    #region QuestionVariables
    [SerializeField]
    [Tooltip("timer lower point")]
    private List<QuestionAsset> _questionList;
    private QuestionAsset _currentQuestion; public QuestionAsset CurrentQuestion => _currentQuestion;

    [SerializeField]
    [Tooltip("Nombre d'erreur max")]
    private int _errorMax = 10;

    [SerializeField]
    private int _error = 0;

    [SerializeField]
    [Tooltip("Suspicion Slider script")]
    private SliderController _suspicionSlider;
    #endregion

    private AudioManager audioGet;
    private AutoRotate[] disk;
    private HeartDraw heartDraw;
    private QuestionUiController questionUiController;

    [Header("OTHER__________________________________________________________________________________")]
    [Header("")]

    [SerializeField, Tooltip("heartBeatAnimation")]
    private Animator heartAnim;

    //EVENTS_____________________________________________________________________________________________________________________________
    #region EventVariables
    public delegate void QuestionAssetDelegate(QuestionAsset asset);
    public event QuestionAssetDelegate questionChange;
    public event QuestionAssetDelegate newQuestionBegin;
    public event QuestionAssetDelegate answer; 
    #endregion

    [SerializeField, Tooltip("Objet contenant toute la doc du jeu")]
    private GameObject doc;

    private void Awake()
    {
        heartDraw = FindObjectOfType<HeartDraw>();
        questionUiController = FindObjectOfType<QuestionUiController>();
        disk = FindObjectsOfType<AutoRotate>();
        effectSlider = FindObjectOfType<AutoSider>();
        audioGet = FindObjectOfType<AudioManager>();
        DesactiveGlitch();
        alertPoint.SetActive(false);
        spriteRouge.SetActive(false);
        doc.SetActive(false);
    }

    private void Start()
    {
        _hearthrate = 60;
        _pupilDilatation = 1;

        // On set le slider de suspicion
        _suspicionSlider.SetMaxValue(_errorMax);
        _suspicionSlider.SetValue(_error);

        // On charge la nouvelle question
        UpdateQuestion();

        // On initialise le timer
        UpdateTimer();
        // On lance l'event pour afficher la question a l'ecran
        questionChange(_currentQuestion);

        _timerSlider.SetMaxValue(_questionTimer);

        // On lance le timer apr�s 3s
        Invoke(nameof(LunchNewQuestion), 3f);
    }

    void Update()
    {
        if(_timer <= 0)
        {
            SetSuspition(2);
            Respond();
        }

        if(_timerIsDecrease)
        {
            _timer -= Time.deltaTime;
            _timerSlider.SetValue(_timer);

            if(_timer < _questionTimer/2)
            {
                alertPoint.SetActive(true);
                spriteRouge.SetActive(true);
            }
        }
    }

    public void Respond()
    {
        if(_timerIsDecrease)
        {
            alertPoint.SetActive(false);
            spriteRouge.SetActive(false);

            // On stoppe le timer
            _timerIsDecrease = false;

            // On reset le timer
            UpdateTimer();

            //play sound
            audioGet.AudioSource.outputAudioMixerGroup = audioGet.AudioMixerArray[2];
            audioGet.AudioSource.PlayOneShot(audioGet.AudioClipArray[1]);

            // Lance la verification des param�tres
            TestSuspicion();
            // Dit d'afficher la reponse en texte
            if (answer != null)
            {
                answer(_currentQuestion);
            }

            // Dans 3s on passe a la question suivante
            Invoke(nameof(ChargementNewQuestion), 3f);
        }
    }

    private void TestSuspicion()
    {
        int tmp = 0;

        if (Mathf.Abs(_currentQuestion.Heart - _hearthrate) > _hearthTolerance)
        {
            tmp += 1;
        }
        if (Mathf.Abs(_currentQuestion.Pupil - _pupilDilatation) > _pupilTolerance)
        {
            tmp +=1;
        }
        if(Mathf.Abs(breathMesh.localScale.x - breathAskedMesh.localScale.x) > _breathTolerance)
        {
            tmp +=1;
        }
        else if (Mathf.Abs(breathMesh.localScale.y - breathAskedMesh.localScale.y) > _breathTolerance)
        {
            tmp +=1;
        }
        SetSuspition(tmp);
    }

    private void ChargementNewQuestion()
    {
        // Apres le temps de latence pour la reponse, on teste si win
        _questionNumber -= 1;
        if (_questionNumber == 0)
        {
            SceneManager.LoadScene("WinScreen");
        }
        // Sinon on charge une nouvelle question
        UpdateQuestion();
        // On affiche la question
        questionChange(_currentQuestion);

        // Puis on affiche apres 3s les donnees et on lance le timer
        Invoke(nameof(LunchNewQuestion), 3f);
    }

    private void UpdateQuestion()
    {
        int _tmp = Random.Range(0, _questionList.Count);
        _currentQuestion = _questionList[_tmp];

        if (_questionList.Count > 1)
        {
            _questionList.RemoveAt(_tmp);
        }
    }

    private void LunchNewQuestion()
    { 
        // On lance le timer
        _timerIsDecrease = true;

        // On  affiche les donnees
        if (newQuestionBegin != null)
        {
            newQuestionBegin(_currentQuestion);
        }
    }

    private void UpdateTimer()
    {
        if(_questionTimer > _questionTimerLimit)
        {
            _questionTimer = _questionTimer * _questionTimerDecreaseSpeed;
        }
        
        if(_questionTimer <= _questionTimerLimit)
        {
            _questionTimer = _questionTimerLimit;
        }

        _timer = _questionTimer;
        _timerSlider.SetValue(_timer);
    }

    private void SetSuspition(int value)
    {
        _error += value;
        _suspicionSlider.SetValue(_error);

        for(int i=0; i < value; i++)
        {
            glitchList[Random.Range(0, glitchList.Count-1)].SetActive(true);
        }
        Invoke(nameof(DesactiveGlitch), 1.5f);

        if (_error >= _errorMax)
        {
            _error = 0;
            Debug.Log("Loose");
            audioGet.AudioSource.outputAudioMixerGroup = audioGet.AudioMixerArray[2];
            audioGet.AudioSource.PlayOneShot(audioGet.AudioClipArray[2]);
            StopAll();
            Invoke(nameof(GameOver), 5f);
        }

        if(value > 0)
        {
            audioGet.AudioSource.outputAudioMixerGroup = audioGet.AudioMixerArray[2];
            audioGet.AudioSource.PlayOneShot(audioGet.AudioClipArray[3]);
        }
    }

    private void GameOver()
    {
        SceneManager.LoadScene("LoseScreen");
    }

    private void DesactiveGlitch()
    {
        for(int i=0; i < glitchList.Count; i++)
        {
            glitchList[i].SetActive(false);
        }
    }

    private void StopAll()
    {
        foreach (AutoRotate disk in disk)
        {
            disk.enabled = false; 
        }
        gameOverSprite.enabled = true;
        effectSlider.enabled = false;
        heartDraw.enabled = false;
        questionUiController.enabled = false;
        heartAnim.enabled = false;
    }

    public void OpenDoc(bool isActive)
    {
        doc.SetActive(isActive);
    }
}
