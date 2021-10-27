using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoSider : MonoBehaviour
{
    private Slider slider;

    [SerializeField]
    private float speed = 1;

    private bool sens = true;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    void Update()
    {
        if(sens)
        {
            slider.value += 0.01f * speed;
            if(slider.value >= slider.maxValue)
            {
                sens = false;
            }
        }
        else
        {
            slider.value -= 0.01f * speed;
            if(slider.value <= slider.minValue)
            {
                sens = true;
            }
        }
    }
}