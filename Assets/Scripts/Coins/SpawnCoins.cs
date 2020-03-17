using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCoins : MonoBehaviour
{
    [SerializeField] private Coin _template;
    [SerializeField] private float _minSpawnDistance;
    [SerializeField] private float _maxSpawnDistance;
    [SerializeField] private int _maxCoinsInLine;

    private float _nextSpawnPositionX;

    private void OnValidate()
    {
        if (_minSpawnDistance > _maxSpawnDistance)
        {
            float minTmp = _minSpawnDistance;
            _minSpawnDistance = _maxSpawnDistance;
            _maxSpawnDistance = minTmp;
        }
    }

    private void Start()
    {
        _nextSpawnPositionX = Camera.main.RightPosition() + Random.Range(_minSpawnDistance, _maxSpawnDistance);
    }

    private void Update()
    {
        if (CanSpawn())
        {
            Spawn();
        }
    }

    private void Spawn()
    {
        int count = Random.Range(0, _maxCoinsInLine + 1);

        float x = _nextSpawnPositionX;
        float y = 0;
        for (int i = 0; i < count; i++)
        {
            Coin inst = Instantiate(_template, Vector2.zero, Quaternion.identity);

            x += inst.SpriteSize.x;
            y = Physics2D.Raycast(new Vector2(x, 100f), Vector2.down).point.y + inst.SpriteSize.y;

            inst.transform.position = new Vector2(x, y);
        }

        _nextSpawnPositionX = x + Random.Range(_minSpawnDistance, _maxSpawnDistance);
    }

    private bool CanSpawn()
    {
        return Camera.main.RightPosition() > _nextSpawnPositionX;
    }
}
