using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

// Penser � chaque fois que la pr�fab est utilis�e r�assigner l'audioManager de la sc�ne en question
// et cr�er/assigner la fonction pour quitter les param�tres au bouton concern�

public class SettingsManager : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Gameobject contenant la fen�tre du menu principal")]
    private GameObject mainWindow;

    public Dropdown resolutionsDropdown;

    public AudioMixer audioMixer;
    [Header("Slider son :")]
    public Slider generalSoundSlider;
    public Slider musicSlider;
    public Slider soundSlider;

    public void Start()
    {
        /*audioMixer.GetFloat("General", out float generalValueForSlider);
        generalSoundSlider.value = generalValueForSlider;

        audioMixer.GetFloat("Music", out float musicValueForSlider);
        musicSlider.value = musicValueForSlider;

        audioMixer.GetFloat("Sound", out float soundValueForSlider);
        soundSlider.value = soundValueForSlider;*/

        Screen.fullScreen = true;
    }

    public void SetResolution(int resolutionIndex)
    {
        switch (resolutionIndex)
        {
            case 0:
                Screen.SetResolution(1920, 1080, Screen.fullScreen);
                break;
            case 1:
                Screen.SetResolution(1680, 1050, Screen.fullScreen);
                break;
            case 2:
                Screen.SetResolution(1280, 720, Screen.fullScreen);
                break;
            case 3:
                Screen.SetResolution(720, 480, Screen.fullScreen);
                break;
            default:
                break;
        }
    }

    public void FullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void SetGeneralSound(float volume)
    {
        if (volume <= -30)
        {
            volume = -80;
        }
        audioMixer.SetFloat("Music", volume);
    }

    public void SetMusic(float volume)
    {
        if (volume <= -30)
        {
            volume = -80;
        }
        audioMixer.SetFloat("Music", volume);
    }

    public void SetBruitage(float volume)
    {
        if (volume <= -30)
        {
            volume = -80;
        }
        audioMixer.SetFloat("Sound", volume);
    }

    public void ButtonReturn()
    {
        gameObject.SetActive(false);
        mainWindow.SetActive(true);
    }
}