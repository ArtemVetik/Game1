using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstacles : MonoBehaviour
{
    [SerializeField] private List<Obstacle> _tempates;
    [SerializeField] [Range(0f, 1f)] private float _minGroundNormal = 0.65f;
    [SerializeField] private float _minSpawnDistance;
    [SerializeField] private float _maxSpawnDistance;

    private float _nextSpawnPositionX;

    private void OnValidate()
    {
        if (_minSpawnDistance > _maxSpawnDistance)
        {
            float min = _minSpawnDistance;
            _minSpawnDistance = _maxSpawnDistance;
            _maxSpawnDistance = min;
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
            Obstacle template = _tempates[Random.Range(0, _tempates.Count)];
            Spawn(template);
        }
    }

    private void Spawn(Obstacle template)
    {
        Vector2 origin = new Vector2(Camera.main.RightPosition() + template.transform.localScale.x, 100f);
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down);
        if (hit.normal.y < _minGroundNormal)
            return;

        float deviationAngle = Mathf.Acos(hit.normal.x) * 180f / Mathf.PI;

        Obstacle inst = Instantiate(template, hit.point, Quaternion.Euler(0, 0, deviationAngle - 90f));
        inst.transform.position += Vector3.up * (inst.SpriteSize.y / 2);

        _nextSpawnPositionX += Random.Range(_minSpawnDistance, _maxSpawnDistance);
    }

    private bool CanSpawn()
    {
        return Camera.main.RightPosition() > _nextSpawnPositionX;
    }
}
