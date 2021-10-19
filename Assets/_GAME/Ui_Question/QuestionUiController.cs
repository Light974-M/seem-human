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
