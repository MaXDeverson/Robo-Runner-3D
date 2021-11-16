using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootLogic : MonoBehaviour
{
    [SerializeField] private Gun _gun;
    [SerializeField] private ManagerAnimation _managerAniamtion;
    [SerializeField] private bool _shoot;
    void Start()
    {

    }

    private void Update()
    {

        _gun.ShootWait(_shoot);
        _managerAniamtion.SetMainAnimation(_shoot ? AnimationType.Shoot : AnimationType.Stay, ManagerAnimation.LayerType.HandLayer);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tag.Enemy))
        {

        }
    }
}
