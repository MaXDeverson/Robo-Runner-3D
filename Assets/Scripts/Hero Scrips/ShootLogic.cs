using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootLogic : MonoBehaviour
{
    [SerializeField] private Gun _gun;
    [SerializeField] private ManagerAnimation _managerAniamtion;
    [SerializeField] private HeroDestroyer _heroDestroyer;
    [SerializeField] private ShootEventListener _shootEvent;
    private bool _enemyIsInZone;
    private bool _isDie;
    void Start()
    {
        _heroDestroyer.DieAction += () => _isDie = true;
        _shootEvent.ShootAction += () => _gun.ShootOnce();
    }

    private void Update()
    {
        if (!_isDie)
        {
            _managerAniamtion.SetMainAnimation(_enemyIsInZone ? AnimationType.Shoot : AnimationType.Stay, ManagerAnimation.LayerType.HandLayer);
        }
        else
        {
            _managerAniamtion.SetMainAnimation(AnimationType.Stay, ManagerAnimation.LayerType.HandLayer);
        }
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
