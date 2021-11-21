using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroDestroyer : MonoBehaviour
{
    public Action<int> GetDamageAction;
    public Action<float> GetDamageActionProcent;
    public Action DieAction;
    [SerializeField] private int _countLifes;
    [SerializeField] private ManagerAnimation _managerAnimation;
    private float _maxCounLefes;
    void Start()
    {
        _maxCounLefes = _countLifes;
    }

    private void OnTriggerEnter(Collider other)
    {
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

    private void GetDamage(int count,AnimationType typeAnimation)
    {
        _countLifes -= count;
        if (_countLifes >= 0)
        {
            GetDamageAction?.Invoke(_countLifes);
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
}
