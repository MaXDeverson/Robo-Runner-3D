using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : Triggerable
{
    [SerializeField] private Gun _gun;
    [SerializeField] private EnemyDestroyer _enemyDestroyer;
    [SerializeField] private EnemyAnimator _animator;
    [SerializeField] private ShootEventListener _shootEvent;
    [SerializeField] private int _queueLength = 0;
    private bool _isShoot;
    private bool _isDie;

    
    void Start()
    {
        _enemyDestroyer.ActionDie += () => _isDie = true;
        _gun.SetAudioClip(Level.CurrentLevel.SoundList.EnemyShoot);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isShoot) return;
        if (other.CompareTag(Tag.Player) && !other.isTrigger)
        {
            if (_queueLength > 0)
            {
                _animator.PlayAnimation(AnimationType.Shoot);
                _animator.PlayAnimation(AnimationType.Queue);
                _shootEvent.ShootAction += ShootOfQueue;
            }
            else
            {
                _shootEvent.ShootAction += () => {
                    if (!_isDie) _gun.ShootOnce();
                };
                _animator.PlayAnimation(AnimationType.Shoot);
                _isShoot = true;
            }
           
        }
    }
    private void ShootOfQueue()
    {
        if (!_isDie) _gun.ShootOnce();
        if (_queueLength--<0)
        {
            _shootEvent.ShootAction += () => {
                if (!_isDie) _gun.ShootOnce();
            };
            _animator.PlayAnimation(AnimationType.Shoot);
            _shootEvent.ShootAction -= ShootOfQueue;
        }
    }

    public override void OnTrigger(Collider inputCollider, int triggerIndex)
    {
        if(inputCollider.CompareTag(Tag.Player))
        {
            _animator.PlayAnimation(AnimationType.Stay);
        }
    }
}
