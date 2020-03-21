using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Mine : Obstacle
{
    private Animator _animator;

    private void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Rigidbody2D body))
        {
            body.velocity = Vector2.zero;
            body.AddForce((Vector2.left + Vector2.up) * 300f);
            _animator.SetTrigger("Explosion");
        }
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
