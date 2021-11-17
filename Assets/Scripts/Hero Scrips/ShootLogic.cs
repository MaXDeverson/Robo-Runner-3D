using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootLogic : MonoBehaviour
{
    [SerializeField] private Gun _gun;
    [SerializeField] private ManagerAnimation _managerAniamtion;
    private bool _enemyIsInZone;
    void Start()
    {

    }

    private void Update()
    {
        _gun.ShootWait(_enemyIsInZone);
        _managerAniamtion.SetMainAnimation(_enemyIsInZone ? AnimationType.Shoot : AnimationType.Stay, ManagerAnimation.LayerType.HandLayer);
        _enemyIsInZone = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(Tag.Enemy))
        {
            _enemyIsInZone = true;
        }
    }
}
