using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackroundMove : MonoBehaviour
{
    [SerializeField] private BallMovement _ball;
    [SerializeField] private float _shiftSpeed;
    
    private Material _backround;

    private void Awake()
    {
        _backround = GetComponent<Image>().material;
    }

    private void LateUpdate()
    {
        _backround.mainTextureOffset += Vector2.right * _ball.Velocity.x * _shiftSpeed * Time.deltaTime;
    }
}
