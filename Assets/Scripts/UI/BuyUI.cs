
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyUI : MonoBehaviour
{
    [SerializeField] private Button _nextHeroButton;
    [SerializeField] private Button _previousHeroButton;

    [SerializeField] private Button _selectButton;
    [SerializeField] private Button _buyButton;
    [SerializeField] private Button _upgradeButton;
    [SerializeField] private Button _back;
    [SerializeField] private GameObject _pricePlace;
    [SerializeField] private TextMeshProUGUI _price;
    void Start()
    {
        _back.onClick.AddListener(() => SetActive(false));
    }
    public void UpdateUI(HeroData data)
    {
        _selectButton.gameObject.SetActive(data.IsBuy && !data.IsSelect);
        _buyButton.gameObject.SetActive(!data.IsBuy);
        _price.text = data.IsBuy ? data.IsSelect?"Selected" : "Available": data.Price + "";
    }
    public void AddActionNext(Action action) => _nextHeroButton.onClick.AddListener(() => action?.Invoke());
    public void AddActionPrevious(Action action) => _previousHeroButton.onClick.AddListener(() => action?.Invoke());
    public void AddActionSelect(Action action) => _selectButton.onClick.AddListener(() => action?.Invoke());
    public void AddActionBuy(Action action) => _buyButton.onClick.AddListener(() => action?.Invoke());
    public void AddActionBack(Action action) => _back.onClick.AddListener(() => action?.Invoke());
    public void AddActionUpgrade(Action action) => _upgradeButton.onClick.AddListener(() => action?.Invoke());

    public void SetActive(bool isActive)
    {
        _nextHeroButton.gameObject.SetActive(isActive);
        _previousHeroButton.gameObject.SetActive(isActive);
        _selectButton.gameObject.SetActive(isActive);
        _buyButton.gameObject.SetActive(isActive);
        _upgradeButton.gameObject.SetActive(isActive);
        _back.gameObject.SetActive(isActive);
        _pricePlace.SetActive(isActive);
    }

}
