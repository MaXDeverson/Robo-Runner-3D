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
    [SerializeField] private Button _rateSelectButton;
    [SerializeField] private Button _shieldSelectButton;
    [SerializeField] private TextMeshProUGUI _priceUpgrade;
    [SerializeField] private Color _selectButtonColor;
    [SerializeField] private Color _defalultButtonColor;
    [Header("Update View")]
    [SerializeField] private List<GameObject> _viewBoxesUpdate;
    [SerializeField] private List<GameObject> _useViewBoxesUpdate;
    [SerializeField] private TextMeshProUGUI _textUpgradeInformation;
    [Header("Sound")]
    [SerializeField] private AudioSource _buttonSource;
    private List<Button> _upgradeButtons;
    private ColorBlock _selectColorBlock;
    private ColorBlock _defaultColorBlock;

    void Start()
    {
        InitializationSelectUpdateButtons();
        SoundInit();
    }

    private void SoundInit()
    {
        _backButton.onClick.AddListener(() => _buttonSource.Play());
        _upgradeButton.onClick.AddListener(() => _buttonSource.Play());
        _lifesSelectButton.onClick.AddListener(() => _buttonSource.Play());
        _damageSelectButton.onClick.AddListener(() => _buttonSource.Play());
        _rateSelectButton.onClick.AddListener(() => _buttonSource.Play());
        _shieldSelectButton.onClick.AddListener(() => _buttonSource.Play());
    }
    private void InitializationSelectUpdateButtons()
    {
        _upgradeButtons = new List<Button> { _lifesSelectButton, _damageSelectButton, _rateSelectButton, _shieldSelectButton };
        _defaultColorBlock = new ColorBlock();
        _defaultColorBlock.colorMultiplier = 1.18f;
        _defaultColorBlock.normalColor = _defalultButtonColor;
        _defaultColorBlock.selectedColor = _lifesSelectButton.colors.selectedColor;
        _defaultColorBlock.pressedColor = _lifesSelectButton.colors.pressedColor;
        _defaultColorBlock.highlightedColor = _lifesSelectButton.colors.highlightedColor;
        ///
        _selectColorBlock = new ColorBlock();
        _selectColorBlock.colorMultiplier = 1.18f;
        _selectColorBlock.normalColor = _selectButtonColor;
        _selectColorBlock.selectedColor = _lifesSelectButton.colors.selectedColor;
        _selectColorBlock.pressedColor = _lifesSelectButton.colors.pressedColor;
        _selectColorBlock.pressedColor = _lifesSelectButton.colors.highlightedColor;
        ///
        _lifesSelectButton.colors = _selectColorBlock;
        _lifesSelectButton.onClick.AddListener(() =>
        {
            if (!_lifesSelectButton.colors.Equals(_selectColorBlock))
            {
                _upgradeButtons.ForEach(x => x.colors = _defaultColorBlock);
                _lifesSelectButton.colors = _selectColorBlock;
            }

        });
        _damageSelectButton.onClick.AddListener(() =>
        {
            if (!_damageSelectButton.colors.Equals(_selectColorBlock))
            {
                _upgradeButtons.ForEach(x => x.colors = _defaultColorBlock);
                _damageSelectButton.colors = _selectColorBlock;
            }
        });
        _rateSelectButton.onClick.AddListener(() =>
        {
            if (!_rateSelectButton.colors.Equals(_selectColorBlock))
            {
                _upgradeButtons.ForEach(x => x.colors = _defaultColorBlock);
                _rateSelectButton.colors = _selectColorBlock;
            }
        });
        _shieldSelectButton.onClick.AddListener(() =>
        {
            if (!_shieldSelectButton.colors.Equals(_selectColorBlock))
            {
                _upgradeButtons.ForEach(x => x.colors = _defaultColorBlock);
                _shieldSelectButton.colors = _selectColorBlock;
            }
        });

    }
    public void AddActionBack(Action action) => _backButton.onClick.AddListener(() => action?.Invoke());
    public void AddActionUpgrade(Action action) => _upgradeButton.onClick.AddListener(() => action?.Invoke());
    public void AddActionLifesSelect(Action action) => _lifesSelectButton.onClick.AddListener(() => action?.Invoke());
    public void AddActionDamageSelect(Action action) => _damageSelectButton.onClick.AddListener(() => action?.Invoke());
    public void AddActionRateSelect(Action action) => _rateSelectButton.onClick.AddListener(() => action?.Invoke());
    public void AddActionShieldSelect(Action action) => _shieldSelectButton.onClick.AddListener(() => action?.Invoke());
    public void SetActive(bool active)
    {
        _rootObj.SetActive(active);
        if (active)
        {
            //initialization;
        }
    }
    public void UpdateUI(HeroData data, UpgradeType type)
    {
        int maxUpgrade = 0;
        switch (type)
        {
            case UpgradeType.Lifes:
                maxUpgrade = data.GetLifesLevelsData.Length;
                for(int i = 0; i < _viewBoxesUpdate.Count; i++)
                {
                   _viewBoxesUpdate[i].SetActive(i<maxUpgrade);
                    _useViewBoxesUpdate[i].SetActive(i <= data.LifesLevel);
                }
                if(maxUpgrade - 1 == data.LifesLevel)
                {
                    _upgradeButton.gameObject.SetActive(false);
                    _priceUpgrade.text = "Full";
                    _textUpgradeInformation.text = "Upgrade lifes is full";
                }
                else
                {
                    _upgradeButton.gameObject.SetActive(true);
                    _priceUpgrade.text = data.GetLifesLevelsData[data.LifesLevel + 1].GetPrice() + " <>";
                    _textUpgradeInformation.text = "Upgrade lifes count from " + data.GetLifesLevelsData[data.LifesLevel].GetValue() +
                        " to " + data.GetLifesLevelsData[data.LifesLevel + 1].GetValue();
                }
                break;
            case UpgradeType.Damage:
                maxUpgrade = data.GetDamageLevelsData.Length;
                for (int i = 0; i < _viewBoxesUpdate.Count; i++)
                {
                    _viewBoxesUpdate[i].SetActive(i < maxUpgrade);
                    _useViewBoxesUpdate[i].SetActive(i <= data.DamageLevel);
                }
                if (maxUpgrade -1 == data.DamageLevel)
                {
                    _upgradeButton.gameObject.SetActive(false);
                    _priceUpgrade.text = "Full";
                    _textUpgradeInformation.text = "Upgrade damage is full";
                }
                else
                {
                    _upgradeButton.gameObject.SetActive(true);
                    _priceUpgrade.text = data.GetDamageLevelsData[data.DamageLevel + 1].GetPrice() + " <>";
                    _textUpgradeInformation.text = "Upgrade damage from " + data.GetDamageLevelsData[data.DamageLevel].GetValue() +
                        " to " + data.GetDamageLevelsData[data.DamageLevel + 1].GetValue();
                }
                break;
            case UpgradeType.Shield:
                maxUpgrade = data.GetShieldTimeLevelsData.Length;
                for (int i = 0; i < _viewBoxesUpdate.Count; i++)
                {
                    _viewBoxesUpdate[i].SetActive(i < maxUpgrade);
                    _useViewBoxesUpdate[i].SetActive(i <= data.ShieldTimeLevel);
                }
                if (maxUpgrade -1 == data.ShieldTimeLevel)
                {
                    _upgradeButton.gameObject.SetActive(false);
                    _priceUpgrade.text = "Full";
                    _textUpgradeInformation.text = "Upgrade time shield is full";
                }
                else
                {
                    _upgradeButton.gameObject.SetActive(true);
                    _priceUpgrade.text = data.GetShieldTimeLevelsData[data.ShieldTimeLevel + 1].GetPrice() + " <>";
                    _textUpgradeInformation.text = "Upgrade shield time from " + data.GetShieldTimeLevelsData[data.ShieldTimeLevel].GetValue() +
                  " to " + data.GetShieldTimeLevelsData[data.ShieldTimeLevel + 1].GetValue();
                }
                break;
            case UpgradeType.Rate:
                maxUpgrade = data.GetRateLevelsData.Length;
                for (int i = 0; i < _viewBoxesUpdate.Count; i++)
                {
                    _viewBoxesUpdate[i].SetActive(i < maxUpgrade);
                    _useViewBoxesUpdate[i].SetActive(i <= data.RateLevel);
                }
                if (maxUpgrade -1 == data.RateLevel)
                {
                    _upgradeButton.gameObject.SetActive(false);
                    _priceUpgrade.text = "Full";
                    _textUpgradeInformation.text = "Upgrade a rate is full";
                }
                else
                {
                    _upgradeButton.gameObject.SetActive(true);
                    _priceUpgrade.text = data.GetRateLevelsData[data.RateLevel + 1].GetPrice() + " <>";
                    _textUpgradeInformation.text = "Upgrade rate from " + data.GetRateLevelsData[data.RateLevel].GetValue() +
                        " to " + data.GetRateLevelsData[data.RateLevel + 1].GetValue();
                }
                break;
        }
    }
}
