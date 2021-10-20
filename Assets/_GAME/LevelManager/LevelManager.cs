using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
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
    [Range(0,1)]
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

    [Header("")]
    [Header("QUESTIONS________________________________________________________________________________________________________________")]

    [SerializeField]
    [Tooltip("timer lower point")]
    private List<QuestionAsset> _questionList;

    private QuestionAsset _currentQuestion;

    //EVENTS_____________________________________________________________________________________________________________________________

    public delegate void QuestionAssetDelegate(QuestionAsset asset);

    public event QuestionAssetDelegate questionChange;


    void Update()
    {
        if(_timer <= 0)
        {
            UpdateQuestion();
            UpdateTimer();
            _questionNumber -= 1;

            if(_questionNumber == 0)
            {
                SceneManager.LoadScene("WinScreen");
            }
        }

        _timer -= Time.deltaTime;
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
