using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValuesQuestionController : MonoBehaviour
{
    [Header("")]
    [Header("QUESTION VALUES WRITE____________________________________________________________________________________________________________")]

    [SerializeField]
    [Tooltip("place where question will be displayed")]
    private Text textZone;

    [SerializeField]
    [Tooltip("LevelManager(waou)")]
    private LevelManager levelManager;

    private void OnEnable()
    {
        levelManager.newQuestionBegin += SetValues;
        levelManager.answer += SetValues2;
    }

    private void OnDisable()
    {
        levelManager.newQuestionBegin -= SetValues;
        levelManager.answer -= SetValues2;
    }

    private void SetValues(QuestionAsset questionAsset)
    {
        textZone.text = $"HUMAN INFORMATIONS :\n\n\n\n -Heart rate : {questionAsset.Heart} \n\n -Pupil : {questionAsset.Pupil} \n\n -Amplitude : {questionAsset.Amplitude} \n\n -Length : {questionAsset.Longueur}";
    }

    private void SetValues2(QuestionAsset questionAsset)
    {
        textZone.text = $"{questionAsset.Answer}";
    }
}
