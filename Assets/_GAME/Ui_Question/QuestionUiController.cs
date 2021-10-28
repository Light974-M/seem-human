using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionUiController : MonoBehaviour
{
    [Header("")]
    [Header("QUESTION WRITE____________________________________________________________________________________________________________")]

    [SerializeField]
    [Tooltip("place where question will be displayed")]
    private Text textZone;
    public Text TextZone
    {
        get { return textZone; }
        set { textZone = value; }
    }

    private LevelManager levelManager;
    private WriteBlueScreen textDefil;

    [Header("")]
    [Header("BLADE RUNNER IMG____________________________________________________________________________________________________________")]

    [SerializeField]
    [Tooltip("place where the blade runner sprite will be displayed")]
    private Image cameraImage;

    [SerializeField]
    [Tooltip("blade runner sprite list")]
    private List<Sprite> spriteList;

    [SerializeField]
    [Tooltip("sprite changing rate")]
    private float _idleTimerChange;

    private float _idleTimer = 0f;
    private int _indexSprite = 0;

    private void Awake()
    {
        textDefil = FindObjectOfType<WriteBlueScreen>();
        levelManager = FindObjectOfType<LevelManager>();
        cameraImage.sprite = spriteList[0];
    }

    private void Update()
    {
        _idleTimer += Time.deltaTime;
        if (_idleTimer > _idleTimerChange)
        {
            switch (_indexSprite)
            {
                case 0:
                    cameraImage.sprite = spriteList[1];
                    _indexSprite = 1;
                    break;
                case 1:
                    cameraImage.sprite = spriteList[2];
                    _indexSprite = 2;
                    break;
                case 2:
                    cameraImage.sprite = spriteList[3];
                    _indexSprite = 3;
                    break;
                case 3:
                    cameraImage.sprite = spriteList[0];
                    _indexSprite = 0;
                    break;
            }
            _idleTimer = 0;
        }
    }

    private void OnEnable()
    {
        levelManager.questionChange += textDefil.SetText;
        levelManager.answer += textDefil.SetAnswer;
    }

    private void OnDisable()
    {
        levelManager.questionChange -= textDefil.SetText;
        levelManager.answer -= textDefil.SetAnswer;
    }
}