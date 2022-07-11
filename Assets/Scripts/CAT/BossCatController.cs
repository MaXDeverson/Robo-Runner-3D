using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCatController : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private ManagerAnimation _managerAnimation;
    [SerializeField] private bool _run;
    private Rigidbody _rigidbody;
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_run)
        {
            _rigidbody.velocity = new Vector3(0, _rigidbody.velocity.y, _speed);
            _managerAnimation.PlayAnimation(AnimationType.Run,ManagerAnimation.LayerType.MainLayer);
        }
      
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case Tag.Jump:
                _run = false;
                _managerAnimation.PlayAnimation(AnimationType.Jump, ManagerAnimation.LayerType.MainLayer);
                _rigidbody.AddForce(new Vector3(0, 3, 0), ForceMode.Impulse);
                break;
        }
    }
}
