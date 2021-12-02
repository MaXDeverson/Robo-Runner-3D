using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ManagerAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private float _upForce;
    [SerializeField] private ParticleSystem _dieParticles;
    [SerializeField] private ParticleSystem _flyAnimation;
    [SerializeField] private Transform _animatetTransformForCatchBullet;
    private const string MAIN_LAYER_NAME = "MainLayer";
    private const string HAND_LAYER_NAME = "HandLayer";
    private const string SPEED_JUMP_MULTIPLIER = "SpeedJump";
    private Rigidbody _rigidbody;
    private bool _palyDieAnimation;
    private bool _isDie;
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator.SetFloat(SPEED_JUMP_MULTIPLIER, 0.2f);
    }
    void Update()
    {
        
    }

    public async void SetMainAnimation(AnimationType type,LayerType layerType)
    {
        if (_palyDieAnimation) return;
        switch (type)
        {
            case AnimationType.GetDamage:
                for(int i = 0; i < 10; i++)
                {
                    if (i < 5)
                    {
                        _animatetTransformForCatchBullet.Rotate(new Vector3(0, 5, 0));
                    }
                    else
                    {
                        _animatetTransformForCatchBullet.Rotate(new Vector3(0, -5, 0));
                    }
                    await Task.Delay(10);
                }
                transform.eulerAngles = Vector3.zero;
                return;
            case AnimationType.Die:
                _palyDieAnimation = true;
                _dieParticles.Play();
                transform.tag = Tag.Bullet;//For exept bullet animation after die;
                break;
            case AnimationType.GetDamageMine:
                _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0, _rigidbody.velocity.z);
                _rigidbody.AddForce(new Vector3(0, _upForce, 0), ForceMode.Impulse);
                break;
            case AnimationType.Jump:
                StartCoroutine(SlowJumpAnimation());
                break;
            case AnimationType.Run:
                _flyAnimation.Stop();
                break;
        }
        string layerName = layerType.Equals(LayerType.MainLayer) ? MAIN_LAYER_NAME : HAND_LAYER_NAME;
        _animator.SetInteger(layerName, (int)type);
    }

    private IEnumerator SlowJumpAnimation()
    {
        _animator.SetFloat(SPEED_JUMP_MULTIPLIER, 1);
        _flyAnimation.Play();
        yield return new WaitForSeconds(1f);
        _animator.SetFloat(SPEED_JUMP_MULTIPLIER,0.2f);
    }

    public enum LayerType
    {
        MainLayer,
        HandLayer
    }
}
