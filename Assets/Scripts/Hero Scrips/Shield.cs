using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] private HeroDestroyer _destroyer;
    [SerializeField] private GameObject _shieldObj;
    private const int MAX_SCALE_SHIELD = 6;
    private float _shieldScaleValue = 1;

    private bool _isAnimatedScale;
    private bool _animationIsActive;
    void Start()
    {
        _shieldObj.transform.localScale = new Vector3(1, 1, 1);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!_isAnimatedScale && _animationIsActive)
        {
            StartCoroutine(Scale());
        }
    }

    public void SetActive(bool enable)
    {
        _destroyer.IgnoreDamage(enable);
        _shieldObj.SetActive(enable);
        StartScaleAniamtion();
    }

    private void StartScaleAniamtion()
    {
        _animationIsActive = true;
    }

    private IEnumerator Scale()
    {
        _isAnimatedScale = true;
        yield return new WaitForSeconds(0.001f);
        _shieldScaleValue += 0.6f;
        if (_shieldScaleValue >= MAX_SCALE_SHIELD)
        {
            _shieldObj.transform.localScale = new Vector3(MAX_SCALE_SHIELD, MAX_SCALE_SHIELD, MAX_SCALE_SHIELD);
            _animationIsActive = false;
        }
        else
        {
            _shieldObj.transform.localScale = new Vector3(_shieldScaleValue, _shieldScaleValue, _shieldScaleValue);
            _isAnimatedScale = false;
        }
       
    }
}
