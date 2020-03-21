using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class CoinDisplay : MonoBehaviour
{
    [SerializeField] private CoinCollector _collector;
    [SerializeField] private Text _coins;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _collector.OnCoinCollected += UpdateCoins;
    }

    private void OnDisable()
    {
        _collector.OnCoinCollected -= UpdateCoins;
    }

    private void UpdateCoins(int value)
    {
        _animator.Play("Collected");
        _coins.text = value.ToString();
    }
}
