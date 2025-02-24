using UnityEngine;

public class Fliper : MonoBehaviour
{
    private int _mirrorFlip;
    private bool _isLookAtRight;

    private void Awake()
    {
        _isLookAtRight = true;
        _mirrorFlip = 180;
    }

    public void Flip(float xDirection)
    {
        if(xDirection < 0 && _isLookAtRight)
        {
            _isLookAtRight = false;
            transform.Rotate(Vector2.up,_mirrorFlip);
        }
        else if(xDirection > 0 && !_isLookAtRight)
        {
            _isLookAtRight = true;
            transform.Rotate(Vector2.up, _mirrorFlip);
        }
    }
}