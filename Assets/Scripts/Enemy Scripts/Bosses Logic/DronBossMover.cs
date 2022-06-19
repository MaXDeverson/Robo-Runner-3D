using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using DG.Tweening.Plugins.Core.PathCore;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;

public class DronBossMover : Triggerable
{
    [SerializeField] private List<Transform> _introTraectory;
    [SerializeField] private Transform _aroundPoint;
    [SerializeField] private Transform _correctPosition;
    private Transform _player;
    TweenerCore<Vector3, Path, PathOptions> tween;
    private bool _startFirstMode;
    private bool _startSecondMode;
    private bool _startThirdMode;
    void Start()
    {
        Vector3[] vectorsPath = new Vector3[_introTraectory.Count];
        for (int i = 0; i < _introTraectory.Count; i++)
            vectorsPath[i] = _introTraectory[i].position;
        Path path = new Path(PathType.CatmullRom, vectorsPath, 1);
        tween = transform.DOPath(path, 10);
        _player = Level.CurrentLevel.Hero;
        StartCoroutine(SecondMode());
    }
    void Update()
    {
        if (_startFirstMode)
        {
            Vector3 deltaDistance = _aroundPoint.position - transform.position;
            float newYRotation = Mathf.Atan(deltaDistance.x / deltaDistance.z);
            transform.eulerAngles = new Vector3(0, (newYRotation * 180 / Mathf.PI) + (deltaDistance.z >= 0 ? 0 : 180), transform.eulerAngles.z);
        }
        if (_startSecondMode)
        {
            Vector3 deltaDistance = _player.position - transform.position;
            float newYRotation = Mathf.Atan(deltaDistance.x / deltaDistance.z);
            float newXRotation = Mathf.Atan(deltaDistance.y / deltaDistance.z);
            transform.DORotate(new Vector3((newXRotation * 180 / Mathf.PI), (newYRotation * 180 / Mathf.PI) + (deltaDistance.z >= 0 ? 0 : 180), transform.eulerAngles.z), 1f);
        }
    }

    private IEnumerator SecondMode()
    {
        _startFirstMode = true;
        yield return new WaitForSeconds(7);
        _startFirstMode = false;
        _startSecondMode = true;
        //Vector3 deltaDistance = _correctPosition.position - _aroundPoint.position;
        //float newXRotation = Mathf.Atan(deltaDistance.y / deltaDistance.z);
        //float newYRotation = Mathf.Atan(deltaDistance.x / deltaDistance.z);
        //transform.DORotate(new Vector3((newXRotation * 180 / Mathf.PI), (newYRotation * 180 / Mathf.PI) + (deltaDistance.z > 0 ? 0 : 180), transform.eulerAngles.z), 3f);
        //Debug.Log("Second");
    }

    public override void OnTrigger(Collider inputCollider, int triggerIndex)
    {
        if (inputCollider.CompareTag(Tag.Player))
        {
            GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 15);
            Debug.Log("on trigger player");
        }
    }
}
