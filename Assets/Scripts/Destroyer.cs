using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private Health _health;

    private void Awake() =>
        _health = GetComponent<Health>();

    private void OnEnable() =>
        _health.Ended += Destroy;

    private void OnDisable() =>
        _health.Ended -= Destroy;

    private void Destroy() =>
        Destroy(gameObject);
}