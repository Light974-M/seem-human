using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreathSetDetector : MonoBehaviour
{
    [SerializeField]
    [Tooltip("mesh that will be used for scale breath")]
    private Transform breathMesh;

    [SerializeField]
    [Tooltip("mesh that will be used for scale breath asked")]
    private Transform breathAskedMesh;

    private float memoX = 0;
    private float memoY = 0;
    
    private void OnMouseDrag()
    {
        Vector3 mousePos = Input.mousePosition;
        float changeX = 0;
        float changeY = 0;

        if(mousePos.x > memoX)
        {
            changeX = 1;
        }
        else if (mousePos.x < memoX)
        {
            if(breathMesh.localScale.x >= 0.25)
            {
                changeX = -1;
            }
        }

        if (mousePos.y > memoY)
        {
            changeY = 1;
        }
        else if (mousePos.y < memoY)
        {
            if (breathMesh.localScale.y >= 0.25)
            {
                changeY = -1;
            }
        }

        // x = 0.25/5.5    y = 0.25/2.25

        breathMesh.localScale += new Vector3(changeX * (Time.deltaTime * 2), changeY * (Time.deltaTime * 2), 0);


        memoX = mousePos.x;
        memoY = mousePos.y;
    }
}
