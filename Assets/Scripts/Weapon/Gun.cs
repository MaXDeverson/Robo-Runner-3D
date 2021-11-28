using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Gun : Weapon
{
    [SerializeField] private Rigidbody _bullet;
    [SerializeField] private ParticleSystem _muzzleFlash;
    [SerializeField] private float _rateFire;
    [SerializeField] private float _bulletVelocity;

    [SerializeField] private float _bulletLifeTime;

    //for direction shoot;
    [SerializeField] private Transform _muzzlePoint;
    [SerializeField] private Transform _backPoint;
    //Sound
    [SerializeField] private AudioSource _audio;
    private bool isActive;
    private bool isShoot;


    private void FixedUpdate()
    {
        if (isActive && !isShoot)
        {
            _audio.Play();
            StartCoroutine(WaitShoot());
        }
    }

    private IEnumerator WaitShoot()
    {
        isShoot = true;
        _muzzleFlash.Play();
        Rigidbody newBullet = Instantiate(_bullet, _backPoint.position, transform.rotation);
        Vector3 directionShoot = _muzzlePoint.position - _backPoint.position;
        //newBullet.velocity = new Vector3(0, 0, _bulletVelocity);
        newBullet.velocity = directionShoot.normalized * _bulletVelocity;
        Destroy(newBullet.gameObject, _bulletLifeTime);
        yield return new WaitForSeconds(_rateFire);
        isShoot = false;
    }

    /// <summary>
    /// true: weapon shoot
    /// falce: weapon stop shoot;
    /// </summary>
    /// <param name="shoot"></param>
    public override void ShootWait(bool shoot)
    {
        isActive = shoot;
    }
    public override void ShootOnce()
    {
        _muzzleFlash.Play();
        Rigidbody newBullet = Instantiate(_bullet, _backPoint.position, transform.rotation);
        Vector3 directionShoot = _muzzlePoint.position - _backPoint.position;
        newBullet.velocity = directionShoot.normalized * _bulletVelocity;
        Destroy(newBullet.gameObject, _bulletLifeTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(_backPoint.position, 0.05f);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(_muzzlePoint.position, 0.05f);
        Gizmos.color = Color.cyan;
    }
}