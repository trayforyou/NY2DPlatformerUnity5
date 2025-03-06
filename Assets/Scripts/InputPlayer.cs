using System;
using UnityEngine;

[RequireComponent(typeof(PlayerMover))]
public class InputPlayer : MonoBehaviour
{
    public const string Horizontal = nameof(Horizontal);
    public const string Jump = nameof(Jump);
    public const string Fire1 = nameof(Fire1);

    private float _xInput;

    public event Action<float> XInputed;
    public event Action<bool> JumpInputed;
    public event Action<bool> Attacked;

    private void Update()
    {
        _xInput = Input.GetAxis(Horizontal);

        XInputed?.Invoke(_xInput);

        if (Input.GetButtonDown(Jump))
            JumpInputed?.Invoke(true);

        if(Input.GetButtonDown(Fire1))
            Attacked?.Invoke(true);
    }
}