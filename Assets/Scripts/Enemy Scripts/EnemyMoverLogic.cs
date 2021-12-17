using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoverLogic : MonoBehaviour
{
    public float PositionX { get; private set; }
    [SerializeField] private Transform _aim;
    [SerializeField] private float _speed;
    void Start()
    {
        _aim = Level.CurrentLevel.Hero;
    }
    void Update()
    {
        PositionX = Mathf.Lerp(transform.position.x,_aim.position.x,_speed);
    }
}
