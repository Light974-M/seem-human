using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EyeSizeSetDetector : MonoBehaviour
{
    [SerializeField]
    [Tooltip("pupil that will be scaled")]
    private Transform eyePupil;

    [SerializeField]
    [Tooltip("text that will render diameter")]
    private Text diameter;

    [SerializeField, Tooltip("text that will render actual diameter")]
    private Text actualDiameter;

    private LevelManager levelManager;

    private void OnEnable()
    {
        levelManager.newQuestionBegin += SetValue;
    }

    private void OnDisable()
    {
        levelManager.newQuestionBegin -= SetValue;
    }

    private void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    private void SetValue(QuestionAsset currentQuestion)
    {
        actualDiameter.text = $"Asked : {currentQuestion.Pupil} Vd";
    }

    private void OnMouseOver()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (eyePupil.localScale.x < 1f)
                eyePupil.localScale += new Vector3(0.1f, 0.1f, 0);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if(eyePupil.localScale.x > 0.1f)
                eyePupil.localScale -= new Vector3(0.1f, 0.1f, 0);
        }

        float Vd = Mathf.Round(eyePupil.localScale.x * 100) / 100;
        diameter.text = $"{Vd} Vd";
        levelManager.PupilDilatation = Vd;
    }
}