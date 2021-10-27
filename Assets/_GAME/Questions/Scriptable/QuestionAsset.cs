using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewQuestionAsset", menuName = "Question Asset")]
public class QuestionAsset : ScriptableObject
{
    [SerializeField]
    [Tooltip("the question")]
    private string _question = "";       public string Question => _question;

    [SerializeField]
    [Tooltip("the answer")]
    private string _answer = "";         public string Answer => _answer;

    private int _heart = 0;              public int Heart => _heart;
    private float _pupil = 0;              public float Pupil => _pupil;
    private int _amplitude = 0;          public int Amplitude => _amplitude;
    private int _longueur = 0;           public int Longueur => _longueur;

    [SerializeField]
    [Tooltip("feeling needed")]
    private string feeling = "vigilant";

    private void Awake()
    {
        Debug.Log("Test");
        if (feeling == "vigilant")
        {
            _heart = 160; _pupil = 0.2f; _amplitude = 80; _longueur = 100;
        }
        else if (feeling == "disgust")
        {
            _heart = 60; _pupil = 0.5f; _amplitude = 70; _longueur = 80;
        }
        else if (feeling == "rage")
        {
            _heart = 150; _pupil = 0.4f; _amplitude = 50; _longueur = 30;
        }
        else if (feeling == "terror")
        {
            _heart = 180; _pupil = 0.1f; _amplitude = 30; _longueur = 40;
        }
        else if (feeling == "sadness")
        {
            _heart = 50; _pupil = 0.8f; _amplitude = 80; _longueur = 60;
        }
        else if (feeling == "surprise")
        {
            _heart = 100; _pupil = 0.9f; _amplitude = 80; _longueur = 60;
        }
        else if (feeling == "ecstacy")
        {
            _heart = 80; _pupil = 0.9f; _amplitude = 40; _longueur = 50;
        }
        else if (feeling == "admiration")
        {
            _heart = 60; _pupil = 0.5f; _amplitude = 40; _longueur = 60;
        }
    }
}