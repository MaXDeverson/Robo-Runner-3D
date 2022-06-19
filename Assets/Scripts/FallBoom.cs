
using UnityEngine;

public class FallBoom : MonoBehaviour
{
    [SerializeField] private ParticleSystem _fallParticlesEffect;
    [SerializeField] private AudioSource _audio;
    private Rigidbody _rigidbody;
    private bool _canAnimated;
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        _canAnimated = _rigidbody.velocity.y <= -7;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Collider other = collision.collider;
        if (other.CompareTag(Tag.Untagged) && !other.isTrigger && _canAnimated)
        {
            ParticleSystem newEffect = Instantiate(_fallParticlesEffect, transform.position, Quaternion.identity);
            newEffect.transform.parent = null;
            newEffect.Play();
            _audio.PlayOneShot(Level.CurrentLevel.SoundList.HeroFall);
            Destroy(newEffect, 10);
            _canAnimated = false;
        }
    }
}
