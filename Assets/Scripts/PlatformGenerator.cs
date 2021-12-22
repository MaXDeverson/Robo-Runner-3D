using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    [SerializeField] private EnemyDestroyer _destroyer;
    [SerializeField] private Transform _generationObject;
    [SerializeField] private float _deltaZPosition;
    [SerializeField] private Transform _changePositionObj;
    private bool _isDestroy;
    void Start()
    {
        _destroyer.ActionDie += () =>
        {
            _isDestroy = true;
        };
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(Tag.Player) && !_isDestroy)
        {
             Instantiate(_generationObject, new Vector3(transform.position.x, transform.position.y , transform.position.z + _deltaZPosition), Quaternion.identity);
            _changePositionObj.position = new Vector3(_changePositionObj.position.x, _changePositionObj.position.y, _changePositionObj.position.z + _deltaZPosition);
        }
    }
}
