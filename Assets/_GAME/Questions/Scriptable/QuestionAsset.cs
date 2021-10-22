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

    [SerializeField]
    [Tooltip("heart rythm asked")]
    private int _heart = 0;              public int Heart => _heart;

    [SerializeField]
    [Tooltip("pupil dilation asked")]
    private float _pupil = 0;              public float Pupil => _pupil;

    [SerializeField]
    [Tooltip("inspiration point")]
    private int _amplitude = 0;          public int Amplitude => _amplitude;

    [SerializeField]
    [Tooltip("expiration point")]
    private int _longueur = 0;           public int Longueur => _longueur;


}
