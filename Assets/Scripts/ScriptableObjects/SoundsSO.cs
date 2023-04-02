using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SoundsSO")]
public class SoundsSO : ScriptableObject
{
    public AudioClip Ambience;
    public AudioClip[] PlaceBlock;
    public AudioClip GameOver;
    public AudioClip UISelect;
}
