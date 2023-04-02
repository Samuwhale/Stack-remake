using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallCube : MonoBehaviour
{
    private void Start()
    {
        MS.Main.GameManager.OnGameReset += GameManager_OnGameReset;
    }

    private void GameManager_OnGameReset()
    {
        MS.Main.GameManager.OnGameReset -= GameManager_OnGameReset;
        Destroy(gameObject);
    }

    public void SetLocalScaleAndPosition(Vector3 scale, Vector3 position)
    {
        transform.localScale = scale;
        transform.localPosition = position;
    }

    public void SetColor(Color color)
    {
        GetComponent<Renderer>().material.color = color;
    }
}
