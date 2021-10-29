using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClignoteToggle : MonoBehaviour
{
    private Toggle toggle;

    private bool isNotCLicked = true;

    [SerializeField]
    private GameObject image;

    private void Awake()
    {
        image.SetActive(true);
    }

    private void Start()
    {
        Invoke(nameof(Desactive), 1);
    }

    private void Desactive()
    {
        image.SetActive(false);
        if(isNotCLicked)
        {
            Invoke(nameof(Active), 1);
        }
    }

    private void Active()
    {
        image.SetActive(true);
        Invoke(nameof(Desactive), 1);
    }

    public void Click()
    {
        isNotCLicked = false;
    }
}
