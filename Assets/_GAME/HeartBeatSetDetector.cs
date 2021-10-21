using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartBeatSetDetector : MonoBehaviour
{
    private bool _isChangingHeartBeat;    public bool IsChangingHeartBeat => _isChangingHeartBeat;

    private void OnMouseOver()
    {
        if(Input.GetKey(KeyCode.Mouse1))
        {
            _isChangingHeartBeat = true;
        }
        else
        {
            _isChangingHeartBeat = false;
        }
    }

    private void OnMouseExit()
    {
        _isChangingHeartBeat = false;
    }
}
