using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField, Tooltip("list of audio sounds use by the game(master)")]
    private AudioClip[] audioClipArray;    public AudioClip[] AudioClipArray => audioClipArray;

    [SerializeField, Tooltip("audio source used")]
    private AudioSource audioSource;     public AudioSource AudioSource => audioSource;

    [SerializeField, Tooltip("audio mixer array used")]
    private AudioMixerGroup[] audioMixerArray;     public AudioMixerGroup[] AudioMixerArray => audioMixerArray;
}
