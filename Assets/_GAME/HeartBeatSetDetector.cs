using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartBeatSetDetector : MonoBehaviour
{
    private bool _isChangingHeartBeat;    public bool IsChangingHeartBeat => _isChangingHeartBeat;

    private void OnMouseOver()
    {
        _isChangingHeartBeat = true;
    }

    private void OnMouseExit()
    {
        _isChangingHeartBeat = false;
    }
}
