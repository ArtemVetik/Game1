using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _movementSpeed;

    private Vector3 _offcet;

    private void Start()
    {
        _offcet = new Vector3(0,0,transform.position.z - _target.position.z);
    }

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, _target.position + _offcet, _movementSpeed * Time.deltaTime);
    }
}
