using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _followObj;
    [SerializeField] private Vector3 _deltaPosition;
    private const float _smoothTime = 0.2f;
    private float _forSmooth;

    private float _startRotationX;

    void Start()
    {
        _startRotationX = transform.eulerAngles.x;
        Vector3 deltaDistance = _followObj.position - transform.position;
        float newXRotation = Mathf.Atan(-deltaDistance.y / deltaDistance.z) * 180 / Mathf.PI;
        _startRotationX -= newXRotation;
       // _startRotation.x = 
        //transform.eulerAngles = new Vector3((newXRotation * 180 / Mathf.PI) - _startRotation.x, transform.eulerAngles.y, transform.eulerAngles.z);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float newYPosition = Mathf.SmoothDamp(transform.position.y,_followObj.position.y + _deltaPosition.y, ref _forSmooth, _smoothTime);
        transform.position = new Vector3(_deltaPosition.x+ (_followObj.position.x / 5),newYPosition, _deltaPosition.z + _followObj.position.z);
        //rotation
        Vector3 deltaDistance = _followObj.position - transform.position;
        float newXRotation = Mathf.Atan(-deltaDistance.y / deltaDistance.z);
        transform.eulerAngles = new Vector3(_startRotationX + (newXRotation * 180 / Mathf.PI), transform.eulerAngles.y, transform.eulerAngles.z);
    }
}
