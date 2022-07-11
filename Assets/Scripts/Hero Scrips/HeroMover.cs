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
    private float _velocityXMultiplier = 1F;
    public float VelocityXMultiplier { get => _velocityXMultiplier; set => _velocityXMultiplier = value; }
    //for jump animation;
    private bool _isJump;
    private bool _canAbortJump;
    private GameObject _jumpGameObject;
    //for start animation;
    private bool _startAnimationWillPlayed;
    //for stay animation
    private bool _stay;

    //private float _maxValue = 0;
    void Start()
    {
        _jumpGameObject = gameObject;
        _rigidbody = GetComponent<Rigidbody>();
        _destroyer.DieAction += () => _isDie = true;
        _managerAnimation.PlayAnimation(AnimationType.Jump, ManagerAnimation.LayerType.MainLayer);

    }
    void Update()
    {
        float xVelocity = _moverLogic.PositionX * 70 * _velocityXMultiplier;
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
        _rigidbody.velocity = new Vector3(xVelocity, _rigidbody.velocity.y, _stay?_velocity * 0.4f:_velocity);
        //float newXPosition = transform.position.x + (xVelocity / 70);
        //transform.position = new Vector3(newXPosition> _moveXRestriction? _moveXRestriction:(newXPosition< -_moveXRestriction?-_moveXRestriction:newXPosition), transform.position.y, transform.position.z);
        if (!_isJump && !_stay && _startAnimationWillPlayed)
        {
            _managerAnimation.PlayAnimation(AnimationType.Run, ManagerAnimation.LayerType.MainLayer);
        }

        ////////////////////////
        //float speed = _rigidbody.velocity.y;
        //if (_maxValue < speed)
        //{
        //    _maxValue = speed;
        //    Debug.Log(_maxValue);
        //}
        //if (_rigidbody.velocity.y > _maxVelocity)
        //{
        //    _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, _maxVelocity, _rigidbody.velocity.z);
        //}
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
        switch (inputCollider.tag)
        {
            case Tag.Jump:
                if (!_jumpGameObject.Equals(inputCollider.gameObject))
                {
                    _jumpGameObject = inputCollider.gameObject;
                    _isJump = true;
                    _managerAnimation.PlayAnimation(AnimationType.Jump, ManagerAnimation.LayerType.MainLayer);
                    _rigidbody.AddForce(new Vector3(0, 3, 0), ForceMode.Impulse);
                    await Task.Delay(500);
                    _canAbortJump = true;
                }
                break;
            case Tag.StopSpot:
                _stay = true;
                _managerAnimation.PlayAnimation(AnimationType.Stay, ManagerAnimation.LayerType.MainLayer);
                break;
        }
    }
    public override void TriggerExit(Collider exitCollider, int triggerIndex)
    {
        if (exitCollider.CompareTag(Tag.StopSpot))
        {
            _stay = false;
            _managerAnimation.PlayAnimation(AnimationType.Run, ManagerAnimation.LayerType.MainLayer);
        }
    }
}
