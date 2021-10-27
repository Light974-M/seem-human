using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Pack contenant la fenètre des paramètres")]
    private GameObject settingsWindow;

    [SerializeField]
    [Tooltip("Nom de la scène à charger quand on appuie sur play")]
    private string levelToLoad = "Level1";

    private void Awake()
    {
        if(settingsWindow != null)
        {
            settingsWindow.SetActive(false);
        }
    }

    public void ButtonPlay()
    {
        SceneManager.LoadScene(levelToLoad);
    }

    public void ButtonSettings()
    {
        gameObject.SetActive(false);
        settingsWindow.SetActive(true);
    }

    public void ButtonQuit()
    {
        Application.Quit();
        Debug.Log("Application fermée");
    }
}