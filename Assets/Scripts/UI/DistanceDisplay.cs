using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class DistanceDisplay : MonoBehaviour
{
    [SerializeField] private Transform _player;

    private Text _distance;

    private void Awake()
    {
        _distance = GetComponent<Text>();
    }

    private void LateUpdate()
    {
        _distance.text = string.Format("{0:#} м", _player.transform.position.x);
    }
}
