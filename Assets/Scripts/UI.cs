using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textLifes;
    [SerializeField] private Slider _lifesSlider;
    [Header("For crystals")]
    [SerializeField] private TextMeshProUGUI _textCountUsualCrystals;
    [SerializeField] private TextMeshProUGUI _textCountElectricCrystal;
    [SerializeField] private Button _replyButton;

    private HeroDestroyer _heroDestroyer;

    public void SetHeroDestroyer(HeroDestroyer heroDestroyer) => _heroDestroyer = heroDestroyer;
    public void SetUpdateDataUsualCrystal(PlayerData dtata)
    {
        dtata.ChangeCountUsualCrystalAction += (int updateCount) =>
        {
            _textCountUsualCrystals.text = updateCount + "";
        };
        dtata.ChangeCountElectroCrystalAction += (int updateCount) =>
          {
              _textCountElectricCrystal.text = updateCount + "";
          };
    }
    private void Start()
    {
        _replyButton.onClick.AddListener(() =>
        {
            Level.CurrentLevel.Restart();
        });
        _heroDestroyer.GetDamageAction += (count) =>
        {
            _textLifes.text = count + "";
        };
        _heroDestroyer.GetDamageActionProcent += (procent) =>
        {
            _lifesSlider.value = procent;
        };
    }

    void Update()
    {
        
    }
}
