using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Obstacle : MonoBehaviour
{
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
