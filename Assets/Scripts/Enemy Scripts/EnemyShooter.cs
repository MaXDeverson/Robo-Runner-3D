using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    [SerializeField] private Gun _gun;
    [SerializeField] private EnemyDestroyer _enemyDestroyer;
    [SerializeField] private EnemyAnimator _animator;
    [SerializeField] private ShootEventListener _shootEvent;
    private bool _isShoot;
    private bool _isDie;
    void Start()
    {
        _enemyDestroyer.ActionDie += () => _isDie = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isShoot) return;
        if (other.CompareTag(Tag.Player) && !other.isTrigger)
        {
            _shootEvent.ShootAction += () => {
               if(!_isDie)_gun.ShootOnce();
            };
            _animator.PlayAnimation(AnimationType.Shoot);
            _isShoot = true;
        }
        
    }
}
