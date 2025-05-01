using System;
using UnityEngine;

public class Fliper : MonoBehaviour
{
    private int _mirrorFlip;
    private bool _isLookAtRight;

    public event Action<bool> LookingAtRight;

    private void Awake()
    {
        _isLookAtRight = true;

        LookingAtRight?.Invoke(_isLookAtRight);

        _mirrorFlip = 180;
    }

    public void Flip(float xDirection)
    {
        if(xDirection < 0 && _isLookAtRight)
        {
            _isLookAtRight = false;

            LookingAtRight?.Invoke(_isLookAtRight);

            transform.Rotate(Vector2.up,_mirrorFlip);
        }
        else if(xDirection > 0 && !_isLookAtRight)
        {
            _isLookAtRight = true;

            LookingAtRight?.Invoke(_isLookAtRight);

            transform.Rotate(Vector2.up, _mirrorFlip);
        }
    }
}