using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyDestroyer : MonoBehaviour
{
    public UnityAction<int> ActionGetDamage;
    public UnityAction ActionDie;
    [SerializeField] private int _countLifes;
    [SerializeField] protected EnemyAnimator _animator;
    // Update is called once per frame
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tag.Bullet))
        {
            if (!other.GetComponent<Bullet>().IsEnemy())
            {
                if (_countLifes > 0)
                {
                    _countLifes--;
                    ActionGetDamage?.Invoke(_countLifes);
                    if (_animator != null) _animator.PlayAnimation(AnimationType.GetDamage);
                    if (_countLifes == 0)
                    {
                        if (_animator != null) _animator.PlayAnimation(AnimationType.Die);
                        ActionDie?.Invoke();
                    }
                }
            }
        }
    }
}
