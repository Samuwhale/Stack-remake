using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetTextToPaletteColor : MonoBehaviour
{
    [SerializeField] private ColorPaletteSO _palette;

    [SerializeField] private int _colorIndex;

    private TextMeshProUGUI _text;
    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        if (_text != null & _palette.Colors.Length > _colorIndex)
        {
            _text.color = _palette.Colors[_colorIndex];
        }
    }
}
