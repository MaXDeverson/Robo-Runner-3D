using System;
using System.Collections.Generic;
using TMPro;
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
    [SerializeField] private BuyUI _upgradeUI;
    [Header("Crystals")]
    [SerializeField] private TextMeshProUGUI _textUsualCrystals;
    [SerializeField] private TextMeshProUGUI _textElectroCrystals;


    void Start()
    {
        _upgradeUI.gameObject.SetActive(false);
        _playButton.onClick.AddListener(() =>
        {
            if(DateTime.Now < new DateTime(2022,1,14))
            {
                _initializator.Play();
                _loadObjects.SetActive(true);
            }
            else
            {
                _text.text = "Срок дії закінчився, напишіть в інстаграм waisempai (Власнику ігри)";
            }
        });
        _upgradeButton.onClick.AddListener(() => {
            _upgradeUI.gameObject.SetActive(true);
             SetActiveUI(false);
        });
        //player data initialization;
        PlayerData data = PlayerData.GetPlayerData();
        _textUsualCrystals.text = data.CountUsualCrystals + "";
        _textElectroCrystals.text = data.CountElectroCrystals + "";
    }
    public void AddActionPlay(Action action) => _playButton.onClick.AddListener(() => action?.Invoke());
    public void AddActionUpgrade(Action action) => _upgradeButton.onClick.AddListener(() => action?.Invoke());
    public void AddActionResset(Action action) => _reset.onClick.AddListener(()=>action?.Invoke());

    public void SetActiveUI(bool isActive)
    {
        _upgradeButton.gameObject.SetActive(isActive);
        _playButton.gameObject.SetActive(isActive);
        _reset.gameObject.SetActive(isActive);

    }
    public void UpdateUsualCrystals(int countUsualCrystals) => _textUsualCrystals.text = countUsualCrystals + "";
    public void UpdateElectroCrysals(int countCrystals)=> _textElectroCrystals.text = countCrystals + "";

}
