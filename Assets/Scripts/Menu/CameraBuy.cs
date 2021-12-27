using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraBuy : MonoBehaviour
{
    [SerializeField] private Vector3 _delta;
    [SerializeField] private float _smoothValue;
    private Transform _target;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SetTarget(Transform target)
    {
        _target = target;
        transform.DOMove(target.position + _delta, 0.5f,false);
    }

    public void AnimatePositionNear()
    {
        _delta -= _delta.normalized;
        transform.DOMove(_target.position + _delta, 0.5f, false);
    }

    public void AnimatePositionFar()
    {
        _delta +=_delta.normalized;
        transform.DOMove(_target.position + _delta, 0.5f, false);
    }
}
