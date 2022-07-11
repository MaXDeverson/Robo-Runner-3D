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
    private bool _isDie;

    private void Start()
    {
        DieAction += () =>
        {
            _isDie = true;
            GetComponent<Collider>().material = null;
        };
    }
    public void SetGetDamageAction(Action<int> action, bool invoke = false)
    {
        getDamageAction += action;
        if (invoke) getDamageAction.Invoke(_countLifes);
    }
    public void SetGetDamageActionProcent(Action<float> action, bool invoke = false)
    {
        GetDamageActionProcent += action;
        if (invoke) GetDamageActionProcent.Invoke(_countLifes / _maxCounLefes);
    }
    public void SetCountLifes(int count)
    {
        _countLifes = count;
        _maxCounLefes = count;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Eneter " + other.tag);
        if (other.CompareTag(Tag.Dead))
        {
            GetDamage(50, AnimationType.Die);
        }
        if (_isIgnoreDamage) return;
        switch (other.tag)
        {
            case Tag.EnemyBullet:
                GetDamage(1, AnimationType.GetDamage);
                break;
            case Tag.Mine:
                GetDamage(1, AnimationType.GetDamageMine);
                break;
            case Tag.Barrel:
                GetDamage(5, AnimationType.GetDamage);
                break;
        }
    }
    private void GetDamage(int count, AnimationType typeAnimation)
    {

        _countLifes -= count;
        if (!_isDie)
        {
            getDamageAction?.Invoke(_countLifes);
            GetDamageActionProcent?.Invoke(_countLifes / _maxCounLefes);
        }
        if (_countLifes > 0)
        {
            _managerAnimation.PlayAnimation(typeAnimation, ManagerAnimation.LayerType.MainLayer);
        }
        else if(!_isDie)
        {
            //_managerAnimation.PlayAnimation(typeAnimation, ManagerAnimation.LayerType.MainLayer);
            _managerAnimation.PlayAnimation(AnimationType.Die, ManagerAnimation.LayerType.MainLayer);
            DieAction?.Invoke();
        }
    }
    public void IgnoreDamage(bool input)
    {
        _isIgnoreDamage = input;
    }

}
