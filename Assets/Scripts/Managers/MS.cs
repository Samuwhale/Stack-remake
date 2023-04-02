using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SocialPlatforms.Impl;

public class MS : MonoBehaviour
{
    public static MS Main { get; private set; }

    [SerializeField]  GameManager _gameManager;
    [SerializeField]  UIManager _uIManager;
    
    [SerializeField]  EventManager _eventManager;
    [SerializeField]  AudioManager _audioManager;
    [SerializeField]  ScoreManager _scoreManager;
    
    
    // Input SHOULD be accessed through the scriptable object 'InputReader', but for quick and dirty testing an
    // instance of it can be accessed through the InputManager (which basically just exposes an InputReader).
    
    [SerializeField] InputManager _inputManager;

    public GameManager GameManager => _gameManager;

    public UIManager UIManager => _uIManager;

    public EventManager EventManager => _eventManager;

    public AudioManager AudioManager => _audioManager;
    public ScoreManager ScoreManager => _scoreManager;

    public InputManager InputManager => _inputManager;


    private void Awake()
    {
        if (Main == null)
        {
            Main = this;
            DontDestroyOnLoad(this);
            
        }
        else
        {
            Destroy(gameObject);
        }
    }

    
}