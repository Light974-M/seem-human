using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartDraw : MonoBehaviour
{
    [SerializeField]
    [Tooltip("component that will be used for drawing")]
    private LineRenderer lineRender;

    void Update()
    {
        transform.position += Vector3.up / 10;
        lineRender.positionCount++;
        lineRender.SetPosition(lineRender.positionCount - 1, transform.position);
    }
}
