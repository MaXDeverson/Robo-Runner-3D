using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyDestroyer : MonoBehaviour
{
    public UnityAction<int> ActionGetDamage;
    public UnityAction ActionDie;
    [SerializeField] private int _countLifes;
    [SerializeField] private EnemyAnimator _animator;
    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tag.Bullet))
        {
         
            if (_countLifes > 0)
            {
                _countLifes--;
                ActionGetDamage?.Invoke(_countLifes);
                _animator.PlayAnimation(AnimationType.GetDamage);
            }
            else
            {
                _animator.PlayAnimation(AnimationType.Die);
                ActionDie?.Invoke();
            }
        }
    }
}
