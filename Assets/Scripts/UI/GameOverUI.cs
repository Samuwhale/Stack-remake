using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour {
    [SerializeField] private GameObject _visibilityObject;
    [SerializeField] private Button _playAgainButton;

    private void Awake() {
        _playAgainButton.onClick.AddListener(OnPlayAgainClicked);
        _playAgainButton.Select();
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
    }
    
    public void Hide()
    {
        _visibilityObject.SetActive(false);
    }
    
}
