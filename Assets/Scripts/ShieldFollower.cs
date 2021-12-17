using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldFollower : MonoBehaviour
{
    [SerializeField] private Transform _followObj;
    private Vector3 _delta = new Vector3(0, -0.5f, 0);

    // Update is called once per frame
    private void FixedUpdate()
    {
        transform.position = _followObj.position + _delta;
    }

}
