using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public enum Direction
{
    Horizontal,
    Forward,
}

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
    private Direction movementDirection;

    [SerializeField] private GameObject _fallCubePrefab;

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


    public void SetHeadings(Direction direction)
    {
        Vector3 directionVector;
        switch (direction)
        {
            case Direction.Horizontal:
                directionVector = transform.right;
                break;
            case Direction.Forward:
                directionVector = transform.forward;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
        }

        _fromLocation = transform.position + directionVector * (_moveDistance / 2);
        _toLocation = transform.position + -directionVector * (_moveDistance / 2);
        movementDirection = direction;
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

    float CalculateOverhang(Vector3 previousCubeLocalPos)
    {
        switch (movementDirection)
        {
            case Direction.Horizontal:
                return transform.localPosition.x - previousCubeLocalPos.x;

            case Direction.Forward:
                return transform.localPosition.z - previousCubeLocalPos.z;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    Vector3 CalculateNewScale(float overhang)
    {
        switch (movementDirection)
        {
            case Direction.Horizontal:
                return new Vector3(transform.localScale.x - Mathf.Abs(overhang), transform.localScale.y, transform.localScale.z);
            case Direction.Forward:
                return new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z - Mathf.Abs(overhang));
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    Vector3 CalculateNewPosition(float overhang)
    {
        switch (movementDirection)
        {
            case Direction.Horizontal:
                return new Vector3(transform.localPosition.x - overhang / 2f, transform.localPosition.y, transform.localPosition.z);
            case Direction.Forward:
                return new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z - overhang / 2f);
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    void CreateFallCube(float overhang)
    {
        var fallCubeGameObject = Instantiate(_fallCubePrefab, transform.parent);
        FallCube fallCube = fallCubeGameObject.GetComponent<FallCube>();
        fallCube.SetColor(_material.color);
        
        switch (movementDirection)
        {
            
            case Direction.Horizontal:
                float fallCubeScaleX = Mathf.Abs(overhang);
                float fallCubePosX = overhang > 0
                    ? transform.localPosition.x + transform.localScale.x / 2 + fallCubeScaleX / 2
                    : transform.localPosition.x - transform.localScale.x / 2 - fallCubeScaleX / 2;
                
                
                fallCube.SetLocalScaleAndPosition(new Vector3(fallCubeScaleX, transform.localScale.y, transform.localScale.z),
                    new Vector3(fallCubePosX, transform.localPosition.y, transform.localPosition.z));
                
                break;
            case Direction.Forward:
                float fallCubeScaleZ = Mathf.Abs(overhang);
                float fallCubePosZ = overhang > 0
                    ? transform.localPosition.x + transform.localScale.x / 2 + fallCubeScaleZ / 2
                    : transform.localPosition.x - transform.localScale.x / 2 - fallCubeScaleZ / 2;

                fallCube.SetLocalScaleAndPosition(new Vector3(transform.localScale.x, transform.localScale.y,fallCubeScaleZ),
                    new Vector3(transform.localPosition.x, transform.localPosition.y,fallCubePosZ));
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

    }
    
    public bool TryPlace(Cube previousCube)
    {
        StopMoving();
        Vector3 previousCubeLocalPos;

        if (previousCube != null)
        {
            previousCubeLocalPos = previousCube.transform.localPosition;
        }
        else
        {
            previousCubeLocalPos = Vector3.zero - Vector3.up * transform.localScale.y;
        }


        // float overhangX = transform.localPosition.x - previousCubeLocalPos.x;
        //float newScaleX = transform.localScale.x - Mathf.Abs(overhangX);
        float overhang = CalculateOverhang(previousCubeLocalPos);

        Vector3 newScale = CalculateNewScale(overhang);

        if (newScale.x <= 0 || newScale.z <= 0)
        {
            gameObject.AddComponent<Rigidbody>();
            return false;
        }

        Vector3 newPosition = CalculateNewPosition(overhang);

        // float newPosX = transform.localPosition.x - overhangX / 2;
        transform.localPosition = newPosition;
        transform.localScale = newScale;

        CreateFallCube(overhang);
        
        return true;
    }
}