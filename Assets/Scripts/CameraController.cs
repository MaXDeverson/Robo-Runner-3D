using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _followObj;
    private Vector3 _deltaPosition;
    private const float _smoothTime = 0.2f;
    private float _forSmooth;

    void Start()
    {
        _deltaPosition = transform.position - _followObj.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float newYPosition = Mathf.SmoothDamp(transform.position.y,_followObj.position.y + _deltaPosition.y, ref _forSmooth, _smoothTime);
        transform.position = new Vector3(_deltaPosition.x+ (_followObj.position.x / 5),newYPosition, _deltaPosition.z + _followObj.position.z);
    }
}
