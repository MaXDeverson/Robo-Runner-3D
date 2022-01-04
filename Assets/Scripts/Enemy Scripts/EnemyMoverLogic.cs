using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoverLogic : MonoBehaviour
{
    public float PositionX { get; private set; }
    [SerializeField] private Transform _aim;
    [SerializeField] private float _speed;
    [SerializeField] private float _shiftX;
    void Start()
    {
        _aim = Level.CurrentLevel.Hero;
    }
    void Update()
    {
        PositionX = _aim.position.x + _shiftX;
    }
}
