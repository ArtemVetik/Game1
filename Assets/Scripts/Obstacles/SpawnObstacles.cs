using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstacles : MonoBehaviour
{
    [SerializeField] private List<Obstacle> _tempates;

    private Vector2 _lastSpawnPosition;

    private void Start()
    {
        _lastSpawnPosition = Camera.main.transform.position;
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

        float deviation = Mathf.Acos(hit.normal.x) * 180f / Mathf.PI;
        Instantiate(template, hit.point + new Vector2(0, template.transform.localScale.y), Quaternion.Euler(0, 0, deviation - 90f));
        _lastSpawnPosition = hit.point;
    }

    private bool CanSpawn()
    {
        return Camera.main.RightPosition() - _lastSpawnPosition.x > 20f;
    }
}
