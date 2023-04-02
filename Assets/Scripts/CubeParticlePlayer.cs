using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeParticlePlayer : MonoBehaviour
{
    private Cube _cube;
    private ParticleSystem _particleSystem;

    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }

    private void Start()
    {
        _cube = GetComponentInParent<Cube>();
        _cube.OnPlaced += PlayPlaceParticle;
    }

    public void PlayPlaceParticle()
    {
        transform.localScale = transform.parent.localScale;
        _particleSystem.Play();
    }
}
