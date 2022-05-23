using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    [SerializeField] private ParticleSystem _boomParticle;
    [SerializeField] private GameObject _destroyMineMash;
    private BoxCollider _collider;
    private void Start()
    {
        _collider = GetComponent<BoxCollider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tag.Player) || other.CompareTag(Tag.Bullet))
        {
            _boomParticle.Play();
            _collider.enabled = false;
            if (_destroyMineMash != null) Destroy(_destroyMineMash.gameObject);
        }
        //if(!other.CompareTag(Tag.Player)) this.tag = Tag.Untagged;

    }
}
