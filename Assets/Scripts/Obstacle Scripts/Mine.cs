using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    [SerializeField] private ParticleSystem _boomParticle;
    [SerializeField] private GameObject _destroyMineMash;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tag.Player))
        {
            _boomParticle.Play();
            if(_destroyMineMash!= null)Destroy(_destroyMineMash.gameObject);
        }
    }
}
