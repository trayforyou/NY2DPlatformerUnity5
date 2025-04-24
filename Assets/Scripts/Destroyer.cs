using UnityEngine;

[RequireComponent(typeof(Health))]
public class Destroyer : MonoBehaviour
{
    private Health _health;

    private void Awake() =>
        _health = GetComponent<Health>();

    private void OnEnable() =>
        _health.HealthEnded += Destroy;

    private void OnDisable() =>
        _health.HealthEnded -= Destroy;

    private void Destroy() =>
        Destroy(gameObject);
}