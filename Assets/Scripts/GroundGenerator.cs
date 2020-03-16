using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

[RequireComponent(typeof(SpriteShapeController))]
public class GroundGenerator : MonoBehaviour
{
    [SerializeField] private float _heightRange = 5f;
    [SerializeField] private float _minDistanceBetweenPoints = 2f;
    [SerializeField] private float _maxDistanceBetweenPoints = 10f;

    private Spline _spline;
    private float _lowerPosition;
    private float _spawnDistance;

    private void OnValidate()
    {
        if (_minDistanceBetweenPoints > _maxDistanceBetweenPoints)
        {
            float minTmp = _minDistanceBetweenPoints;
            _minDistanceBetweenPoints = _maxDistanceBetweenPoints;
            _maxDistanceBetweenPoints = minTmp;
        }
    }

    private void Awake()
    {
        _spline = GetComponent<SpriteShapeController>().spline;
    }

    private void Start()
    {
        _spawnDistance = 10f;
        _lowerPosition = -_heightRange - Camera.main.orthographicSize * 2;
        _spline.Clear();
        InitSpline();
    }

    private void Update()
    {
        if (CanSpawn())
            AddRightPoint();

        if (CanRemove())
            RemoveLeftPoint();
    }

    private void InitSpline()
    {
        float cameraWidth = 2 * Camera.main.orthographicSize * Screen.width / Screen.height;
        _spline.InsertPointAt(0, new Vector3(Camera.main.LeftPosition(), _lowerPosition));
        _spline.InsertPointAt(1, new Vector3(Camera.main.LeftPosition(), Camera.main.transform.position.y));
        _spline.InsertPointAt(2, new Vector3(Camera.main.RightPosition(), Camera.main.transform.position.y));
        _spline.InsertPointAt(3, new Vector3(Camera.main.RightPosition(), _lowerPosition));
    }

    private void RemoveLeftPoint()
    {
        _spline.RemovePointAt(1);
        _spline.SetPosition(0, new Vector2(_spline.GetPosition(1).x, _lowerPosition));
    }

    private void AddRightPoint()
    {
        float prevXposition = _spline.GetPosition(_spline.LastPointInd()).x;
        float nextXposition = prevXposition + Random.Range(_minDistanceBetweenPoints, _maxDistanceBetweenPoints);
        float nextYposition = Random.Range(-_heightRange / 2, _heightRange / 2);
        Vector3 nextPosition = new Vector3(nextXposition, nextYposition);

        _spline.InsertPointAt(_spline.LastPointInd(), nextPosition);
        _spline.SetPosition(_spline.LastPointInd(), new Vector3(nextXposition, _lowerPosition));

        SetTangentMode(_spline.LastPointInd() - 2);
    }

    private void SetTangentMode(int index)
    {
        _spline.SetTangentMode(index, ShapeTangentMode.Broken);

        Vector3 tangent = _spline.GetPosition(index + 1) - _spline.GetPosition(index - 1);
        float rightLength = (_spline.GetPosition(index + 1) - _spline.GetPosition(index)).magnitude;
        float leftLength = (_spline.GetPosition(index) - _spline.GetPosition(index - 1)).magnitude;
        float lengthKf = 0.2f;

        _spline.SetLeftTangent(index, -tangent.normalized * leftLength * lengthKf);
        _spline.SetRightTangent(index, tangent.normalized * rightLength * lengthKf);
    }

    private bool CanSpawn()
    {
        return _spline.GetPosition(_spline.GetPointCount() - 2).x - Camera.main.RightPosition() < _spawnDistance;
    }

    private bool CanRemove()
    {
        return Camera.main.LeftPosition() - _spline.GetPosition(2).x > 1f;
    }
}
