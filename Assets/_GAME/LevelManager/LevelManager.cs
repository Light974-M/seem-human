using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    [Range(0, 10)]
    private float _questionTimerLimit = 1f;

    /*[SerializeField]
    [Tooltip("timer slider")]
    private ;*/

    [Header("")]
    [Header("LEVEL________________________________________________________________________________________________________________")]

    [SerializeField]
    [Tooltip("Questions number before game end")]
    [Range(0, 100)]
    private int _questionNumber = 30;

    [SerializeField]
    [Tooltip("Niveau de tolérence entre les valeurs actuelles et celles demandées")]
    private int _tolerance = 10;

    [Header("")]
    [Header("QUESTIONS________________________________________________________________________________________________________________")]

    [SerializeField]
    [Tooltip("timer lower point")]
    private List<QuestionAsset> _questionList;

    private QuestionAsset _currentQuestion;

    //EVENTS_____________________________________________________________________________________________________________________________
    public delegate void QuestionAssetDelegate(QuestionAsset asset);
    public event QuestionAssetDelegate questionChange;

    private void Start()
    {
        _hearthrate = 80;
        _pupilDilatation = 1;
        _amplitude = 5;
        _longueur = 5;
    }

    void Update()
    {
        if(_timer <= 0)
        {
            UpdateGame();
            Debug.Log("Trop tard, t'es sus");
        }

        _timer -= Time.deltaTime;
    }

    private void TestSuspicion()
    {
        if(Mathf.Abs(_currentQuestion.Heart - _hearthrate) < _tolerance)
        {
            Debug.Log("BPM trop différent, t'es sus");
        }
        if (Mathf.Abs(_currentQuestion.Pupil - _pupilDilatation) < _tolerance)
        {
            Debug.Log("Pupille trop différente, t'es sus");
        }
        if (Mathf.Abs(_currentQuestion.Amplitude - _amplitude) < _tolerance)
        {
            Debug.Log("Souffle trop différent, t'es sus");
        }
        if (Mathf.Abs(_currentQuestion.Longueur - _longueur) < _tolerance)
        {
            Debug.Log("Souffle trop différent, t'es sus");
        }
    }

    private void UpdateGame()
    {
        TestSuspicion();
        UpdateQuestion();
        UpdateTimer();
        _questionNumber -= 1;

        if (_questionNumber == 0)
        {
            SceneManager.LoadScene("WinScreen");
        }
    }

    private void UpdateQuestion()
    {
        int _tmp = Random.Range(0, _questionList.Count);
        _currentQuestion = _questionList[_tmp];

        if (_questionList.Count > 1)
        {
            _questionList.RemoveAt(_tmp);
        }
        
        if(questionChange != null)
        {
            questionChange(_currentQuestion);
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
    }
}