using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuideUI :Triggerable
{
    [SerializeField] private Button _clickAnimation;
    [SerializeField] private Button _guideButton;
    [SerializeField] private GameObject _lockPanel;
    [SerializeField] private GameObject _firstParent;
    [SerializeField] private float _timeScale;
    [SerializeField] private GuideUI _nextGuide;
    [SerializeField] private int _guideIndex;
    [SerializeField] private bool _startinAwake;
    [SerializeField] private bool _setInvizible;
    private const float FIXED_DELTA_TIME = 0.02f;
    private const float DELTA_TIME_SLOW = 0.01f;
    private void Start()
    {
        //if(_lockPanel != null && _nextGuide == null) _lockPanel.SetActive(false);
        _guideButton.gameObject.SetActive(false);
        _clickAnimation.gameObject.SetActive(false);
        _clickAnimation.onClick.AddListener(()=>
        {
            _guideButton.onClick.Invoke();
        });
        _guideButton.onClick.AddListener(() =>
        {
            if(_guideIndex == Serializator.DeSerialize(DataName.CurrentGuideIndex))
            {
                Time.timeScale = 1;
                Time.fixedDeltaTime = FIXED_DELTA_TIME;
                _clickAnimation.gameObject.SetActive(false);
                if (_lockPanel != null)
                {
                    _guideButton.transform.SetParent(_firstParent.transform,false);
                    _lockPanel.transform.SetParent(_guideButton.transform, false);
                    _lockPanel.transform.SetParent(_firstParent.transform, false);
                    if (_nextGuide==null) _lockPanel.SetActive(false);
                }
                Serializator.Serialize(DataName.CurrentGuideIndex, _guideIndex + 1);
                if (_nextGuide != null)
                {
                    _nextGuide.PlayGyide();
                }
                else
                {
                    Time.timeScale = 1;
                    Time.fixedDeltaTime = FIXED_DELTA_TIME;
                }
            }
        });
        if (_startinAwake)
        {
            PlayGyide();
        }
        if (!_setInvizible && _guideIndex != Serializator.DeSerialize(DataName.CurrentGuideIndex))
        {
            _guideButton.gameObject.SetActive(true);
        }
    }
    public override void OnTrigger(Collider inputCollider, int triggerIndex)
    {
        if (inputCollider.CompareTag(Tag.Player) && !inputCollider.isTrigger)
        {
            PlayGyide(true);
        }
    }
    public void PlayGyide(bool isTimeScale = false)
    {
        if (_guideIndex == Serializator.DeSerialize(DataName.CurrentGuideIndex))
        {
            _clickAnimation.gameObject.SetActive(true);
            _guideButton.gameObject.SetActive(true);
            if (_lockPanel != null)
            {
                _lockPanel.SetActive(true);
                _guideButton.transform.SetParent(_lockPanel.transform, false);
                _clickAnimation.transform.SetParent(_lockPanel.transform, false);
                _lockPanel.transform.SetParent(_firstParent.transform, false);
            }
            if (isTimeScale)
            {
                Time.timeScale = _timeScale;
                Time.fixedDeltaTime = DELTA_TIME_SLOW;
            }
        }
    }
}
