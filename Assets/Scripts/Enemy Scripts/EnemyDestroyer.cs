using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyDestroyer : MonoBehaviour
{
    public UnityAction<int,float> ActionGetDamage;
    public UnityAction ActionDie;
    [SerializeField] private int _countLifes;
    [SerializeField] protected EnemyAnimator _animator;
    private float _startCountLifes;
    private int _heroDamage;

    private void Awake()
    {
        _heroDamage = Level.CurrentLevel.HeroData.DamageCount;
    }

    private void Start()
    {
        _startCountLifes = _countLifes;
    }
    // Update is called once per frame
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tag.Bullet))
        {
            if (!other.GetComponent<Bullet>().IsEnemy())
            {
                if (_countLifes > 0)
                {
                    _countLifes-=_heroDamage;
                    ActionGetDamage?.Invoke(_countLifes,_countLifes / _startCountLifes);
                    if (_animator != null) _animator.PlayAnimation(AnimationType.GetDamage);
                    if (_countLifes <= 0)
                    {
                        if (_animator != null)_animator.PlayAnimation(AnimationType.Die);
                        ActionDie?.Invoke();
                    }
                }
            }
        }
    }
}
