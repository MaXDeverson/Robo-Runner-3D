using System.Threading.Tasks;
using UnityEngine;

public class HeroMover : Triggerable
{
    [SerializeField] private MoverLogic _moverLogic;
    [SerializeField] private float _velocity;
    [SerializeField] private ManagerAnimation _managerAnimation;
    [SerializeField] private HeroDestroyer _destroyer;
    private Rigidbody _rigidbody;
    private const float _moveXRestriction = 2.7F;
    private bool _isDie;
    //for jump animation;
    private bool _isJump;
    private bool _canAbortJump;
    //for start animation;
    private bool _startAnimationWillPlayed;
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _destroyer.DieAction += () => _isDie = true;
        _managerAnimation.SetMainAnimation(AnimationType.Jump, ManagerAnimation.LayerType.MainLayer);
    }
    void Update()
    {
        if (_isDie) return;
        //    float newXPosition = transform.position.x + _moverLogic.PositionX;
        //    if (newXPosition > _moveXRestriction)
        //    {
        //        newXPosition = _moveXRestriction;
        //    }
        //    else if (newXPosition < -_moveXRestriction)
        //    {
        //        newXPosition = -_moveXRestriction;
        //    }
        //    transform.position = new Vector3(newXPosition, transform.position.y, transform.position.z);

        float xVelocity = _moverLogic.PositionX * 50;
            if (transform.position.x > _moveXRestriction)
            {
                transform.position = new Vector3(_moveXRestriction, transform.position.y, transform.position.z);
                xVelocity = 0;
            }
            if (transform.position.x < -_moveXRestriction)
            {
                transform.position = new Vector3(-_moveXRestriction, transform.position.y, transform.position.z);
                xVelocity = 0;
            }
        _rigidbody.velocity = new Vector3(xVelocity, _rigidbody.velocity.y, _velocity);
        if (!_isJump && _startAnimationWillPlayed)
        {
            _managerAnimation.SetMainAnimation(AnimationType.Run, ManagerAnimation.LayerType.MainLayer);
        }
    }

    public float GetZVelocity() => _velocity;

    private void OnCollisionStay(Collision collision)
    {
        if (_canAbortJump)
        {
            _isJump = false;
            _canAbortJump = false;
        }
        _startAnimationWillPlayed = true;
    }
    public async override void OnTrigger(Collider inputCollider)
    {
        if (inputCollider.CompareTag(Tag.Jump))
        {
            _isJump = true;
        _managerAnimation.SetMainAnimation(AnimationType.Jump, ManagerAnimation.LayerType.MainLayer);
        await Task.Delay(500);
        _canAbortJump = true;
        }
    }
}
