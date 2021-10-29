using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewQuestionAsset", menuName = "Question Asset")]
public class QuestionAsset : ScriptableObject
{
    [SerializeField, Tooltip("the question")]
    private string _question = "";       public string Question => _question;

    [SerializeField, Tooltip("the answer")]
    private string _answer = "";         public string Answer => _answer;
    private int _heart = 0;              public int Heart => _heart;
    private float _pupil = 0;              public float Pupil => _pupil;
    private int _amplitude = 0;          public int Amplitude => _amplitude;
    private int _longueur = 0;           public int Longueur => _longueur;

    [SerializeField, Tooltip("feelingneeded")]
    private Emotion feeling = Emotion.Vigilant;     public string Feeling => feeling.ToString();

    public enum Emotion
    {
        Vigilant,
        Disgust,
        Rage,
        Terror,
        Sadness,
        Surprise,
        Ecstacy,
        Admiration,
        None
    }

    private void Awake()
    {
        switch(feeling)
        {
            case Emotion.Vigilant:
                _heart = 160; _pupil = 0.2f; _amplitude = 80; _longueur = 100;
                break;
            case Emotion.Disgust:
                _heart = 60; _pupil = 0.5f; _amplitude = 70; _longueur = 80;
                break;
            case Emotion.Rage:
                _heart = 150; _pupil = 0.4f; _amplitude = 50; _longueur = 30;
                break;
            case Emotion.Terror:
                _heart = 180; _pupil = 0.1f; _amplitude = 30; _longueur = 40;
                break;
            case Emotion.Sadness:
                _heart = 50; _pupil = 0.8f; _amplitude = 80; _longueur = 60;
                break;
            case Emotion.Surprise:
                _heart = 100; _pupil = 0.9f; _amplitude = 80; _longueur = 60;
                break;
            case Emotion.Ecstacy:
                _heart = 80; _pupil = 0.9f; _amplitude = 40; _longueur = 50;
                break;
            case Emotion.Admiration:
                _heart = 60; _pupil = 0.5f; _amplitude = 40; _longueur = 60;
                break;
        }
    }
}