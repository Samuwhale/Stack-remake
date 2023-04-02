using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;

using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PauseScreenUI : MonoBehaviour, IHasSettings
{
    [SerializeField] private GameObject _visibilityObject;
    [SerializeField] private Button _resumeGameButton;
    [SerializeField] private Button _toSettingsButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _exitToMenuButton;


    private void Awake()
    {
        _resumeGameButton.onClick.AddListener(ResumeGamePressed);
        _toSettingsButton.onClick.AddListener(ToSettingsPressed);
        _exitToMenuButton.onClick.AddListener(ExitToMenuPressed);
        _restartButton.onClick.AddListener(RestartButtonPressed);
    }

    private void RestartButtonPressed()
    {
        MS.Main.GameManager.ResetGame();
        Hide();
    }


    private void Start()
    {
        MS.Main.GameManager.OnGamePaused += Show;
        MS.Main.GameManager.OnGameResumed += Hide;
        Hide();
    }

    private void ToSettingsPressed()
    {
        MS.Main.UIManager.SettingsUI.Show();
        MS.Main.UIManager.SettingsUI.SetPreviousObject(this);
        Hide();
    }



    private void ResumeGamePressed()
    {
        MS.Main.GameManager.TryResume();
        Hide();
    }

    private void ExitToMenuPressed()
    {
        
    }

    public void Show()
    {
        _visibilityObject.SetActive(true);
        if (MS.Main.InputManager.IsUsingGamepad()) _resumeGameButton.Select();
    }

    public void Hide()
    {
        _visibilityObject.SetActive(false);
    }
}