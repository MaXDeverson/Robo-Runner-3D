using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private EnemyMoverLogic _moverLogic;
    [SerializeField] private EnemyAnimator _animator;
    [SerializeField] private EnemyDestroyer _destroyer;
    [SerializeField] private float _speed = 1;
    private Rigidbody _rigidbody;
    private bool CanAnimated;
    private bool Pause;
    private bool _isDIe;
    private const float NoAnimatedDistance = 0.2f; 
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _destroyer.ActionDie += () =>
        {
            _isDIe = true;
        };
    }

    // Update is called once per frame
    private void Update()
    {
        if (_animator.CurrentAnimation.Equals(AnimationType.Shoot) && !Pause)
        {
            Pause = true;
            PlayMoveAnimation();
        }
        if (CanAnimated && !_isDIe)
        {
            if (_moverLogic.PositionX > transform.position.x + NoAnimatedDistance)
            {
                _animator.PlayAnimation(AnimationType.MoveRight);
                _rigidbody.velocity = new Vector3(_speed, _rigidbody.velocity.y,_rigidbody.velocity.z);
            }
            else if (_moverLogic.PositionX < transform.position.x - NoAnimatedDistance)
            {
                _animator.PlayAnimation(AnimationType.MoveLeft);
                _rigidbody.velocity = new Vector3(-_speed, _rigidbody.velocity.y, _rigidbody.velocity.z);
            }
            else
            {
                _animator.PlayAnimation(AnimationType.Stay);
                _rigidbody.velocity = new Vector3(0,_rigidbody.velocity.y, _rigidbody.velocity.z);
            }
        }
       // transform.position = new Vector3(_moverLogic.PositionX, transform.position.y, transform.position.z);
    }

    private async void PlayMoveAnimation()
    {
        await Task.Delay(1000);
        CanAnimated = true;
    }
}
