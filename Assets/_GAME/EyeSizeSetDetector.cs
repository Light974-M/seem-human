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

    private void OnMouseOver()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (eyePupil.localScale.x <= 1.15f)
                eyePupil.localScale += new Vector3(0.01f, 0.01f, 0);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if(eyePupil.localScale.x >= 0.02f)
                eyePupil.localScale -= new Vector3(0.01f, 0.01f, 0);
        }

        float Vd = Mathf.Round(eyePupil.localScale.x * 100) / 10;
        diameter.text = $"{Vd} Vd";
    }
}
