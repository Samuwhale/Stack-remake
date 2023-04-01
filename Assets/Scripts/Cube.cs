using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

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
        SetRandomColor();
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

    public void SetRandomColor()
    {
        SetColor(Random.ColorHSV(0, 1f, 0.95f, 1f, 0.95f, 1f));
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

    public bool TryPlace(Cube previousCube)
    {
        StopMoving();
        Vector3 previousCubeLocalPos;
        Vector3 previousCubeLocalScale;

        if (previousCube != null)
        {
            previousCubeLocalPos = previousCube.transform.localPosition;
            previousCubeLocalScale = previousCube.transform.localScale;
        }
        else
        {
            previousCubeLocalPos = Vector3.zero - Vector3.up * transform.localScale.y;
            previousCubeLocalScale = new Vector3(1, 0.1f, 1);
        }

        
        float overhangX = transform.localPosition.x - previousCubeLocalPos.x;


        float newScaleX = transform.localScale.x - Mathf.Abs(overhangX);
        if (newScaleX <= 0)
        {
            gameObject.AddComponent<Rigidbody>();
            return false;
        }

        float newPosX = transform.localPosition.x - overhangX / 2;

        Vector3 newPosition = new Vector3(newPosX, transform.localPosition.y,
            transform.localPosition.z);
        Vector3 newScale = new Vector3(newScaleX, transform.localScale.y,
            transform.localScale.z);

        transform.localPosition = newPosition;
        transform.localScale = newScale;

        // GameObject fallCube = GameObject.CreatePrimitive(PrimitiveType.Cube);

        return true;
    }
}