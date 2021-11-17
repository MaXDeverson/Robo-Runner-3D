
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private ParticleSystem _finishParticles;
    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger)
            return;
        if (other.CompareTag(Tag.Enemy) || other.CompareTag(Tag.Untagged))
        {
            _finishParticles.Play();
            Destroy(gameObject,0.2f);
        }

    }
}