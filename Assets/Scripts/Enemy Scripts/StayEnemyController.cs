
using UnityEngine;
using DG.Tweening;
using System.Collections;

public class StayEnemyController : MonoBehaviour
{
    [SerializeField] private Transform _enemy;
    [SerializeField] private EnemyAnimator _animation;
    [SerializeField] private Transform _throwPrefab;
    [SerializeField] private Transform _erisePosition;
    [SerializeField] private EnemyDestroyer _destroyer;
    private bool _wasThrowed;
    private bool _isDie;
    void Start()
    {
        _destroyer.ActionDie += () => _isDie = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_isDie)
            switch (other.tag)
            {
                case Tag.Player:
                    if (!_wasThrowed)
                    {
                        StartCoroutine(Throw(other.transform));
                        _wasThrowed = true;
                    }

                    break;
            }
    }

    private IEnumerator Throw(Transform target)
    {
        _animation.PlayAnimation(AnimationType.Throw);
        Vector3 vectorDirection = target.position - _enemy.position;
        float eulerAngle = Mathf.Atan(vectorDirection.x / vectorDirection.z) * 180 / Mathf.PI;
        _enemy.DORotate(new Vector3(0, _enemy.eulerAngles.y + (eulerAngle * 1.6f), 0), 0.3f);
        yield return new WaitForSeconds(0.5f);
        if (!_isDie)
        {
            Rigidbody newPrefab = Instantiate(_throwPrefab, _erisePosition.position, Quaternion.identity).GetComponent<Rigidbody>();
            vectorDirection = target.position - newPrefab.transform.position;
            vectorDirection.z += 3f;
            vectorDirection.y = 0;
            newPrefab.AddForce(vectorDirection.normalized * 10, ForceMode.Impulse);
        }
    }
    private void GranadeMove(Transform target)
    {
        Transform newPrefab = Instantiate(_throwPrefab, _erisePosition.position, Quaternion.identity);
        newPrefab.DOMove(new Vector3(target.position.x, target.position.y, target.position.z + 5), 0.3f);
    }
}
