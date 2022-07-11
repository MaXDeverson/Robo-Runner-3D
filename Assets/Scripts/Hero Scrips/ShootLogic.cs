using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootLogic : Triggerable
{
    [SerializeField] private Gun _gun;
    [SerializeField] private ManagerAnimation _managerAniamtion;
    [SerializeField] private HeroDestroyer _heroDestroyer;
    [SerializeField] private ShootEventListener _shootEvent;//Event from animation
    [SerializeField] private int _shootTriggerLenght;
    private bool _enemyIsInZone;
    private bool _isDie;
    private bool _isInStopZone;
    void Start()
    {
        _heroDestroyer.DieAction += () => _isDie = true;
        _shootEvent.ShootAction += () => _gun.ShootOnce();
        _gun.SetAudioClip(Level.CurrentLevel.SoundList.HeroShoot);
    }
    private void FixedUpdate()
    {
        if (!_isDie)
        {
            _managerAniamtion.PlayAnimation(_enemyIsInZone && !_isInStopZone ? AnimationType.Shoot : AnimationType.Stay, ManagerAnimation.LayerType.HandLayer);
        }
        else
        {
            _managerAniamtion.PlayAnimation(AnimationType.Stay, ManagerAnimation.LayerType.HandLayer);
        }
        _enemyIsInZone = false;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(Tag.Enemy))
        {
            _enemyIsInZone = true;
        }
    }
    private void OnDrawGizmos()
    {
        //Gizmos.DrawLine(_raycastStart1.position, _raycastStart1.position + new Vector3(0, 0, _shootTriggerLenght));
    }
    public override void OnTrigger(Collider inputCollider, int triggerIndex)
    {
        if (inputCollider.CompareTag(Tag.StopSpot))
        {
            _isInStopZone = true;
            Debug.Log("Enter");
        }
    }
    public override void TriggerExit(Collider exitCollider, int triggerIndex)
    {
        if (exitCollider.CompareTag(Tag.StopSpot))
        {
            Debug.Log("Exit");
            _isInStopZone = false;
        }
        
    }
}
