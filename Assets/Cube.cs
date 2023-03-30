using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class Cube : MonoBehaviour
{
    [SerializeField] private float _moveCompletionTime;
    [SerializeField] private float _moveDistance;
    private int _currentDirection = 1;
    private Vector3 _fromLocation;
    private Vector3 _toLocation;
    private float _moveTimer = 0;
    private float _lerpProgress; 
    
    private void Start()
    {
        SetHeadings();
        Move();
    }

    private void Update()
    {
        Move();
    }

    void Move()
    {
        _moveTimer += Time.deltaTime;
        _lerpProgress = _moveTimer / _moveCompletionTime;
        
        transform.position = Vector3.Lerp(_fromLocation, _toLocation, Mathf.SmoothStep(0, 1, _lerpProgress));

        if (_moveTimer > _moveCompletionTime)
        {
            SwitchHeading();
        }
        
        Debug.Log($"moveTimer = {_moveTimer}, lerpprog = {_lerpProgress}");
    }


    void SetHeadings()
    {
        _fromLocation = transform.position + transform.right * (_moveDistance / 2);
        _toLocation = transform.position + -transform.right * (_moveDistance / 2);
    }

    void SwitchHeading()
    {
        _moveTimer = 0;
        (_toLocation, _fromLocation) = (_fromLocation, _toLocation);
    }
}
