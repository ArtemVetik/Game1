using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class BackroundMoveStatic : MonoBehaviour
{
    private enum Direction
    {
        Right = 1, Left = -1,
    }

    [SerializeField] private Direction _direction;
    [SerializeField] private float _speed;

    private Material _backround;

    private void Awake()
    {
        _backround = GetComponent<Image>().material;
    }

    private void LateUpdate()
    {
        _backround.mainTextureOffset += Vector2.left * (int)_direction * _speed * Time.deltaTime;
    }
}
