
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private ParticleSystem _finishParticles;
    [SerializeField] private ParticleSystem _mainParticles;
    [SerializeField] private bool _isEnemy;
    //[SerializeField] private GameObject _destroyObj;
    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    public bool IsEnemy() => _isEnemy;
    private void OnTriggerEnter(Collider other)
    {
        if ((!other.isTrigger && !other.CompareTag(Tag.Bullet)) || other.CompareTag(Tag.Barrel))
        {
            //if(_destroyObj!=null) Destroy(_destroyObj);
            if (_mainParticles != null)
            {
                _mainParticles.Stop();
            }
            if (_rigidbody != null)
            {
                _finishParticles.Play();
                _rigidbody.velocity = Vector3.zero;
                Destroy(_finishParticles, 0.5f);
                _finishParticles.transform.parent = null;
                Destroy(this.gameObject);
            }

        }
    }
}