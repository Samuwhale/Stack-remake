using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class Floor : MonoBehaviour
{
    private Material _material;
    [SerializeField] private float _colorChangeDuration;
    private void Awake()
    {
        _material = GetComponent<Renderer>().material;
    }

    void Start()
    {
        MS.Main.GameManager.OnGameReset += GameManager_OnGameReset;
    }

    private void GameManager_OnGameReset()
    {
        SetRandomColor();
    }



    private void SetColor(Color color)
    {
        _material.DOColor(color, _colorChangeDuration);
    }
    
    private void SetRandomColor()
    {
        
        SetColor(Random.ColorHSV(0, 1f, 0.95f, 1f,0.7f, 0.8f));
    }
}
