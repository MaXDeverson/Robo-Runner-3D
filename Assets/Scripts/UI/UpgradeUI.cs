
using System;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUI : MonoBehaviour
{
    [SerializeField] private Button _nextHeroButton;
    [SerializeField] private Button _previousHeroButton;

    [SerializeField] private Button _selectButton;
    [SerializeField] private Button _buyButton;
    [SerializeField] private Button _updateLifesButton;
    [SerializeField] private Button _back;
    void Start()
    {
        _back.onClick.AddListener(() => SetActive(false));
    }
    public void UpdateUI(HeroData data)
    {
        _selectButton.gameObject.SetActive(data.IsBuy && !data.IsSelect);
        _buyButton.gameObject.SetActive(!data.IsBuy);
    }
    public void AddActionNext(Action action) => _nextHeroButton.onClick.AddListener(() => action?.Invoke());
    public void AddActionPrevious(Action action) => _previousHeroButton.onClick.AddListener(() => action?.Invoke());
    public void AddActionSelect(Action action) => _selectButton.onClick.AddListener(() => action?.Invoke());
    public void AddActionBuy(Action action) => _buyButton.onClick.AddListener(() => action?.Invoke());
    public void AddAtionUpdateLifes(Action action) => _updateLifesButton.onClick.AddListener(() => action?.Invoke());
    public void AddActionBack(Action action) => _back.onClick.AddListener(() => action?.Invoke());

    public void SetActive(bool isActive)
    {
        _nextHeroButton.gameObject.SetActive(isActive);
        _previousHeroButton.gameObject.SetActive(isActive);
        _selectButton.gameObject.SetActive(isActive);
        _buyButton.gameObject.SetActive(isActive);
        _updateLifesButton.gameObject.SetActive(isActive);
        _back.gameObject.SetActive(isActive);
    }

}
