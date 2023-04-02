using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaySoundOnButtonClick : MonoBehaviour
{
    private Button _button;
    public static event Action OnAnyButtonClicked;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(PlayOnClickSound);
    }
    
    private void PlayOnClickSound()
    {
        OnAnyButtonClicked?.Invoke();
    }
}
