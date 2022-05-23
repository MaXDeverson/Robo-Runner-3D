using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;
using UnityEngine.SceneManagement;

public class BuyUppLogic : MonoBehaviour
{
    [SerializeField] private List<Transform> _visualHeroes;
    [SerializeField] private CameraBuy _camera;
    [SerializeField] private BuyUI _buyUI;
    [SerializeField] private StartUI _startUI;
    [SerializeField] private UpgradeUI _upgradeUI;
    private List<HeroData> _dataHeroes;
    private PlayerData _palyerData;
    private int _selectedHeroIndex;
    private int _currentHeroIndex;
    //
    private UpgradeType _upgradeType;

    private void Awake()
    {
        Serializator.InitializeHeroDataUpdates();
    }
    private void Start()
    {
        //try
        //{
        //    MobileAds.Initialize(x => { });
        //}
        //catch (Exception exception)
        //{
        //    _startUI.ShowNotification(exception.Message);
        //}
        _startUI.gameObject.SetActive(true);////fortesting
        _upgradeUI.gameObject.SetActive(true);
        _buyUI.gameObject.SetActive(true);
        _buyUI.SetActive(false);
        _upgradeUI.SetActive(false);
        ////////////////////////////////////////
        _palyerData = PlayerData.GetPlayerData();
        InitializationDataOfHero();

        InitializationUI();
        _camera.SetTarget(_visualHeroes[_selectedHeroIndex], false);
        _palyerData.SetChangeCounUCystal((usualCont) => _startUI.UpdateUsualCrystals(usualCont));
        _palyerData.SetChangeCounECrystal((count) => _startUI.UpdateElectroCrysals(count));
    }
    private void InitializationUI()
    {
        _buyUI.AddActionNext(NextHero);
        _buyUI.AddActionSelect(() => SelectHero());
        _buyUI.AddActionBack(BackToUI);
        _buyUI.AddActionPrevious(PreviousHero);
        _buyUI.AddActionBuy(BuyHero);
        _buyUI.AddActionUpgrade(UpgradeHeroMode);
        _buyUI.AddActionUpgrade(() => _camera.AnimateRotate());

        _startUI.AddActionUpgrade(() => _buyUI.SetActive(true));
        _startUI.AddActionUpgrade(() => _buyUI.UpdateUI(_dataHeroes[_currentHeroIndex]));
        _startUI.AddActionUpgrade(_camera.AnimatePositionNear);
        _startUI.AddActionResset(Resset);
        InitializationUpgradeUI();
    }
    private void InitializationUpgradeUI()
    {
        _upgradeUI.AddActionBack(BackToBuyMode);
        _upgradeUI.AddActionBack(() => _camera.AnimateRotateOut());
        ///Select buttons initialize
        _upgradeUI.AddActionLifesSelect(() =>
        {
            _upgradeType = UpgradeType.Lifes;
            _upgradeUI.UpdateUI(_dataHeroes[_currentHeroIndex], _upgradeType);
        });
        _upgradeUI.AddActionDamageSelect(() =>
        {
            _upgradeType = UpgradeType.Damage;
            _upgradeUI.UpdateUI(_dataHeroes[_currentHeroIndex], _upgradeType);
        });
        _upgradeUI.AddActionShieldSelect(() =>
        {
            _upgradeType = UpgradeType.Shield;
            _upgradeUI.UpdateUI(_dataHeroes[_currentHeroIndex], _upgradeType);
        });
        _upgradeUI.AddActionRateSelect(() =>
        {
            _upgradeType = UpgradeType.Rate;
            _upgradeUI.UpdateUI(_dataHeroes[_currentHeroIndex], _upgradeType);
        });
        ///
        _upgradeUI.AddActionUpgrade(() => UpgradeHero());
    }
    private void InitializationDataOfHero()
    {
        _dataHeroes = Serializator.DeSerialize();
        for (int i = 0; i < _dataHeroes.Count; i++)
        {
            if (_dataHeroes[i].IsSelect)
            {
                _selectedHeroIndex = i;
                Level.SetHeroIndex(i);
                _currentHeroIndex = i;
            }
        }
    }
    private void BackToUI()
    {
        _startUI.SetActiveUI(true);
        _camera.SetTarget(_visualHeroes[_selectedHeroIndex]);
        _camera.AnimatePositionFar();
        _currentHeroIndex = _selectedHeroIndex;
    }
    private void NextHero()
    {
        if (++_currentHeroIndex >= _visualHeroes.Count)
        {
            _currentHeroIndex = 0;
        }
        _buyUI.UpdateUI(_dataHeroes[_currentHeroIndex]);
        _camera.SetTarget(_visualHeroes[_currentHeroIndex]);
    }
    private void PreviousHero()
    {
        if (--_currentHeroIndex < 0)
        {
            _currentHeroIndex = _visualHeroes.Count - 1;
        }
        _buyUI.UpdateUI(_dataHeroes[_currentHeroIndex]);
        _camera.SetTarget(_visualHeroes[_currentHeroIndex]);
    }
    private void BuyHero()
    {
        int price = _dataHeroes[_currentHeroIndex].Price;
        if (_palyerData.SpendUsualCrystals(price))
        {
            _dataHeroes[_currentHeroIndex].IsBuy = true;
            _buyUI.UpdateUI(_dataHeroes[_currentHeroIndex]);
            Serializator.Serialize(_dataHeroes);
            Serializator.Serialize(DataName.CountCrystals, _palyerData.CountUsualCrystals);
        }
        else
        {
            _startUI.ShowNotification("You need " + price + " money");
        }
    }
    private void UpgradeHero()
    {
        int price = 0;
        HeroData currentHero = _dataHeroes[_currentHeroIndex];
        switch (_upgradeType)
        {
            case UpgradeType.Lifes:
                price = currentHero.GetLifesLevelsData[currentHero.LifesLevel + 1].GetPrice();
                if (_palyerData.SpendUsualCrystals(price))
                {
                    currentHero.LifesLevel++;
                }
                else
                {
                    _startUI.ShowNotification("You need " + price + " money");
                }
                break;
            case UpgradeType.Damage:
                price = currentHero.GetDamageLevelsData[currentHero.DamageLevel + 1].GetPrice();
                if (_palyerData.SpendUsualCrystals(price))
                {
                    currentHero.DamageLevel++;
                }
                else
                {
                    _startUI.ShowNotification("You need " + price + " money");
                }
                break;
            case UpgradeType.Shield:
                price = currentHero.GetShieldTimeLevelsData[currentHero.ShieldTimeLevel + 1].GetPrice();
                if (_palyerData.SpendUsualCrystals(price))
                {
                    currentHero.ShieldTimeLevel++;
                }
                else
                {
                    _startUI.ShowNotification("You need " + price + " money");
                }
                break;
            case UpgradeType.Rate:
                price = currentHero.GetRateLevelsData[currentHero.RateLevel + 1].GetPrice();
                if (_palyerData.SpendUsualCrystals(price))
                {
                    currentHero.RateLevel++;
                }
                else
                {
                    _startUI.ShowNotification("You need " + price + " money");
                }
                break;
        }
        Serializator.Serialize(_dataHeroes);
        _upgradeUI.UpdateUI(_dataHeroes[_currentHeroIndex], _upgradeType);
    }
    private void SelectHero()
    {
        _dataHeroes.ForEach(hero => hero.IsSelect = false);
        _dataHeroes[_currentHeroIndex].IsSelect = true;
        _buyUI.UpdateUI(_dataHeroes[_currentHeroIndex]);
        _selectedHeroIndex = _currentHeroIndex;
        Level.SetHeroIndex(_currentHeroIndex);
        Serializator.Serialize(_dataHeroes);
    }
    private void UpgradeHeroMode()
    {
        _buyUI.SetActive(false);
        _upgradeUI.SetActive(true);
        _upgradeUI.UpdateUI(_dataHeroes[_currentHeroIndex], _upgradeType);
    }
    private void BackToBuyMode()
    {
        _upgradeUI.SetActive(false);
        _buyUI.SetActive(true);
        _buyUI.UpdateUI(_dataHeroes[_currentHeroIndex]);
    }
    private void Resset()
    {
        Serializator.ResetValues();
        PlayerData.ResetValues();
        SceneManager.LoadScene(0);
    }
}
public enum UpgradeType
{
    Lifes,
    Damage,
    Shield,
    Rate,
}
