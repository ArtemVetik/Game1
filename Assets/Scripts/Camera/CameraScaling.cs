using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScaling : MonoBehaviour
{
    [SerializeField] private Transform _ball;

    private float _startOrthographicSize;
    private LayerMask _groundMask;

    private void Start()
    {
        _startOrthographicSize = Camera.main.orthographicSize;
        _groundMask = LayerMask.NameToLayer("Ball") << 8;
    }

    private void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(_ball.position, Vector2.down, Mathf.Infinity, _groundMask);

        Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, _startOrthographicSize + hit.distance * 0.5f, Time.fixedDeltaTime);
    }
}
