using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[Serializable]
public struct TileValueColor
{
    public Color Color;
    public int Value;
}



[CreateAssetMenu(menuName = "ValueColorSO")]
public class ValueColorsSO : ScriptableObject
{
    public Color DefaultColor;
    
    public TileValueColor[] TileValueColors;

    public Color GetColorFromValue(int value)
    {
        foreach (var tileValueColor in TileValueColors)
        {
            
            if (tileValueColor.Value == value)
            {
                return tileValueColor.Color;
            }
        }

        return DefaultColor;
    }
}


