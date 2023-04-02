using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour, IHasSettings {
    [SerializeField] private GameObject _visibilityObject;
    [SerializeField] private Button _playAgainButton;
    [SerializeField] private Button _settingsButton;

    private void Awake() {
        _playAgainButton.onClick.AddListener(OnPlayAgainClicked);
        _settingsButton.onClick.AddListener(OnSettingsClicked);
        
    }

    private void OnSettingsClicked()
    {
        MS.Main.UIManager.SettingsUI.Show();
        MS.Main.UIManager.SettingsUI.SetPreviousObject(this);
        Hide();
    }

    private void Start()
    {
        MS.Main.GameManager.GameLost += GameManager_OnGameLost;
        MS.Main.GameManager.OnGameStateChanged += GameManager_OnGameStateChanged;
        Hide();
    }

    private void GameManager_OnGameStateChanged(GameManager.States currentState)
    {
        if (currentState != GameManager.States.GameOver) Hide();
    }

    private void GameManager_OnGameLost()
    {
        Show();
    }


    void OnPlayAgainClicked() {
        MS.Main.GameManager.ResetGame();
    }
    
    

    public void Show()
    {
        _visibilityObject.SetActive(true);
        if (MS.Main.InputManager.IsUsingGamepad()) _playAgainButton.Select();
    }
    
    public void Hide()
    { 
        _visibilityObject.SetActive(false);
    }
    
}
