using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    [SerializeField] private Gun _gun;
    [SerializeField] private EnemyDestroyer _enemyDestroyer;
    [SerializeField] private EnemyAnimator _animator;
    [SerializeField] private ShootEventListener _shootEvent;
    [SerializeField] private bool _shootInStart;
    void Start()
    {
        _enemyDestroyer.ActionDie += () => _gun.ShootWait(false);
        if (_shootInStart)
        {
            _shootEvent.ShootAction += () => _gun.ShootOnce();
            _animator.PlayAnimation(AnimationType.Shoot);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tag.Player) && !other.isTrigger)
        {
            _shootEvent.ShootAction += () => _gun.ShootOnce();
            _animator.PlayAnimation(AnimationType.Shoot);
        }
    }
}
