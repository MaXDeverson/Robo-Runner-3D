using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BossBehavior_1 : Triggerable
{
    [SerializeField] private EnemyAnimator _enamyAnimator;
    [SerializeField] private Vector3 _jumpForce;
    private Rigidbody _rigidbody;
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    public override void OnTrigger(Collider inputCollider, int triggerIndex)
    {
        if (inputCollider.CompareTag(Tag.Player))
        {
            StartCoroutine(Jump());
        }
      
    }
    private IEnumerator Jump()
    {
        _enamyAnimator.PlayAnimation(AnimationType.Jump);
        yield return new WaitForSeconds(0.2f);
        _rigidbody.AddForce(_jumpForce, ForceMode.Impulse);
    }
    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.collider.tag)
        {
            case Tag.Untagged:
                _enamyAnimator.PlayAnimation(AnimationType.Stay);
                break;
        }
    }
}
