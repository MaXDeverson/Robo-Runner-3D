using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    [SerializeField] private float _speedMove = 1;
    [SerializeField] private SnapAxis axis = SnapAxis.X;
    [SerializeField] private float _diapazon = 2.5f;
    private bool _to = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        DirectionTypeMove();
    }

    private void DirectionTypeMove()
    {
        if (_to)
        {
            if (transform.position.x > _diapazon) _to = false;
        }
        else
        {
            if (transform.position.x < -_diapazon) _to = true;
        }
        Vector3 direction = new Vector3(_speedMove, 0, 0) * Time.deltaTime * (_to? 1 : -1);
        transform.Translate(direction);
    }
}
