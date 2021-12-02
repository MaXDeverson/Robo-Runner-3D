using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartUI : MonoBehaviour
{
    [SerializeField] private Initializator _initializator;
    [SerializeField] private Button _play;
    [SerializeField] private Button _reset;
    [SerializeField] private GameObject _loadOBjects;
    void Start()
    {
        _play.onClick.AddListener(() =>
        {
            _initializator.Play();
            _loadOBjects.SetActive(true);
        });
        _reset.onClick.AddListener(() =>
        {
            _initializator.Reset();
        });
    }
}
