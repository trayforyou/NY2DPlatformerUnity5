using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _smooth;

    private Vector2 _velocity = Vector2.zero;

    private void Update()
    {
        transform.position = Vector2.SmoothDamp(transform.position, _player.position, ref _velocity, _smooth);
   }
}