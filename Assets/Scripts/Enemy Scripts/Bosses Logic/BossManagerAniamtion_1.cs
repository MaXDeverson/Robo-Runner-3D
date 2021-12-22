using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManagerAniamtion_1 : EnemyAnimator
{
    [SerializeField] private ParticleSystem _flyAnimation;
    private const string SPEED_JUMP_MULTIPLIER = "SpeedJump";
    private bool _isJump;
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator.SetFloat(SPEED_JUMP_MULTIPLIER, 1f);
        _flyAnimation.Stop();
    }
    public override void PlayAnimation(AnimationType animation)
    {
        if (!_isJump || animation == AnimationType.Die)
        {
            base.PlayAnimation(animation);
            _flyAnimation.Stop();
        }
        if(animation == AnimationType.Jump && !_isJump)
        {
            _isJump = true;
            StartCoroutine(SlowJumpAnimation());

        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag(Tag.Untagged))
        {
            _isJump = false;
        }
    }
    private IEnumerator SlowJumpAnimation()
    {
        _animator.SetFloat(SPEED_JUMP_MULTIPLIER, 1f);
        if(_flyAnimation != null) _flyAnimation.Play();
        yield return new WaitForSeconds(0.4f);
        _animator.SetFloat(SPEED_JUMP_MULTIPLIER, 0.5f);
    }
}
