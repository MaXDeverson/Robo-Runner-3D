using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUI : MonoBehaviour
{
    [SerializeField] private GameObject _rootObj;
    [SerializeField] private Button _backButton;
    [SerializeField] private Button _upgradeButton;

    [SerializeField] private Button _lifesSelectButton;
    [SerializeField] private Button _damageSelectButton;
    [SerializeField] private TextMeshProUGUI _priceUpgrade;
    [SerializeField] private List<GameObject> _upgradeLevelIcons;
    void Start()
    {
        
    }

    public void AddActionBack(Action action) => _backButton.onClick.AddListener(() => action?.Invoke());
    public void AddActionUpgrade(Action action) => _upgradeButton.onClick.AddListener(() => action?.Invoke());
    public void AddActionLifesSelect(Action action) => _lifesSelectButton.onClick.AddListener(() => action?.Invoke());
    public void AddActionDamageSelect(Action action) => _damageSelectButton.onClick.AddListener(() => action?.Invoke());

    public void SetActive(bool active)
    {
        _rootObj.SetActive(active);
        if (active)
        {
           //initialization;
        }
    }

    public void UpdateUI(HeroData data)
    {

    }
}
