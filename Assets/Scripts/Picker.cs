using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Picker : MonoBehaviour
{
    [SerializeField] private int _applesCount;

    private void Awake()
    {
        _applesCount = 0;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Apple>(out _))
        {
            _applesCount++;
            Destroy(collision.gameObject);
        }
    }
}