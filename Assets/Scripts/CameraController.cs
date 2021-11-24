using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _followObj;
    [SerializeField] private Vector3 _deltaPosition;
    private const float _smoothTime = 0.2f;
    private float _forSmooth;
    [Header("For start animation")]

    [SerializeField] private Transform _startTransform;
    private bool _animationWasPlayed;
    private bool _playAnimaiton;

    private float _startRotationX;

    void Start()
    {
        _startRotationX = transform.eulerAngles.x;
        Vector3 deltaDistance = _followObj.position - transform.position;
        float newXRotation = Mathf.Atan(-deltaDistance.y / deltaDistance.z) * 180 / Mathf.PI;
        _startRotationX -= newXRotation;
        //transform.position = _startTransform.position;
       // transform.eulerAngles = _startTransform.eulerAngles;
       // float newYPosition = Mathf.SmoothDamp(transform.position.y, _followObj.position.y + _deltaPosition.y, ref _forSmooth, _smoothTime);
       // transform.position = new Vector3(_deltaPosition.x + (_followObj.position.x / 5), newYPosition, _deltaPosition.z + _followObj.position.z);
       //transform.DOMove(new Vector3(_deltaPosition.x + (_followObj.position.x / 5), newYPosition, _deltaPosition.z + _followObj.position.z),1,false);
        //float angleX = _startRotationX + (newXRotation * 180 / Mathf.PI);
       // transform.DORotate(new Vector3(angleX,0,0),1);
        _animationWasPlayed = true;
    }

    void FixedUpdate()
    {
        if (_animationWasPlayed)
        {
            float newYPosition = Mathf.SmoothDamp(transform.position.y, _followObj.position.y + _deltaPosition.y, ref _forSmooth, _smoothTime);
            transform.position = new Vector3(_deltaPosition.x + (_followObj.position.x / 5), newYPosition, _deltaPosition.z + _followObj.position.z);
            Vector3 deltaDistance = _followObj.position - transform.position;
            float newXRotation = Mathf.Atan(-deltaDistance.y / deltaDistance.z);
            transform.eulerAngles = new Vector3(_startRotationX + (newXRotation * 180 / Mathf.PI), transform.eulerAngles.y, transform.eulerAngles.z);
        }
        else
        {

        }
     
    }

    public void PlayStartAnimation()
    {
        _playAnimaiton = true;
    }
}
