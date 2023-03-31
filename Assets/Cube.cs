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
    private Material _material;
    private bool _shouldMove;

    private void Awake()
    {
        _material = GetComponent<Renderer>().material;
    }

    private void Start()
    {
        MS.Main.GameManager.OnGameStateChanged += GameManager_OnGameStateChanged;
    }

    private void GameManager_OnGameStateChanged(GameManager.States state)
    {
        if (state != GameManager.States.Playing)
        {
            StopMoving();
            return;
        }
        
    }

    private void Update()
    {
        Move();
    }

    public void SetColor(Color color)
    {
        _material.color = color;
    }

    void Move()
    {
        if (!_shouldMove) return;

        _moveTimer += Time.deltaTime;
        _lerpProgress = _moveTimer / _moveCompletionTime;

        transform.position = Vector3.Lerp(_fromLocation, _toLocation, Mathf.SmoothStep(0, 1, _lerpProgress));

        if (_moveTimer > _moveCompletionTime)
        {
            SwitchHeading();
        }
    }


    public void SetHeadings()
    {
        _fromLocation = transform.position + transform.right * (_moveDistance / 2);
        _toLocation = transform.position + -transform.right * (_moveDistance / 2);
    }

    void SwitchHeading()
    {
        _moveTimer = 0;
        (_toLocation, _fromLocation) = (_fromLocation, _toLocation);
    }

    public void StopMoving()
    {
        _shouldMove = false;
    }

    public void StartMoving()
    {
        _shouldMove = true;
    }

    void SplitOver(Transform cubeBelow)
    {
        
    }
}