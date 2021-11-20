using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    [SerializeField] private Gun _gun;
    [SerializeField] private EnemyDestroyer enemyDestroyer;
    void Start()
    {
        _gun.ShootWait(true);
        enemyDestroyer.ActionDie += () => _gun.ShootWait(false);
    }
}
