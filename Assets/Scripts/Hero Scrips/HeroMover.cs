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
    private GameObject _jumpGameObject;
    //for start animation;
    private bool _startAnimationWillPlayed;
    void Start()
    {
        _jumpGameObject = gameObject;
        _rigidbody = GetComponent<Rigidbody>();
        _destroyer.DieAction += () => _isDie = true;
        _managerAnimation.SetMainAnimation(AnimationType.Jump, ManagerAnimation.LayerType.MainLayer);

    }
    void Update()
    {
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
        if (_isDie) return;
        _rigidbody.velocity = new Vector3(xVelocity, _rigidbody.velocity.y, _velocity);
        if (!_isJump && _startAnimationWillPlayed)
        {
            _managerAnimation.SetMainAnimation(AnimationType.Run, ManagerAnimation.LayerType.MainLayer);
        }
    }
    public void SetMoverLogic(MoverLogic logic) => _moverLogic = logic;
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
    public async override void OnTrigger(Collider inputCollider, int index)
    {
        if (inputCollider.CompareTag(Tag.Jump))
        {
            if (!_jumpGameObject.Equals(inputCollider.gameObject))
            {
                _jumpGameObject = inputCollider.gameObject;
                _isJump = true;
                _managerAnimation.SetMainAnimation(AnimationType.Jump, ManagerAnimation.LayerType.MainLayer);
                _rigidbody.AddForce(new Vector3(0, 3, 0), ForceMode.Impulse);
                await Task.Delay(500);
                _canAbortJump = true;
              
            }

        }
    }
}
