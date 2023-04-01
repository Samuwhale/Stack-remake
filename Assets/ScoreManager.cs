using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int _currentScore;

    private int _highScore;

    public int GetScore()
    {
        return _currentScore;
    }

    public int GetHighScore()
    {
        return _highScore;
    }
    
    public string GetScoreAsString()
    {
        return _currentScore.ToString();
    }

    public string GetHighScoreAsString()
    {
        return _highScore.ToString();
    }

    public event Action OnScoreChanged;
    public event Action OnHighScoreChanged;
    private void Awake()
    {
        _highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    private void Start()
    {
        MS.Main.GameManager.OnGameReset += GameManager_OnGameReset;
        ResetScore();
    }

    private void GameManager_OnGameReset()
    {
        ResetScore();
    }

    public void AddScore(int amount)
    {
        _currentScore += amount;
        OnScoreChanged?.Invoke();
        if (_currentScore > _highScore)
        {
            _highScore = _currentScore;
            OnHighScoreChanged?.Invoke();
            PlayerPrefs.SetInt("HighScore", _highScore);
            PlayerPrefs.Save();;
        }
    }

    public void ResetScore()
    {
        _currentScore = 0;
        OnScoreChanged?.Invoke();
    }

    [Button]
    public void ClearPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}