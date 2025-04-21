using System.Collections.Generic;
using UnityEngine;

public class ItemsSpawner : MonoBehaviour
{
    [SerializeField] List<ItemSpawnPoint> _spawnPoints;
    [SerializeField] Item _prefab;
    [SerializeField] int _countMedKits;

    private ItemSpawnPoint _tempSpawnPoint;
    private Vector2 _tempPosition;
    private int _tempElementIndex;

    private void Awake()
    {
        CheckMedKitCount();
        SpawnItems();
    }

    private void CheckMedKitCount()
    {
        if (_countMedKits > _spawnPoints.Count)
            _countMedKits = _spawnPoints.Count;
    }

    private void SpawnItems()
    {
        Item tempItem;

        for (int i = 0; i < _countMedKits; i++)
        {
            tempItem = Instantiate(_prefab);

            tempItem.transform.position = GetPosition();
        }
    }

    private Vector2 GetPosition()
    {
        _tempElementIndex = Random.Range(0, _spawnPoints.Count);
        _tempSpawnPoint = _spawnPoints[_tempElementIndex];
        _tempPosition = _tempSpawnPoint.transform.position;

        _spawnPoints.RemoveAt(_tempElementIndex);

        return _tempPosition;
    }
}