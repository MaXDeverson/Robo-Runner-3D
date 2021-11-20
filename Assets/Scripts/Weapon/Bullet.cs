
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private ParticleSystem _finishParticles;
    [SerializeField] private ParticleSystem _mainParticles;
    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger)
            return;
            _finishParticles.Play();
            _mainParticles.Stop();
            _rigidbody.velocity = Vector3.zero;
            Destroy(gameObject,0.2f);

    }
}