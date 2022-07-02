using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrightnesChanger : MonoBehaviour
{
    [SerializeField] private Material _material;
    [SerializeField] private float _minValue = 0.3f;
    [SerializeField] private float _maxValue = 1.5f;
    [SerializeField] private float _step = 0.1f;
    [SerializeField] private float _waitTime = 0.1f;
    private float _currentValue;

    private void Start()
    {
        _currentValue = _maxValue;
        StartCoroutine(ToMin());
    }
    private IEnumerator ToMin()
    {
        yield return new WaitForSeconds(_waitTime);
        if((_currentValue -= _step) < _minValue)
        {
            StartCoroutine(ToMax());
        }
        else
        {
            StartCoroutine(ToMin());
            _material.SetFloat("_GlowPower", _currentValue);
        }
    }

    private IEnumerator ToMax()
    {
        yield return new WaitForSeconds(_waitTime);
        if((_currentValue += _step) > _maxValue)
        {
            StartCoroutine(ToMin());
        }
        else
        {
            StartCoroutine(ToMax());
            _material.SetFloat("_GlowPower", _currentValue);
        }
    }
}
