using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DoubleClickController : MonoBehaviour, IPointerClickHandler
{
    public Action DoubleClickAction;
    private DateTime _timeFirstClick;
    private DateTime _timeSecondClick;
    //For  guide
    [SerializeField] private Button _guideButton;
    [SerializeField] private bool _avaliable = true;
    void Start()
    {
        if(_guideButton != null)
        {
            DoubleClickAction += () => _guideButton.onClick.Invoke();
        }

        _timeFirstClick = DateTime.Now;
        _timeSecondClick = DateTime.Now;
       
    }

     public void OnPointerClick(PointerEventData eventData)
    {
        if (_avaliable)
        {
            _timeFirstClick = _timeSecondClick;
            _timeSecondClick = DateTime.Now;
            TimeSpan diff = (_timeSecondClick - _timeFirstClick);
            Debug.Log("Mill:" + diff.Milliseconds + " Sec:" + diff.Seconds);
            if (diff.Milliseconds <= 300 && diff.Seconds < 1 && diff.Minutes < 1)
            {
                DoubleClickAction?.Invoke();
                _timeSecondClick = DateTime.Today;
            }
        }
    }
}
