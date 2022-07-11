using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UILogic : MonoBehaviour
{
    [SerializeField] private List<Transform> _visualHeroes;
    [SerializeField] private CameraBuy _camera;
    [SerializeField] private BuyUI _buyUI;
    [SerializeField] private StartUI _startUI;
    [SerializeField] private StartUI_2 _startUI_2;
    [SerializeField] private UpgradeUI _upgradeUI;
    private List<HeroData> _dataHeroes;
    private PlayerData _playerData;
    private int _selectedHeroIndex;
    private int _currentHeroIndex;
    private SettingsData _settingsData;
    //
    private UpgradeType _upgradeType;
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
        _playerData = PlayerData.GetPlayerData();
        InitializationDataOfHero();
        InitializationUI();
        _camera.SetTarget(_visualHeroes[_selectedHeroIndex], false);
        _playerData.SetChangeCounUCystal((usualCont) => _startUI.UpdateUsualCrystals(usualCont));
        _playerData.SetChangeCounECrystal((count) => _startUI.UpdateElectroCrysals(count));
        _settingsData = Serializator.DeSerializeSettings();
        _startUI.InitializeSettings(_settingsData.LevelSoundValue,_settingsData.Sensitivity,_settingsData.Quality,_settingsData.ShowFPS);
    }
    private void InitializationUI()
    {
        _buyUI.AddActionNext(NextHero);
        _buyUI.AddActionSelect(() => SelectHero());
        _buyUI.AddActionBack(BackToUI);
        _buyUI.AddActionBack(() => _startUI_2.PlayStatsWindowAnimation(StartUI_2.StatsWindowAnimation.First));
        _buyUI.AddActionPrevious(PreviousHero);
        _buyUI.AddActionBuy(BuyHero);
        _buyUI.AddActionUpgrade(() => StartCoroutine(UpgradeHeroMode()));
        _buyUI.AddActionUpgrade(() => _camera.AnimateRotate());
        _buyUI.AddActionUpgrade(() => _startUI_2.PlayStatsWindowAnimation(StartUI_2.StatsWindowAnimation.Third));

        _startUI.AddActionUpgrade(() => _buyUI.SetActive(true));
        _startUI.AddActionUpgrade(() => _buyUI.UpdateUI(_dataHeroes[_currentHeroIndex]));
        _startUI.AddActionUpgrade(_camera.AnimatePositionNear);
        _startUI.AddActionUpgrade(() =>_startUI_2.PlayStatsWindowAnimation(StartUI_2.StatsWindowAnimation.Second));
        _startUI.AddActionResset(Resset);
        _startUI.AddActionPlay(() => _startUI_2.UpdateUI(_dataHeroes[_currentHeroIndex], _dataHeroes[_dataHeroes.Count - 1], _playerData.KillsCount));
        _startUI_2.UpdateUI(_dataHeroes[_currentHeroIndex], _dataHeroes[_dataHeroes.Count-1],_playerData.KillsCount);

        _upgradeUI.AddActionBack(() => _startUI_2.PlayStatsWindowAnimation(StartUI_2.StatsWindowAnimation.Second));
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
        _startUI_2.UpdateUI(_dataHeroes[_currentHeroIndex], _dataHeroes[_dataHeroes.Count - 1], _playerData.KillsCount);
        _buyUI.UpdateUI(_dataHeroes[_currentHeroIndex]);
        _camera.SetTarget(_visualHeroes[_currentHeroIndex]);
    }
    private void PreviousHero()
    {
        if (--_currentHeroIndex < 0)
        {
            _currentHeroIndex = _visualHeroes.Count - 1;
        }
        _startUI_2.UpdateUI(_dataHeroes[_currentHeroIndex], _dataHeroes[_dataHeroes.Count - 1], _playerData.KillsCount);
        _buyUI.UpdateUI(_dataHeroes[_currentHeroIndex]);
        _camera.SetTarget(_visualHeroes[_currentHeroIndex]);
    }
    private void BuyHero()
    {
        int price = _dataHeroes[_currentHeroIndex].Price;
        if (_playerData.SpendUsualCrystals(price))
        {
            _dataHeroes[_currentHeroIndex].IsBuy = true;
            _buyUI.UpdateUI(_dataHeroes[_currentHeroIndex]);
            _startUI_2.UpdateUI(_dataHeroes[_currentHeroIndex], _dataHeroes[_dataHeroes.Count - 1], _playerData.KillsCount);
            Serializator.Serialize(_dataHeroes);
            Serializator.Serialize(DataName.CountCrystals, _playerData.CountUsualCrystals);
        }
        else
        {
            _startUI.ShowNotification("You need " + (price - _playerData.CountUsualCrystals) + " money");
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
                if (_playerData.SpendUsualCrystals(price))
                {
                    currentHero.LifesLevel++;
                }
                else
                {
                    _startUI.ShowNotification("You need " + (price - _playerData.CountUsualCrystals) + " money");
                }
                break;
            case UpgradeType.Damage:
                price = currentHero.GetDamageLevelsData[currentHero.DamageLevel + 1].GetPrice();
                if (_playerData.SpendUsualCrystals(price))
                {
                    currentHero.DamageLevel++;
                }
                else
                {
                    _startUI.ShowNotification("You need " + (price - _playerData.CountUsualCrystals) + " money");
                }
                break;
            case UpgradeType.Shield:
                price = currentHero.GetShieldTimeLevelsData[currentHero.ShieldTimeLevel + 1].GetPrice();
                if (_playerData.SpendUsualCrystals(price))
                {
                    currentHero.ShieldTimeLevel++;
                }
                else
                {
                    _startUI.ShowNotification("You need " + (price - _playerData.CountUsualCrystals) + " money");
                }
                break;
            case UpgradeType.Rate:
                price = currentHero.GetRateLevelsData[currentHero.RateLevel + 1].GetPrice();
                if (_playerData.SpendUsualCrystals(price))
                {
                    currentHero.RateLevel++;
                }
                else
                {
                    _startUI.ShowNotification("You need " + (price - _playerData.CountUsualCrystals) + " money");
                }
                break;
        }
        Debug.Log("Serialize Hero upgrade data");
        Serializator.Serialize(_dataHeroes);
        Serializator.Serialize(DataName.CountCrystals,_playerData.CountUsualCrystals);
        _upgradeUI.UpdateUI(_dataHeroes[_currentHeroIndex], _upgradeType);
        _startUI_2.UpdateUI(_dataHeroes[_currentHeroIndex], _dataHeroes[_dataHeroes.Count - 1], _playerData.KillsCount);
    } 
    private void SelectHero()
    {
        _dataHeroes.ForEach(hero => hero.IsSelect = false);
        _dataHeroes[_currentHeroIndex].IsSelect = true;
        _buyUI.UpdateUI(_dataHeroes[_currentHeroIndex]);
        _startUI_2.UpdateUI(_dataHeroes[_currentHeroIndex], _dataHeroes[_dataHeroes.Count - 1], _playerData.KillsCount);
        _selectedHeroIndex = _currentHeroIndex;
        Level.SetHeroIndex(_currentHeroIndex);
        Serializator.Serialize(_dataHeroes);
    }
    private IEnumerator UpgradeHeroMode()
    {
        _buyUI.SetActive(false);
        yield return new WaitForSeconds(0.5f);//0.5 = camera animation time
        _upgradeUI.SetActive(true);
        _upgradeUI.UpdateUI(_dataHeroes[_currentHeroIndex], _upgradeType);
    }
    private void BackToBuyMode()
    {
        _upgradeUI.SetActive(false);
        _buyUI.SetActive(true);
        _buyUI.UpdateUI(_dataHeroes[_currentHeroIndex]);
        _startUI_2.UpdateUI(_dataHeroes[_currentHeroIndex], _dataHeroes[_dataHeroes.Count - 1], _playerData.KillsCount);
    }
    private void Resset()
    {
        Serializator.ResetValues();
        PlayerData.ResetValues();
        SceneManager.LoadScene(0);
    }
    private void OnApplicationQuit()
    {
        _playerData.SaveData();
        _settingsData.LevelSoundValue = _startUI.LevelSound;
        _settingsData.Sensitivity = _startUI.Sencetivity;
        _settingsData.Quality = _startUI.Quality;
        _settingsData.ShowFPS = _startUI.ShowFps;
        Serializator.SerializeSettings();
    }
}
public enum UpgradeType
{
    Lifes,
    Damage,
    Shield,
    Rate,
}
