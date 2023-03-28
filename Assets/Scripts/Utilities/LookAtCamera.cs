using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private enum Mode
    {
        LookAt,
        LookAtInverted,
        CamForward,
        CamForwardInverted,
        CamForwardMirror,
    }

    [SerializeField] private Mode _mode;

    private void LateUpdate()
    {
        switch (_mode)
        {
            case Mode.LookAt:
                transform.LookAt(Camera.main.transform);
                break;
            case Mode.LookAtInverted:
                var dirFromCam = transform.position - Camera.main.transform.position;
                transform.LookAt(transform.position + dirFromCam);
                break;
            case Mode.CamForward:
                transform.forward = Camera.main.transform.forward;
                break;
            case Mode.CamForwardInverted:
                transform.forward = -Camera.main.transform.forward;
                break;
            case Mode.CamForwardMirror:
                transform.forward = Camera.main.transform.forward;
                transform.localScale.Set(-1f, 1f, 1f);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}