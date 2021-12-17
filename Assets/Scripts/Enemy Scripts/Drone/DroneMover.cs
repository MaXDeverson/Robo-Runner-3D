using MyRandom = System.Random;
using UnityEngine;

public class DroneMover : Triggerable
{
    [SerializeField] private HeroMover _hero;
    private Rigidbody _rigidbody;
    private Vector3 _moveForce;
    private MyRandom _random;

    private bool _canMove;
    private void Awake()
    {
        _random = new MyRandom();
    }
    void Start()
    {
        _hero = Level.CurrentLevel.Hero.GetComponent<HeroMover>();
        _rigidbody = GetComponent<Rigidbody>();
        _moveForce = new Vector3(_random.Next(-3, 0), _random.Next(-3, 0), 0);
    }
    // Update is called once per frame
    void Update()
    {
        if(transform.position.x > 0)
        {
            _moveForce.x =(float)( _random.Next(-2,0) * _random.NextDouble());
        }
        if (transform.position.x < 0)
        {
            _moveForce.x = (float)(_random.Next(0,3) * _random.NextDouble());
        }
        if(transform.position.y > 6)
        {
            _moveForce.y = (float) -_random.NextDouble();
        }
        if(transform.position.y < 6)
        {
            _moveForce.y = (float) _random.NextDouble();
        }
    }
    private void FixedUpdate()
    {
        Vector3 deltaDistance = _hero.transform.position - transform.position;
        float newXRotation = Mathf.Atan(-deltaDistance.y / deltaDistance.z);
        transform.eulerAngles = new Vector3((newXRotation * 180 / Mathf.PI), transform.eulerAngles.y, transform.eulerAngles.z);
        if (_canMove)
        {
            _rigidbody.AddForce(_moveForce);
        }
    }

    public override void OnTrigger(Collider inputCollider)
    {
        if (inputCollider.CompareTag(Tag.Player))
        {
            _canMove = true;
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, _rigidbody.velocity.y, _hero.GetZVelocity());
        }
    }
}
