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

    private void Start()
    {
        _heroDamage = Level.CurrentLevel == null?0:Level.CurrentLevel.HeroData.DamageCount;
        _startCountLifes = _countLifes;
    }
    // Update is called once per frame
    protected virtual void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case Tag.Bullet:
                if (!other.GetComponent<Bullet>().IsEnemy())
                {
                    if (_countLifes > 0)
                    {
                        _countLifes -= _heroDamage;
                        ActionGetDamage?.Invoke(_countLifes, _countLifes / _startCountLifes);
                        if (_animator != null) _animator.PlayAnimation(AnimationType.GetDamage);
                        if (_countLifes <= 0)
                        {
                            //Die is here
                            GetComponent<BoxCollider>().enabled = false;
                            if (_animator != null) _animator.PlayAnimation(AnimationType.Die);
                            ActionDie?.Invoke();
                        }
                    }
                }
                break;
            case Tag.Player:
                if (_animator != null) _animator.PlayAnimation(AnimationType.Die);
                ActionDie?.Invoke();
                break;
        }
    }
}
