using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionUiController : MonoBehaviour
{
    [SerializeField]
    [Tooltip("place where question will be displayed")]
    private Text textZone;

    [SerializeField]
    [Tooltip("LevelManager(waou)")]
    private LevelManager levelManager;

    [SerializeField]
    [Tooltip("Image ou mettre le sprite du Blade Runner")]
    private Image cameraImage;

    [SerializeField]
    [Tooltip("Liste des sprites du Blade Runner")]
    private List<Sprite> spriteList;

    [SerializeField]
    private float _idleTimerChange;
    private float _idleTimer = 0f;
    private int _indexSprite = 0;

    private void Awake()
    {
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
        levelManager.questionChange += SetText;
    }

    private void OnDisable()
    {
        levelManager.questionChange -= SetText;
    }

    private void SetText(QuestionAsset questionAsset)
    {
        textZone.text = $"{questionAsset.Question}";
    }
}