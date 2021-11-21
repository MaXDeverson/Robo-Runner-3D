
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private ParticleSystem _finishParticles;
    [SerializeField] private ParticleSystem _mainParticles;
    //[SerializeField] private GameObject _destroyObj;
    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    private void OnTriggerEnter(Collider other)
    {
        gameObject.GetComponent<Collider>().isTrigger = true;
        if (!other.isTrigger && !other.CompareTag(Tag.Bullet))
        {

            //if(_destroyObj!=null) Destroy(_destroyObj);
            if (_mainParticles != null)
            {
                _mainParticles.Stop();
            }
                _finishParticles.Play();
                _rigidbody.velocity = Vector3.zero;
                _finishParticles.transform.parent = null;
                Destroy(_finishParticles, 0.5f);
            Destroy(this.gameObject);

        }
    }
}