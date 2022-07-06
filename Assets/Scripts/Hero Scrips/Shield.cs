using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Shield : MonoBehaviour
{
    [SerializeField] private HeroDestroyer _destroyer;
    [SerializeField] private GameObject _shieldObj;
    public int ShieldTimeActive { get => _shieldTimeActive;}
    public Action ActivateAction;
    private const int MAX_SCALE_SHIELD = 6;
    private const int MIN_SCALE_SHIELD = 1;
    private float _shieldScaleValue = 1;
    private int _shieldTimeActive = 4;

    private bool _isAnimatedScaleUP;
    private bool _animationUPIsActive;
    private bool _isAnimatedScaleDown;
    private bool _animationDownIsActive;

    private bool _shieldIsActive;

    public void SetShieldTimeActive(int timeInSeconds) => _shieldScaleValue = timeInSeconds;
    public bool ShieldIsActive() => _shieldIsActive;
    void Start()
    {
        _shieldTimeActive = Level.CurrentLevel.HeroData.ShieldTimeCount;
        _shieldObj.transform.localScale = new Vector3(MIN_SCALE_SHIELD, MIN_SCALE_SHIELD, MIN_SCALE_SHIELD);
    }

    void FixedUpdate()
    {
        if (!_isAnimatedScaleUP && _animationUPIsActive)
        {
            StartCoroutine(ScaleUP());
        }
        if(!_isAnimatedScaleDown && _animationDownIsActive)
        {
            StartCoroutine(ScaleDOWN());
        }
    }

    public void SetActive(bool enable)
    {
        _destroyer.IgnoreDamage(enable);
        _shieldObj.SetActive(enable);
        if (enable)
        {
            if (!_shieldIsActive)
            {
                _shieldIsActive = true;
                _shieldObj.transform.localScale = new Vector3(MIN_SCALE_SHIELD, MIN_SCALE_SHIELD, MIN_SCALE_SHIELD);
                _shieldObj.gameObject.SetActive(true);
                _shieldScaleValue = 1;
                _isAnimatedScaleUP = false;
                StartScaleUpAniamtion();
                StartCoroutine(TurnOff());
                ActivateAction.Invoke();
            }
        }
        else
        {
            Level.CurrentLevel.AudioSourses.SourceShield.PlayOneShot(Level.CurrentLevel.SoundList.DisappearanceShield);
            _shieldObj.transform.localScale = new Vector3(6, 6, 6);
            _shieldObj.gameObject.SetActive(true);
            _shieldScaleValue = 6;
            _isAnimatedScaleDown = false;

            StartScaleDownAnimation();
        }
    }

    public void Activate(int time)
    {
        _destroyer.IgnoreDamage(true);
        _shieldObj.SetActive(true);
        int currentTime = _shieldTimeActive;
        _shieldTimeActive = time;
        _shieldIsActive = true;
        _shieldObj.transform.localScale = new Vector3(MIN_SCALE_SHIELD, MIN_SCALE_SHIELD, MIN_SCALE_SHIELD);
        _shieldObj.gameObject.SetActive(true);
        _shieldScaleValue = 1;
        _isAnimatedScaleUP = false;
        StartScaleUpAniamtion();
        StartCoroutine(TurnOff());
        _shieldTimeActive = currentTime;
    }

    private void StartScaleUpAniamtion()
    {
        _animationUPIsActive = true;
    }

    private void StartScaleDownAnimation()
    {
        _animationDownIsActive = true;
    }

    private IEnumerator ScaleUP()
    {
        _isAnimatedScaleUP = true;
        yield return new WaitForSeconds(0.001f);
        _shieldScaleValue += 0.6f;
        if (_shieldScaleValue >= MAX_SCALE_SHIELD)
        {
            _shieldObj.transform.localScale = new Vector3(MAX_SCALE_SHIELD, MAX_SCALE_SHIELD, MAX_SCALE_SHIELD);
            _animationUPIsActive = false;
        }
        else
        {
            _shieldObj.transform.localScale = new Vector3(_shieldScaleValue, _shieldScaleValue, _shieldScaleValue);
            _isAnimatedScaleUP = false;
        }
    }

    private IEnumerator ScaleDOWN()
    {
        _isAnimatedScaleDown = true;
        yield return new WaitForSeconds(0.001f);
        _shieldScaleValue -= 0.6f;
        if (_shieldScaleValue <= MIN_SCALE_SHIELD)
        {
            _shieldObj.transform.localScale = new Vector3(MIN_SCALE_SHIELD, MIN_SCALE_SHIELD, MIN_SCALE_SHIELD);
            _animationDownIsActive = false;
            _shieldObj.gameObject.SetActive(false);

        }
        else
        {
            _shieldObj.transform.localScale = new Vector3(_shieldScaleValue, _shieldScaleValue, _shieldScaleValue);
            _isAnimatedScaleDown = false;
            _shieldIsActive = false;
        }
    }
    private IEnumerator TurnOff()
    {
        yield return new WaitForSeconds(_shieldTimeActive);
        SetActive(false);
    }
}
