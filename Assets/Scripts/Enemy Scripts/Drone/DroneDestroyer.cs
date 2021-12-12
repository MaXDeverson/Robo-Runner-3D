using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneDestroyer : EnemyDestroyer
{
     protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (!other.CompareTag(Tag.Bullet) && !other.isTrigger)
        {
            ActionDie?.Invoke();
            if (_animator != null) _animator.PlayAnimation(AnimationType.Die);
        }
    }
}
