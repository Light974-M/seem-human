using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartDraw : MonoBehaviour
{
    [SerializeField]
    [Tooltip("component that will be used for drawing")]
    private LineRenderer lineRender;

    [SerializeField]
    [Tooltip("object that will be used for input detection of heart beat set")]
    private HeartBeatSetDetector heartBeatSetDetector;

    [SerializeField]
    [Tooltip("UI of bpm")]
    private Text _bpmUi;

    private bool heartBeat = false;
    private int counter = 0;

    private float currentHeartBeat = 60;
    private float beatcounter = 0;

    void Update()
    {
        _bpmUi.text = $"{currentHeartBeat} BPM";

        transform.position += Vector3.right * (20 * Time.deltaTime);
        lineRender.positionCount++;
        lineRender.SetPosition(lineRender.positionCount - 1, transform.position);

        if(transform.localPosition.x > 120)
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
            if(!heartBeatSetDetector.IsChangingHeartBeat)
            {
                heartBeat = true;
                beatcounter = 0;
            }
        }

        if(heartBeatSetDetector.IsChangingHeartBeat)
        {
            if(Input.GetKeyDown(KeyCode.Mouse0))
            {
                heartBeat = true;
                currentHeartBeat = (currentHeartBeat + (60 / beatcounter)) / 2;
                currentHeartBeat = currentHeartBeat / 10;
                currentHeartBeat = Mathf.Round(currentHeartBeat);
                currentHeartBeat = currentHeartBeat * 10;
                beatcounter = 0;
            }
        }

        beatcounter += Time.deltaTime;
    }

    private void DrawHeartBeat()
    {
        if (counter <= 10)
        {
            transform.position += Vector3.up * 1f;
            counter++;
        }
        else if (counter <= 21)
        {
            transform.position -= Vector3.up * 2f;
            counter++;
        }
        else if (counter <= 32)
        {
            transform.position += Vector3.up * 1f;
            counter++;
        }
        else
        {
            counter = 0;
            heartBeat = false;
        }
    }
}
