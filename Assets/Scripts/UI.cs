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

    private HeroDestroyer _heroDestroyer;

    public void SetHeroDestroyer(HeroDestroyer heroDestroyer) => _heroDestroyer = heroDestroyer;
    public void SetUpdateDataUsualCrystal(PlayerData dtata)
    {
        dtata.ChangeCountUsualCrystalAction += (int updateCount) =>
        {
            _textCountUsualCrystals.text = updateCount + "";
        };
    }
    private void Start()
    {
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
