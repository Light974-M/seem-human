using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartDraw : MonoBehaviour
{
    [Header("")]
    [Header("DRAW____________________________________________________________________________________________________________")]

    [SerializeField]
    [Tooltip("component that will be used for drawing")]
    private LineRenderer lineRender;

    [SerializeField]
    [Tooltip("object that will be used for input detection of heart beat set")]
    private HeartBeatSetDetector heartBeatSetDetector;

    [SerializeField]
    [Tooltip("UI of bpm")]
    private Text _bpmUi;

    [SerializeField, Tooltip("UI of Recquiered BPM")]
    private Text _requieredBPM;

    private bool heartBeat = false;
    private int counter = 0;


    [SerializeField]
    private float currentHeartBeat = 60;
    public float CurrentHeartBeat
    {
        get { return currentHeartBeat; }
        set { currentHeartBeat = value; }
    }

    private float beatcounter = 0;

    private LevelManager levelManager;

    private void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    private void OnEnable()
    {
        levelManager.newQuestionBegin += SetValues;
    }

    private void OnDisable()
    {
        levelManager.newQuestionBegin -= SetValues;
    }

    private void SetValues(QuestionAsset question)
    {
        _requieredBPM.text = question.Heart.ToString();
    }

    private AudioManager audio;
    private void Awake()
    {
        audio = FindObjectOfType<AudioManager>();
    }

    private void Update()
    {
        if (heartBeatSetDetector.IsChangingHeartBeat)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                audio.AudioSource.outputAudioMixerGroup = audio.AudioMixerArray[2];
                audio.AudioSource.PlayOneShot(audio.AudioClipArray[0]);

                heartBeat = true;
                currentHeartBeat = (currentHeartBeat + (60 / beatcounter)) / 2;
                currentHeartBeat = currentHeartBeat / 10;
                currentHeartBeat = Mathf.Round(currentHeartBeat);
                currentHeartBeat = currentHeartBeat * 10;
                // On envoie la valeur au levelManager
                levelManager.HearthRate = currentHeartBeat;
                beatcounter = 0;
            }
        }
    }

    void FixedUpdate()
    {
        _bpmUi.text = $"{currentHeartBeat} BPM";

        transform.position += Vector3.right * (30 * Time.deltaTime);
        lineRender.positionCount++;
        lineRender.SetPosition(lineRender.positionCount - 1, transform.position);

        if(transform.localPosition.x > 450)
        {
            lineRender.positionCount = 0;
            transform.localPosition = new Vector3(0,transform.localPosition.y,0);
        }
        if(heartBeat)
        {
            DrawHeartBeat();
        }
        
        if (beatcounter >= 60 / currentHeartBeat)
        {
            if (!heartBeatSetDetector.IsChangingHeartBeat)
            {
                heartBeat = true;
                beatcounter = 0;
            }
        }

        

        beatcounter += Time.deltaTime;
    }

    private void DrawHeartBeat()
    {
        if (counter <= 1)
        {
            transform.position += Vector3.up * 6f;
            counter++;
        }
        else if (counter <= 3)
        {
            transform.position -= Vector3.up * 12f;
            counter++;
        }
        else if (counter <= 5)
        {
            transform.position += Vector3.up * 6f;
            counter++;
        }
        else
        {
            counter = 0;
            heartBeat = false;
        }
    }
}
