using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private float _speed;
    [Header("Y=0,X = 1, Z = 2")]
    [SerializeField] private int _axis;
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (_axis)
        {
            case 0: transform.Rotate(new Vector3(0, _speed, 0));
                break;
            case 1: transform.Rotate(new Vector3(_speed,0, 0));
                break;
            case 2: transform.Rotate(new Vector3(0,0,_speed));
                break;
        }
 
    }
}
