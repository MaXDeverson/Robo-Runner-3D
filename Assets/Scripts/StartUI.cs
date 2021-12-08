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
}
