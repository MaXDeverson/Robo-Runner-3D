using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    [SerializeField] protected Animator _animator;
    [SerializeField] protected float _pushForce;
    [Header("For Die")]
    [SerializeField] protected ParticleSystem _boomParticles;
    [SerializeField] protected GameObject _destroyObj;
    public AnimationType CurrentAnimation { get; protected set; }
    protected Rigidbody _rigidbody;
    protected const string MAIN_LAYER_NAME = "MainLayer";
    protected bool _isDie;
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }


    public virtual async void PlayAnimation(AnimationType animation)
    {
        if (_isDie)
        {
            return;
        }
        CurrentAnimation = animation;
        //Specific animation;
        switch (animation)
        {
            case AnimationType.Die:
                _isDie = true;
                _animator.SetInteger(MAIN_LAYER_NAME,(int)animation);
                _rigidbody.constraints = RigidbodyConstraints.None;
                System.Random random = new System.Random();
                _rigidbody.AddForce(new Vector3(random.Next((int)_pushForce,(int)-_pushForce), _pushForce, -_pushForce * 3), ForceMode.Impulse);
                 await Task.Delay(1100);
                if (Application.isPlaying)
                {
                    _boomParticles.transform.parent = null;
                    Destroy(_destroyObj, 0.1f);
                    _boomParticles.Play();
                }
                return;
            case AnimationType.GetDamage:
                _rigidbody.AddForce(new Vector3(0, 0, -_pushForce), ForceMode.Impulse);
                 await Task.Delay(200);
                if (!_isDie)
                {
                    _rigidbody.velocity = Vector3.zero;
                }
                return;
        }
        _animator.SetInteger(MAIN_LAYER_NAME, (int)animation);
    }

}
