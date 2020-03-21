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
        _nextSpawnPositionX = GetNextSpawnPosition(Camera.main.RightPosition());
    }

    private void Update()
    {
        if (CanSpawn())
            Spawn();
    }

    private void Spawn()
    {
        int coinsInLine = Random.Range(0, _maxCoinsInLine + 1);

        float x = _nextSpawnPositionX;
        float y = 0;
        for (int i = 0; i < coinsInLine; i++)
        {
            Coin inst = Instantiate(_template, Vector2.zero, Quaternion.identity); 

            x += inst.SpriteSize.x;
            RaycastHit2D groundHit = Physics2D.Raycast(new Vector2(x, Camera.main.transform.position.y + Camera.main.orthographicSize*4), Vector2.down);
            y = groundHit.point.y + inst.SpriteSize.y;

            inst.transform.position = new Vector2(x, y);
        }

        _nextSpawnPositionX = GetNextSpawnPosition(x);
    }

    private float GetNextSpawnPosition(float prevPosition)
    {
        return prevPosition + Random.Range(_minSpawnDistance, _maxSpawnDistance);
    }

    private bool CanSpawn()
    {
        return Camera.main.RightPosition() >= _nextSpawnPositionX;
    }
}
