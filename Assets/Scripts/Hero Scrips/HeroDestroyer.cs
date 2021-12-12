using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroDestroyer : MonoBehaviour
{
    private Action<int> getDamageAction;
    public Action<float> GetDamageActionProcent;
    public Action DieAction;
    [SerializeField] private int _countLifes = 3;
    [SerializeField] private ManagerAnimation _managerAnimation;
    private float _maxCounLefes;
    private bool _isIgnoreDamage;

    public void SetGetDamageAction(Action<int> action,bool invoke)
    {
        getDamageAction += action;
        if(invoke) getDamageAction.Invoke(_countLifes);
    }
    void Start()
    {
        _maxCounLefes = _countLifes;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isIgnoreDamage) return;
        switch (other.tag)
        {
            case Tag.EnemyBullet:
                GetDamage(1,AnimationType.GetDamage);
                break;
            case Tag.Mine:
                GetDamage(1,AnimationType.GetDamageMine);
                break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag(Tag.Bullet))
        {
            Debug.Log("Enter Bullet");
        }
    }

    private void GetDamage(int count,AnimationType typeAnimation)
    {
        _countLifes -= count;
        if (_countLifes >= 0)
        {
            getDamageAction?.Invoke(_countLifes);
            GetDamageActionProcent?.Invoke(_countLifes / _maxCounLefes);
        }
        if (_countLifes > 0)
        {
            _managerAnimation.SetMainAnimation(typeAnimation, ManagerAnimation.LayerType.MainLayer);
        }
        else
        {
            _managerAnimation.SetMainAnimation(typeAnimation, ManagerAnimation.LayerType.MainLayer);
            _managerAnimation.SetMainAnimation(AnimationType.Die, ManagerAnimation.LayerType.MainLayer);
            DieAction?.Invoke();
        }
    }

    public void IgnoreDamage(bool input)
    {
        _isIgnoreDamage = input;
    }
}
