using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

[RequireComponent(typeof(SpriteShapeController))]
public class GroundGenerator : MonoBehaviour
{
    [SerializeField] [Range(0.1f, 2f)] private float _heightRange = 1f;

    private SpriteShapeController _spriteShape;
    private float _lowerPosition;

    private void Awake()
    {
        _spriteShape = GetComponent<SpriteShapeController>();
    }

    private void Start()
    {
        _lowerPosition = -_heightRange - Camera.main.orthographicSize * 2;
        _spriteShape.spline.Clear();
        InitSpline();
    }

    private void Update()
    {
        if (CanSpawn())
        {
            AddPoint();
        }
        if (CanRemove())
        {
            RemovePoint();
        }
    }

    private void RemovePoint()
    {
        _spriteShape.spline.RemovePointAt(1);
        _spriteShape.spline.SetPosition(0,new Vector2(_spriteShape.spline.GetPosition(1).x, _lowerPosition));
    }

    private void AddPoint()
    {
        float lastX = _spriteShape.spline.GetPosition(_spriteShape.spline.GetPointCount() - 1).x;
        Vector3 point = new Vector3(lastX + 5f,0);
        _spriteShape.spline.InsertPointAt(_spriteShape.spline.GetPointCount() - 1, point);
        _spriteShape.spline.SetPosition(_spriteShape.spline.GetPointCount() - 1, new Vector3(lastX + 5f, _lowerPosition));
    }

    private void InitSpline()
    {
        float cameraWidth = 2 * Camera.main.orthographicSize * Screen.width / Screen.height;
        _spriteShape.spline.InsertPointAt(0, new Vector3(Camera.main.LeftPosition(), _lowerPosition));
        _spriteShape.spline.InsertPointAt(1, new Vector3(Camera.main.LeftPosition(), Camera.main.transform.position.y));
        _spriteShape.spline.InsertPointAt(2, new Vector3(Camera.main.RightPosition(), Camera.main.transform.position.y));
        _spriteShape.spline.InsertPointAt(3, new Vector3(Camera.main.RightPosition(), _lowerPosition));
    }

    private bool CanSpawn()
    {
        return _spriteShape.spline.GetPosition(_spriteShape.spline.GetPointCount() - 2).x - Camera.main.RightPosition() < 10f;
    }

    private bool CanRemove()
    {
        return Camera.main.LeftPosition() - _spriteShape.spline.GetPosition(2).x > 1f;
    }
}
