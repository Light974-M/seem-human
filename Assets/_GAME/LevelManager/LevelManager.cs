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
    private int _hearthrate; public int Hearthrate => _hearthrate;

    [SerializeField]
    [Tooltip("Actual pupil dilatation")]
    private float _pupilDilatation; public float PupilDilatation => _pupilDilatation;

    [SerializeField]
    [Tooltip("Actual breath value 1")]
    private float _amplitude; public float BreathValue1 => _amplitude;

    [SerializeField]
    [Tooltip("Actual breath value 2")]
    private float _longueur; public float BreathValue2 => _longueur;

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
    private TimeSliderController _timerSlider;

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
    [Tooltip("Niveau de tol�rence entre les valeurs actuelles et celles demand�es")]
    private int _tolerance = 0;

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

    //EVENTS_____________________________________________________________________________________________________________________________
    public delegate void QuestionAssetDelegate(QuestionAsset asset);
    public event QuestionAssetDelegate questionChange;
    public event QuestionAssetDelegate newQuestionBegin;
    public event QuestionAssetDelegate answer;

    private void Start()
    {
        _hearthrate = 80;
        _pupilDilatation = 1;
        _amplitude = 5;
        _longueur = 5;

        // On charge la nouvelle question
        UpdateQuestion();

        // On initialise le timer
        UpdateTimer();
        // On lance l'event pour afficher la question � l'�cran
        questionChange(_currentQuestion);

        _timerSlider.SetMaxTime(_questionTimer);

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
            _timerSlider.SetTime(_timer);
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

            // Lance la v�rification des param�tres
            TestSuspicion();
            // Dit d'afficher la r�ponse en texte
            if (answer != null)
            {
                answer(_currentQuestion);
            }

            // Dans 3s on passe � la question suivante
            Invoke(nameof(ChargementNewQuestion), 3f);
        }
    }

    private void TestSuspicion()
    {
        Debug.Log("Suspition");
        if (Mathf.Abs(_currentQuestion.Heart - _hearthrate) > _tolerance)
        {
            SetSuspition(1);
        }
        if (Mathf.Abs(_currentQuestion.Pupil - _pupilDilatation) > _tolerance)
        {
            SetSuspition(1);
        }
        if (Mathf.Abs(_currentQuestion.Amplitude - _amplitude) > _tolerance)
        {
            SetSuspition(1);
        }
        else if (Mathf.Abs(_currentQuestion.Longueur - _longueur) > _tolerance)
        {
            SetSuspition(1);
        }
    }

    private void ChargementNewQuestion()
    {
        // Apr�s le temps de latence pour la r�ponse, on teste si win
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

        // On  affiche les donn�es
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
        _timerSlider.SetTime(_timer);
    }

    private void SetSuspition(int value)
    {
        _error += value;
        if(_error >= _errorMax)
        {
            //SceneManager.LoadScene("LoseScreen");
            Debug.Log("Loose");
        }
    }
}
