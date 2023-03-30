using System;
using System.Collections;
using System.Collections.Generic;
using Sambono;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    
    
    public enum States {
        StartMenu,
        Playing,
        Menu,
        GameOver,
        GameWon,
        Empty
    }
    
    private States _currentState = States.Empty;

    public States CurrentState {
        get => _currentState;
        private set => _currentState = value;
    }
    
    public event Action<States> OnGameStateChanged;
    public event Action OnGamePaused;
    public event Action OnGameResumed;

    public event Action OnGameReset;
    
    public event Action GameWon;
    
    public event Action GameLost;
    

    public bool IsPaused { get; private set; } 
    private void Update() {
        UpdateFromState(_currentState);
    }

    private void Start()
    {
        _inputReader.PauseEvent += InputReader_OnPauseEvent;
        _inputReader.UnpauseEvent += InputReader_OnUnpauseEvent;
        SwitchState(States.StartMenu);
    }
    

    private void InputReader_OnPauseEvent()
    {
        TryPause();
    }

    private void InputReader_OnUnpauseEvent()
    {
        TryResume();
    }

    public void TryStart()
    {
        if (_currentState == States.StartMenu)
        {
            ResetGame();
        }
    }

    public void ResetGame()
    {
        SwitchState(States.Playing);
        OnGameReset?.Invoke();
    }
    
    public void TryResume()
    {
        if (!IsPaused) return;
        IsPaused = false;
        _inputReader.SwitchToGameplay();
        OnGameResumed?.Invoke();
    }

    public void TryPause()
    {
        if (IsPaused) return;
        if (_currentState == States.StartMenu) return;
        IsPaused = true;
        _inputReader.SwitchToUI(); 
        OnGamePaused?.Invoke();
    }

    private void UpdateFromState(States state) {
        switch (state) {
            case States.StartMenu:
                break;
            case States.Playing:
                
                break;
            case States.Menu:
                
                break;
            case States.GameOver:
                
                break;
            case States.GameWon:
                
                break;
            case States.Empty:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    private void EnterState(States state) {
        switch (state) {
            case States.StartMenu:
                _inputReader.SwitchToUI();
                break;
            case States.Playing:
                _inputReader.SwitchToGameplay(); 
                break;
            case States.Menu:
                _inputReader.SwitchToUI(); 
                break;
            case States.GameOver:
                _inputReader.SwitchToUI();
                GameLost?.Invoke();
                break;
            case States.GameWon:
                _inputReader.SwitchToUI();
                GameWon?.Invoke();
                break;
            case States.Empty:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    private void ExitState(States state) {
        switch (state) {
            case States.StartMenu:
                break;
            case States.Playing:
                break;
            case States.Menu:
                break;
            case States.GameOver:
                break;
            case States.Empty:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

 
    public void SwitchState(States state) {
        Debug.Log($"Called switchstate: {CurrentState} --> {state}");
        if (CurrentState == state) {
            return;
        }
        ExitState(_currentState);
        _currentState = state;
        EnterState(_currentState);
        
        OnGameStateChanged?.Invoke(state);

        Debug.Log($"Switched state to: {CurrentState}");
    }
    
    
}