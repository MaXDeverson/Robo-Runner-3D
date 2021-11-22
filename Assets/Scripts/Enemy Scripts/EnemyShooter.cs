using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    [SerializeField] private Gun _gun;
    [SerializeField] private EnemyDestroyer _enemyDestroyer;
    [SerializeField] private EnemyAnimator _animator;
    void Start()
    {
        _enemyDestroyer.ActionDie += () => _gun.ShootWait(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tag.Player) && !other.isTrigger)
        { 
            _gun.ShootWait(true);
            _animator.PlayAnimation(AnimationType.Shoot);
        }
    }
}
