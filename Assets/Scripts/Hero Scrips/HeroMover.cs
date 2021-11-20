using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMover : MonoBehaviour
{
    [SerializeField] private MoverLogic _moverLogic;
    [SerializeField] private float _velocity;
    [SerializeField] private ManagerAnimation _managerAnimation;
    [SerializeField] private HeroDestroyer _destroyer;
    private Rigidbody _rigidbody;
    private float _previousLogicPosition;
    private const float _moveXRestriction = 2.7F;
    private bool _isDie;
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _destroyer.DieAction += () => _isDie = true;
    }
    void Update()
    {
        if (_isDie) return;
        float newXPosition = transform.position.x + _moverLogic.PositionX;
        if(newXPosition > _moveXRestriction)
        {
            newXPosition = _moveXRestriction;
        }
        else if(newXPosition< -_moveXRestriction)
        {
            newXPosition =- _moveXRestriction;
        }
        transform.position = new Vector3(newXPosition, transform.position.y, transform.position.z);
        _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, _rigidbody.velocity.y, _velocity);
        _managerAnimation.SetMainAnimation(AnimationType.Run, ManagerAnimation.LayerType.MainLayer);
    }
}
