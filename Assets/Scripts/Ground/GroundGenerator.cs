using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

[RequireComponent(typeof(SpriteShapeController))]
public class GroundGenerator : MonoBehaviour
{
    [SerializeField] private float _heightRange;
    [SerializeField] private float _minDistanceBetweenPoints;
    [SerializeField] private float _maxDistanceBetweenPoints;

    private Spline _ground;
    private float _lowerPosition;
    private float _spawnDistance;
    private float _removeDistance;

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
        _ground = GetComponent<SpriteShapeController>().spline;
    }

    private void Start()
    {
        _spawnDistance = _removeDistance = Camera.main.WorldWidth();
        _lowerPosition = -_heightRange - Camera.main.orthographicSize * 2;
        _ground.Clear();
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
        float cameraWidth = Camera.main.WorldWidth();
        _ground.InsertPointAt(0, new Vector3(Camera.main.LeftPosition(), _lowerPosition));
        _ground.InsertPointAt(1, new Vector3(Camera.main.LeftPosition(), Camera.main.transform.position.y));
        _ground.InsertPointAt(2, new Vector3(Camera.main.RightPosition(), Camera.main.transform.position.y));
        _ground.InsertPointAt(3, new Vector3(Camera.main.RightPosition(), _lowerPosition));
    }

    private void AddRightPoint()
    {
        float prevXposition = _ground.GetPosition(_ground.LastPointInd()).x;
        float nextXposition = prevXposition + Random.Range(_minDistanceBetweenPoints, _maxDistanceBetweenPoints);
        float nextYposition = Random.Range(-_heightRange / 2, _heightRange / 2);
        Vector3 nextPosition = new Vector3(nextXposition, nextYposition);

        _ground.InsertPointAt(_ground.LastPointInd(), nextPosition);
        _ground.SetPosition(_ground.LastPointInd(), new Vector3(nextXposition, _lowerPosition));

        SetBrokenTangentMode(_ground.LastPointInd() - 2);
    }

    private void RemoveLeftPoint()
    {
        _ground.RemovePointAt(1);
        _ground.SetPosition(0, new Vector2(_ground.GetPosition(1).x, _lowerPosition));
    }

    private void SetBrokenTangentMode(int index)
    {
        _ground.SetTangentMode(index, ShapeTangentMode.Broken);

        Vector3 tangent = _ground.GetPosition(index + 1) - _ground.GetPosition(index - 1);
        float rightLength = (_ground.GetPosition(index + 1) - _ground.GetPosition(index)).magnitude;
        float leftLength = (_ground.GetPosition(index) - _ground.GetPosition(index - 1)).magnitude;
        float lengthKf = 0.2f;

        _ground.SetLeftTangent(index, -tangent.normalized * leftLength * lengthKf);
        _ground.SetRightTangent(index, tangent.normalized * rightLength * lengthKf);
    }

    private bool CanSpawn()
    {
        return _ground.GetPosition(_ground.GetPointCount() - 2).x - Camera.main.RightPosition() < _spawnDistance;
    }

    private bool CanRemove()
    {
        return Camera.main.LeftPosition() - _ground.GetPosition(2).x > _removeDistance;
    }
}
