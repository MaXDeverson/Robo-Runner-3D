using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartUI : MonoBehaviour
{
    [SerializeField] private Initializator _initializator;
    [SerializeField] private Button _play;
    [SerializeField] private Button _reset;
    [SerializeField] private GameObject _loadOBjects;
    [SerializeField] private Text text;
    [Header("Buy/Update")]
    [SerializeField] private Button _nextHeroButton;
    [SerializeField] private Button _previousHeroButton;
    void Start()
    {
        _play.onClick.AddListener(() =>
        {
            if(DateTime.Now < new DateTime(2021,12,29))
            {
                _initializator.Play();
                _loadOBjects.SetActive(true);
            }
            else
            {
                text.text = "���� 䳿 ���������, �������� � ��������� waisempai (�������� ����)";
            }
        });
        _reset.onClick.AddListener(() =>
        {
            _initializator.Reset();
        });
    }

    public void AddActionNext(Action action) => _nextHeroButton.onClick.AddListener(()=>action?.Invoke());
    public void AddActionPrevious(Action action) => _previousHeroButton.onClick.AddListener(() => action?.Invoke());
    public void AddActionPlay(Action action) => _play.onClick.AddListener(() => action?.Invoke());
}
