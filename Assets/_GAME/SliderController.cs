using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    private Slider slider;

    [SerializeField]
    [Tooltip("Gradient pour le dégradé du slider")]
    private Gradient gradient;

    [SerializeField]
    [Tooltip("Zone du slider remplis")]
    private Image fill;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    public void SetMaxValue(float value)
    {
        slider.maxValue = value;
        slider.value = value;

        if(fill != null && gradient != null)
        {
            fill.color = gradient.Evaluate(1f);
        }
    }

    public void SetValue(float value)
    {
        slider.value = value;

        if (fill != null && gradient != null)
        {
            fill.color = gradient.Evaluate(slider.normalizedValue);
        }
    }
}