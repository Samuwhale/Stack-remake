using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MS : MonoBehaviour
{
    public static MS Main { get; private set; }

    [SerializeField]  GameManager _gameManager;
    [SerializeField]  UIManager _uIManager;
    
    [SerializeField]  EventManager _eventManager;
    [SerializeField]  AudioManager _audioManager;
    

    public GameManager GameManager => _gameManager;

    public UIManager UIManager => _uIManager;

    public EventManager EventManager => _eventManager;

    public AudioManager AudioManager => _audioManager;


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