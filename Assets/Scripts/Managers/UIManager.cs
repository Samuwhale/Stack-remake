using System;
using System.Collections;
using System.Collections.Generic;
using Sambono;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;

public class UIManager : MonoBehaviour
{
    public SettingsUI SettingsUI {get; private set;}

    public PauseScreenUI PauseScreenUI {get; private set;}

    public GameOverUI GameOverUI {get; private set;}
    
    public StartGameUI StartGameUI {get; private set;}

    // Has to be serialized because there is also an instance on the game over screen.
    [SerializeField] private ScoreUI _scoreUI;

    public ScoreUI ScoreUI => _scoreUI;

    private void Start()
    {
        GetScriptsFromChildren();
    }


    void GetScriptsFromChildren()
    {
        SettingsUI = GetComponentInChildren<SettingsUI>();
        
        
        PauseScreenUI = GetComponentInChildren<PauseScreenUI>();
        
        
        GameOverUI = GetComponentInChildren<GameOverUI>();

        StartGameUI = GetComponentInChildren<StartGameUI>();
    }
}