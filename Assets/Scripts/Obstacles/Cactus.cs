using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cactus : Obstacle
{
    private float _force;

    private void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _force = 300f;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Rigidbody2D body))
        {
            
            body.velocity = Vector2.zero;
            body.AddForce((body.transform.position - transform.position).normalized * _force);
        }
    }
}
