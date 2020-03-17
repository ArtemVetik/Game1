using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Mine : Obstacle
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _sprite = GetComponent<SpriteRenderer>();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Rigidbody2D ball))
        {
            ball.velocity = new Vector2(0, ball.velocity.y);
            _animator.SetTrigger("Explosion");
        }
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
