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

    [SerializeField]
    [Tooltip("Actual hearthrate value")]
    private float _hearthrate;
    public float HearthRate
    { get { return _hearthrate; }   set { _hearthrate = value; } }

    [SerializeField]
    [Tooltip("Actual pupil dilatation")]
    private float _pupilDilatation;
    public float PupilDilatation
    { get { return _pupilDilatation; } set { _pupilDilatation = value; } }

    /*[SerializeField]
    [Tooltip("Actual breath value 1")]
    private float _amplitude; public float BreathValue1 => _amplitude;

    [SerializeField]
    [Tooltip("Actual breath value 2")]
    private float _longueur; public float BreathValue2 => _longueur;*/

    [SerializeField]
    [Tooltip("GameObject du dessin du souffle en cours")]
    private Transform breathMesh;

    [SerializeField]
    [Tooltip("GameObject du dessin du souffle demande")]
    private Transform breathAskedMesh;

    [Header("")]
    [Header("TIME________________________________________________________________________________________________________________")]

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

    [Header("")]
    [Header("LEVEL________________________________________________________________________________________________________________")]

    [SerializeField]
    [Tooltip("Questions number before game end")]
    [Range(0, 100)]
    private int _questionNumber = 30;

    [SerializeField]
    [Tooltip("Niveau de tolerence entre les valeurs actuelles et celles demandees")]
    private float _tolerance = 1;

    [Header("")]
    [Header("QUESTIONS________________________________________________________________________________________________________________")]

    [SerializeField]
    [Tooltip("timer lower point")]
    private List<QuestionAsset> _questionList;

    private QuestionAsset _currentQuestion;    public QuestionAsset CurrentQuestion => _currentQuestion;

    [SerializeField]
    [Tooltip("Nombre d'erreur max")]
    private int _errorMax = 10;

    [SerializeField]
    private int _error = 0;

    [SerializeField]
    [Tooltip("Suspicion Slider script")]
    private SliderController _suspicionSlider;

    //EVENTS_____________________________________________________________________________________________________________________________
    public delegate void QuestionAssetDelegate(QuestionAsset asset);
    public event QuestionAssetDelegate questionChange;
    public event QuestionAssetDelegate newQuestionBegin;
    public event QuestionAssetDelegate answer;

    private void Start()
    {
        _hearthrate = 60;
        _pupilDilatation = 1;
        //_amplitude = 5;
        //_longueur = 5;

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
            SetSuspition(3);
            Respond();
        }

        if(_timerIsDecrease)
        {
            _timer -= Time.deltaTime;
            _timerSlider.SetValue(_timer);
        }
    }

    public void Respond()
    {
        if(_timerIsDecrease)
        {
            // On stoppe le timer
            _timerIsDecrease = false;

            // On reset le timer
            UpdateTimer();

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
        if (_currentQuestion.Heart != _hearthrate)
        {
            SetSuspition(1);
            Debug.Log("Coeur sus");
        }
        if (_currentQuestion.Pupil != _pupilDilatation)
        {
            SetSuspition(1);
            Debug.Log("Pupille sus");
        }
        if(Mathf.Abs(breathMesh.localScale.x - breathAskedMesh.localScale.x) > _tolerance)
        {
            SetSuspition(1);
            Debug.Log("Souffle sus");
        }
        else if (Mathf.Abs(breathMesh.localScale.y - breathAskedMesh.localScale.y) > _tolerance)
        {
            SetSuspition(1);
            Debug.Log("Souffle sus");
        }
        else
        {
            Debug.Log("Souffle ok");
        }
    }

    private void ChargementNewQuestion()
    {
        // Apres le temps de latence pour la r�ponse, on teste si win
        _questionNumber -= 1;
        if (_questionNumber == 0)
        {
            SceneManager.LoadScene("WinScreen");
        }
        // Sinon on charge une nouvelle question
        UpdateQuestion();
        // On affiche la question
        questionChange(_currentQuestion);

        // Puis on affiche apr�s 3s les donn�es et on lance le timer
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
        if (_error >= _errorMax)
        {
            SceneManager.LoadScene("LoseScreen");
            Debug.Log("Loose");
        }
    }
}
