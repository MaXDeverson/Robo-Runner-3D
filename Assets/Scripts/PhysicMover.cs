
using UnityEngine;
using DG.Tweening;
using System.Collections;

public class PhysicMover : MonoBehaviour
{
    [SerializeField] private float _distance;
    [SerializeField] private float _timeMove;
    private bool _moveUp = true;
    private bool _isAnimated = false;

    // Update is called once per frame
    void Update()
    {
        if (!_isAnimated)
        {
            if (_moveUp)
            {
                StartCoroutine(MoveUp());
            }
            else
            {
                StartCoroutine(MoveDown());
            }
        }
     
    }

    private IEnumerator MoveUp()
    {
        _isAnimated = true;
        transform.DOMove(transform.position + new Vector3(0, _distance, 0), _timeMove);
        yield return new WaitForSeconds(_timeMove);
        _moveUp = false;
        _isAnimated = false;
    }
    private IEnumerator MoveDown()
    {
        _isAnimated = true;
        transform.DOMove(transform.position + new Vector3(0,-_distance,0), _timeMove);
        yield return new WaitForSeconds(_timeMove);
        _moveUp = true ;
        _isAnimated = false;
    }
}
