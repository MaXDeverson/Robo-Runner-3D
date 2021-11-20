using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private float _pushForce;
    [Header("For Die")]
    [SerializeField] private ParticleSystem _boomParticles;
    [SerializeField] private GameObject _destroyObj;
    private Rigidbody _rigidbody;
    private const string MAIN_LAYER_NAME = "MainLayer";
    private bool _isDie;
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }


    public async  void PlayAnimation(AnimationType animation)
    {
        if (_isDie)
        {
            return;
        }
        switch (animation)
        {
            case AnimationType.Die:
                _isDie = true;
                _animator.SetInteger(MAIN_LAYER_NAME,(int)animation);
                _rigidbody.constraints = RigidbodyConstraints.None;
                System.Random random = new System.Random();
                _rigidbody.AddForce(new Vector3(random.Next((int)_pushForce,(int)-_pushForce), _pushForce, -_pushForce * 3), ForceMode.Impulse);
                 await Task.Delay(1100);
                _boomParticles.transform.SetParent(null);
                Destroy(_destroyObj, 0.1f);
                _boomParticles.Play();
                break;
            case AnimationType.GetDamage:
                _rigidbody.AddForce(new Vector3(0, 0, -_pushForce), ForceMode.Impulse);
                 await Task.Delay(200);
                if (!_isDie)
                {
                    _rigidbody.velocity = Vector3.zero;
                }
                break;
        }
    }
}
