using System;
using UnityEngine;

public class InputPlayer : MonoBehaviour
{
    private float _xInput;

    public event Action<float> XInputed;
    public event Action JumpInputed;

    private void Update()
    {
        _xInput = Input.GetAxis("Horizontal");

        XInputed?.Invoke(_xInput);

        if (Input.GetButtonDown("Jump"))
            JumpInputed?.Invoke();
    }
}