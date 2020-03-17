using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public abstract class Obstacle : MonoBehaviour
{
    protected SpriteRenderer _sprite;

    public Vector3 SpriteSize => _sprite.bounds.size;

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    private void OnBecameInvisible()
    {
        if (transform.position.x < Camera.main.LeftPosition())
            Destroy(gameObject);
    }
}
