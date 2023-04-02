using System.Collections;
using System.Collections.Generic;
using Sambono;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;

    public InputReader InputReader => _inputReader;

    public bool IsUsingGamepad()
    {
        return _inputReader.IsUsingGamepad();
    }
}
