using UnityEngine;

[CreateAssetMenu(fileName = "NewQuestionAsset", menuName = "Question Asset")]
public class QuestionAsset : ScriptableObject
{
    [SerializeField]
    [Tooltip("the question")]
    private string _question = "";

    [SerializeField]
    [Tooltip("the answer")]
    private string _answer = "";

    [SerializeField]
    [Tooltip("heart rythm asked")]
    private int _heart = 0;

    [SerializeField]
    [Tooltip("pupil dilation asked")]
    private int _pupil = 0;

    [SerializeField]
    [Tooltip("inspiration point")]
    private int _amplitude = 0;

    [SerializeField]
    [Tooltip("expiration point")]
    private int _longueur = 0;


}
