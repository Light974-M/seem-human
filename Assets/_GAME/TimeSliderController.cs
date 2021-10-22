using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeSliderController : MonoBehaviour
{
    [SerializeField]
    private Slider slider;

    [SerializeField]
    private Gradient gradient;

    [SerializeField]
    private Image fill;

    public void SetMaxTime(float time)
    {
        slider.maxValue = time;
        slider.value = time;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetTime(float time)
    {
        slider.value = time;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
