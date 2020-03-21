using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Coin : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particles;
    [SerializeField] private int _cost;

    private SpriteRenderer _sprite;

    public Vector3 SpriteSize => _sprite.bounds.size;

    private void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out CoinCollector collector))
        {
            collector.AddCoins(_cost);
            Instantiate(_particles, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        if (transform.position.x < Camera.main.LeftPosition())
            Destroy(gameObject);
    }
}
