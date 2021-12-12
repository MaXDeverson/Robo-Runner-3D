using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyRandom = System.Random;

public class DroneShooter : Triggerable
{
    [SerializeField] private Gun _gun;
    private bool _canShoot;
    private bool _isShoot;
    private MyRandom _random;

    private void Awake()
    {
        _random = new MyRandom();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isShoot && _canShoot)
        {
            _isShoot = true;
            StartCoroutine(Shoot());
        }
    }

    private IEnumerator Shoot()
    {
        yield return new WaitForSeconds(_random.Next(1, 3 + Mathf.Abs( (int)transform.position.z) % 4));
        _gun.ShootOnce();
        _isShoot = false;
    }

    public override void OnTrigger(Collider inputCollider)
    {
        _canShoot = true;
    }
}
