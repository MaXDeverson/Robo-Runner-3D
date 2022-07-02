using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraBuy : MonoBehaviour
{
    [SerializeField] private Vector3 _delta;
    [SerializeField] private float _smoothValue;
    private Transform _target;
    private Vector3 _rotateAnimationPosition = new Vector3(0.6f, -0.2f, 0.3f);
    private Vector3 _rotateAnimationAngle = new Vector3(5, 9, 0);
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SetTarget(Transform target,bool withAnimation = true)
    {
        _target = target;
        if (withAnimation) transform.DOMove(target.position + _delta, 0.5f, false);
        else transform.position = target.position + _delta;
    }

    public void AnimatePositionNear()
    {
        _delta -= _delta.normalized;
        transform.DOMove(_target.position + _delta, 0.5f, false);
    }
    public void AnimateRotate()
    {
        transform.DOMove(transform.position + _rotateAnimationPosition,0.5f, false);
        transform.DORotate(transform.eulerAngles - _rotateAnimationAngle,0.5f);
    }
    public void AnimateRotateOut()
    {
        transform.DOMove(transform.position - _rotateAnimationPosition, 0.5f, false);
        transform.DORotate(transform.eulerAngles + _rotateAnimationAngle, 0.5f);
    }
    public void AnimatePositionFar()
    {
        _delta +=_delta.normalized;
        transform.DOMove(_target.position + _delta, 0.5f, false);
    }
}
