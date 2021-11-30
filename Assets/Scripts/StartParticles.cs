using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartParticles : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particles;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tag.Player))
        {
            _particles.transform.position = new Vector3(other.transform.position.x, _particles.transform.position.y, other.transform.position.z);
            _particles.Play();
        }
    }
}
