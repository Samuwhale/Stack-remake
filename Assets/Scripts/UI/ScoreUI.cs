using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreValueText;
    [SerializeField] private TextMeshProUGUI _highScoreValueText;
    [SerializeField] private GameObject _visibilityObject;


    void Start()
    {
        MS.Main.ScoreManager.OnScoreChanged += UpdateScore;
        MS.Main.ScoreManager.OnHighScoreChanged += UpdateHighScore;
    }

    private void OnEnable()
    {
        UpdateScore();
        UpdateHighScore();
    }

    private void UpdateScore()
    {
        UpdateScoreText(MS.Main.ScoreManager.GetScoreAsString());
        // ChangeScoreColors();
    }
    
    private void UpdateHighScore()
    {
        // ChangeHighScoreColors();
        UpdateHighScoreText(MS.Main.ScoreManager.GetHighScoreAsString());
    }


    void ChangeScoreColors()
    {
        if (_scoreValueText != null)
        {
            _scoreValueText.DOColor(Random.ColorHSV(), 0.5f);
        }
    }


    private void UpdateScoreText(string score)
    {
        if (_scoreValueText != null)
        {
            _scoreValueText.SetText(score);
        }
        
    }
    
    void ChangeHighScoreColors()
    {


        if (_highScoreValueText != null)
        {
            _highScoreValueText.DOColor(Random.ColorHSV(), 0.5f);
        }
    }


    private void UpdateHighScoreText(string score)
    {
   

        if (_highScoreValueText != null)
        {
            _highScoreValueText.SetText(score);
        }
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