using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;

using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class StartGameUI : MonoBehaviour, IHasSettings
{
    [SerializeField] private GameObject _visibilityObject;
    [SerializeField] private Button _startGameButton;
    [SerializeField] private Button _toSettingsButton;



    private void Awake()
    {
        _startGameButton.onClick.AddListener(StartGamePressed);
        _toSettingsButton.onClick.AddListener(ToSettingsPressed);
    }


    private void Start()
    {
        MS.Main.GameManager.OnGameStateChanged += GameManager_OnGameStateChanged;
        Hide();
    }

    private void GameManager_OnGameStateChanged(GameManager.States state)
    {
        if (state == GameManager.States.StartMenu)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void ToSettingsPressed()
    {
        MS.Main.UIManager.SettingsUI.Show();
        MS.Main.UIManager.SettingsUI.SetPreviousObject(this);
        Hide();
    }



    private void StartGamePressed()
    {
        MS.Main.GameManager.TryStart();
        Hide();
    }
    

    public void Show()
    {
        _visibilityObject.SetActive(true);
        if (MS.Main.InputManager.IsUsingGamepad()) _startGameButton.Select();
    }

    public void Hide()
    {
        _visibilityObject.SetActive(false);
    }
}