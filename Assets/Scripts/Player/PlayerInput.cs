using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public const string Horizontal = nameof(Horizontal);
    public const string Jump = nameof(Jump);
    public const string Fire1 = nameof(Fire1);
    public const string Fire2 = nameof(Fire2);
    public const string Pick = nameof(Pick);

    private float _xInput;

    public event Action<float> XInputed;
    public event Action<bool> JumpInputed;
    public event Action Attacked;
    public event Action ActivatedVamirism;
    public event Action Picked;

    private void Update()
    {
        _xInput = Input.GetAxis(Horizontal);

        XInputed?.Invoke(_xInput);

        if (Input.GetButtonDown(Jump))
            JumpInputed?.Invoke(true);

        if (Input.GetButtonDown(Fire1))
            Attacked?.Invoke();

        if (Input.GetButtonDown(Fire2))
            ActivatedVamirism?.Invoke();

        if (Input.GetButtonDown(Pick))
            Picked?.Invoke();
    }
}