using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseManager : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Nom de la scène à charger quand on appuie sur play")]
    private string levelToLoad = "MainMenu";

    private AudioManager audioGet;

    private void Awake()
    {
        audioGet = FindObjectOfType<AudioManager>();
        audioGet.AudioSource.outputAudioMixerGroup = audioGet.AudioMixerArray[2];
        audioGet.AudioSource.PlayOneShot(audioGet.AudioClipArray[0]);
    }

    public void ButtonQuit()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
