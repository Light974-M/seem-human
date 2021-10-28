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

    [SerializeField, Tooltip("Couleur quand le texte change")]
    private Color color = new Color(0.5f, 0, 0);

    private void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    private void OnEnable()
    {
        levelManager.newQuestionBegin += SetValues;
        levelManager.answer += WaitText;
    }

    private void OnDisable()
    {
        levelManager.newQuestionBegin -= SetValues;
        levelManager.answer -= WaitText;
    }

    private void WaitText(QuestionAsset asset)
    {
        textZone.text = $"No emotions required";
    }

    private void SetValues(QuestionAsset asset)
    {
        textZone.text = $"{asset.Feeling}";
        textZone.color = color;
        textZone.fontSize += 10;
        Invoke(nameof(ResetText), 1);
    }

    private void ResetText()
    {
        textZone.color = Color.black;
        textZone.fontSize -= 10;
    }
}