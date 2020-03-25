using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private Vector2 _offset;

    private void FixedUpdate()
    {
        Vector3 _nextPosition = _target.position + (Vector3)_offset;
        _nextPosition.z = transform.position.z;

        transform.position = Vector3.Lerp(transform.position, _nextPosition, _movementSpeed * Time.deltaTime);
    }
}
