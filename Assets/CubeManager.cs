using System;
using System.Collections;
using System.Collections.Generic;
using Sambono;
using UnityEngine;

public class CubeManager : MonoBehaviour
{
    [SerializeField] private Transform _cubePrefab;
    [SerializeField] private InputReader _inputReader;
    private Cube _currentCube;
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
        _currentCube.StopMoving();
        SpawnCube();
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

        if (_currentCube != null) Destroy(_currentCube.gameObject);

        SpawnCube();
    }

    private void SpawnCube()
    {
        if (_currentCube != null) _previousCubes.Add(_currentCube);
        var newCubeTrans = Instantiate(_cubePrefab, transform);
        newCubeTrans.position = newCubeTrans.position + Vector3.up * _previousCubes.Count * _cubeHeight;
        Cube newCube = newCubeTrans.GetComponent<Cube>();
        newCube.SetHeadings();
        newCube.StartMoving();
        _currentCube = newCube;
    }
}