
using UnityEngine;

public class FallBoom : MonoBehaviour
{
    [SerializeField] private ParticleSystem _fallParticlesEffect;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tag.Untagged) && !other.isTrigger)
        {
            ParticleSystem newEffect = Instantiate(_fallParticlesEffect, transform.position, Quaternion.identity);
            newEffect.transform.parent = null;
            newEffect.Play();
            Destroy(newEffect, 10);
        }
    }
}
