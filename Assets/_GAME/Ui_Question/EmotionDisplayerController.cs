using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmotionDisplayerController : MonoBehaviour
{
    [SerializeField]
    [Tooltip("place where question will be displayed")]
    private Text textZone;

    private LevelManager levelManager;

    private void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    private void OnEnable()
    {
        levelManager.newQuestionBegin += SetValues;
    }

    private void OnDisable()
    {
        levelManager.newQuestionBegin -= SetValues;
    }

    private void SetValues(QuestionAsset asset)
    {
        textZone.text = $"{asset.Answer}";
    }
}