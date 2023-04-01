using System;
using System.Collections;
using System.Collections.Generic;
using Sambono;
using UnityEngine;

public class CubeManager : MonoBehaviour
{
    [SerializeField] private Transform _cubePrefab;
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private Transform _cinemachineTarget;
    private Cube _currentCube;
    private Cube _previousCube;
    private List<Cube> _previousCubes = new List<Cube>();
    private bool _shouldSpawn;
    [SerializeField] private float _cubeHeight;

    private void Start()
    {
        MS.Main.GameManager.OnGameReset += GameManager_OnGameReset;
        MS.Main.GameManager.OnGameStateChanged += GameManager_OnStateChanged;
        _inputReader.InteractEvent += InputReader_OnInteract;
    }

    private void InputReader_OnInteract()
    {
        if (_currentCube.TryPlace(_previousCube)) SpawnCube();
        else MS.Main.GameManager.TriggerGameOver();
    }

    private void GameManager_OnStateChanged(GameManager.States state)
    {
        _shouldSpawn = state == GameManager.States.Playing;
    }

    private void GameManager_OnGameReset()
    {
        foreach (var cube in _previousCubes)
        {
            Destroy(cube.gameObject);
        }

        _previousCubes.Clear();

        if (_currentCube != null)
        {
            var cubeToDestroy = _currentCube.gameObject;
            _currentCube = null;
            Destroy(cubeToDestroy);
        }

        if (_previousCube != null)
        {
            var cubeToDestroy = _currentCube.gameObject;
            _previousCube = null;
            Destroy(cubeToDestroy);
        }

        SpawnCube();
    }

    private void SpawnCube()
    {
        if (_currentCube != null)
        {
            _previousCube = _currentCube;
            _previousCubes.Add(_previousCube);
        }

        var newCubeTrans = Instantiate(_cubePrefab, transform);


        if (_previousCube != null)
        {
            newCubeTrans.localScale = _previousCube.transform.localScale;
            newCubeTrans.localPosition = _previousCube.transform.localPosition + Vector3.up * _cubeHeight;
        }
        
        _cinemachineTarget.position = new Vector3(_cinemachineTarget.position.x, newCubeTrans.position.y, _cinemachineTarget.position.z);
        Cube newCube = newCubeTrans.GetComponent<Cube>();
        if (_previousCubes.Count % 2 == 0)
        {
            newCube.SetHeadings(Direction.Forward);
        }
        else
        {
            newCube.SetHeadings(Direction.Horizontal);
        }

        newCube.StartMoving();
        _currentCube = newCube;
    }
}