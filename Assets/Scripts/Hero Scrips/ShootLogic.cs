using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootLogic : MonoBehaviour
{
    [SerializeField] private Gun _gun;
    [SerializeField] private ManagerAnimation _managerAniamtion;
    [SerializeField] private HeroDestroyer _heroDestroyer;
    [SerializeField] private ShootEventListener _shootEvent;//Event from animation
    [SerializeField] private Transform _raycastStart1;
    [SerializeField] private Transform _raycastStart2;
    [SerializeField] private int _shootTriggerLenght;
    private bool _enemyIsInZone;
    private bool _isDie;
    void Start()
    {
        
        _heroDestroyer.DieAction += () => _isDie = true;
        _shootEvent.ShootAction += () => _gun.ShootOnce();
    }

    private void Update()
    {
        //RaycastHit hit;
        //if(Physics.Raycast(_raycastStart1.position,new Vector3(0,0,1), out hit, _shootTriggerLenght))
        //{
        //    Debug.Log(hit.collider.tag);
        //    if (hit.collider.CompareTag(Tag.Enemy))
        //    {
                
        //    }
        //}
        if (!_isDie)
        {
            _managerAniamtion.SetMainAnimation(_enemyIsInZone ? AnimationType.Shoot : AnimationType.Stay, ManagerAnimation.LayerType.HandLayer);
        }
        else
        {
            _managerAniamtion.SetMainAnimation(AnimationType.Stay, ManagerAnimation.LayerType.HandLayer);
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
}
