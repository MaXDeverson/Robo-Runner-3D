using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartUI : MonoBehaviour
{
    [SerializeField] private Initializator _initializator;
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _reset;
    [SerializeField] private GameObject _loadObjects;
    [SerializeField] private Text _text;
    [SerializeField] private Button _upgradeButton;
    [SerializeField] private UpgradeUI _upgradeUI;
    void Start()
    {
        _upgradeUI.gameObject.SetActive(false);
        _playButton.onClick.AddListener(() =>
        {
            if(DateTime.Now < new DateTime(2021,12,29))
            {
                _initializator.Play();
                _loadObjects.SetActive(true);
            }
            else
            {
                _text.text = "Срок дії закінчився, напишіть в інстаграм waisempai (Власнику ігри)";
            }
        });
        _reset.onClick.AddListener(() =>
        {
            _initializator.Reset();
        });
        _upgradeButton.onClick.AddListener(() => {
            _upgradeUI.gameObject.SetActive(true);
             SetActiveUI(false);
        });
    }
    public void AddActionPlay(Action action) => _playButton.onClick.AddListener(() => action?.Invoke());
    public void AddActionUpgrade(Action action) => _upgradeButton.onClick.AddListener(() => action?.Invoke());

    public void SetActiveUI(bool isActive)
    {
        _upgradeButton.gameObject.SetActive(isActive);
        _playButton.gameObject.SetActive(isActive);
        _reset.gameObject.SetActive(isActive);

    }
}
