using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float _pushForce;
    private Rigidbody _rigidbody;
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private async void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tag.Bullet))
        {
            _rigidbody.AddForce(new Vector3(0,0,-_pushForce),ForceMode.Impulse);
            await Task.Delay(200);
            _rigidbody.velocity = Vector3.zero;
        }
    }
}
