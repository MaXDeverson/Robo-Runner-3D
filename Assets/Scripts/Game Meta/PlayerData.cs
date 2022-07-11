using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    private static PlayerData _playerData;
    private Action<int> _changeCountUsualCrystalAction;
    private Action<int> _changeCountElectroCrystalAction;
    private int _countUsualCrystals;
    private int _countElectroCrystals;
    private int _killsCount;
    private int _shieldUseCount;
    private int _continueCount;
    private List<AchieveItemData> _achieveItemDatas;
    public int CountUsualCrystals { get => _countUsualCrystals; }
    public int CountElectroCrystals { get => _countElectroCrystals; }
    public int KillsCount { get => _killsCount; }
    public int ShieldUseCount { get => _shieldUseCount; }
    public int ContiuneCount { get => _continueCount; }
    public void SetChangeCounUCystal(Action<int> action)
    {
        _changeCountUsualCrystalAction += action;
        _changeCountUsualCrystalAction?.Invoke(_countUsualCrystals);
    }
    public void SetChangeCounECrystal(Action<int> action)
    {
        _changeCountElectroCrystalAction += action;
        _changeCountElectroCrystalAction?.Invoke(_countElectroCrystals);
    }
    private PlayerData(int countUCrystals, int countECrystal, int countKills,int shieldUseCount, int contiuneUseCount, List<AchieveItemData> achieveItemDatas)
    {
        _countUsualCrystals = countUCrystals;
        _countElectroCrystals = countECrystal;
        _changeCountUsualCrystalAction?.Invoke(_countUsualCrystals);
        _killsCount = countKills;
        _achieveItemDatas = achieveItemDatas;
        _shieldUseCount = shieldUseCount;
        _continueCount = contiuneUseCount;
    }
    public void AddUsualCrystals(int count = 1)
    {
        _countUsualCrystals += count;
        _changeCountUsualCrystalAction.Invoke(_countUsualCrystals);
    }
    public void AddElectroCrystal()
    {
        _countElectroCrystals++;
        _changeCountElectroCrystalAction.Invoke(_countElectroCrystals);
    }
    public bool SpendElectroCrystals(int count)
    {
        if(_countElectroCrystals < count)
        {
            return false;
        }
        else
        {
            _countElectroCrystals -= count;
            _changeCountElectroCrystalAction.Invoke(_countElectroCrystals);
            return true;
        }
    }
    public bool SpendUsualCrystals(int count)
    {
        if (_countUsualCrystals < count)
        {
            return false;
        }
        else
        {
            _countUsualCrystals -= count;
            _changeCountUsualCrystalAction?.Invoke(_countUsualCrystals);
            return true;
        }
    }
    public void SaveData()
    {
        Serializator.Serialize(DataName.CountCrystals, _countUsualCrystals);
        Serializator.Serialize(DataName.CountECrystals, _countElectroCrystals);
        Serializator.Serialize(DataName.KillsCount, _killsCount);
        Serializator.Serialize(DataName.ContiuneCount, _continueCount);
        Serializator.Serialize(DataName.ShieldUseCount, _shieldUseCount);
        Serializator.SerializeAchievement();
        //Debug.Log("Save contiune count:" + _continueCount + " shield count:" + _shieldUseCount);
    }
    public static PlayerData GetPlayerData()
    {
        bool isFirstLaunch = Serializator.IsFirstLaunching();
        if (_playerData == null)
        {
            int countUCrystals = isFirstLaunch?1000: Serializator.DeSerialize(DataName.CountCrystals);
            int countECrystals = isFirstLaunch?1: Serializator.DeSerialize(DataName.CountECrystals);
            int killsCount = Serializator.DeSerialize(DataName.KillsCount);
            int shieldUseCount = Serializator.DeSerialize(DataName.ShieldUseCount);
            int contiuneUseCount = Serializator.DeSerialize(DataName.ContiuneCount);
           
            if (isFirstLaunch)
            {
              
                Serializator.Serialize(DataName.CountCrystals, countUCrystals);
                Serializator.Serialize(DataName.CountECrystals, countECrystals);
               
            }
            _playerData = new PlayerData(countUCrystals, countECrystals,killsCount,shieldUseCount,contiuneUseCount,Serializator.AchievementItemsInitData);
            return _playerData;
        }
        else
        {
            return _playerData;
        }

    }
    public static void ResetValues()
    {
        Serializator.ResetValues();
        _playerData = new PlayerData(1000, 1, 0, 0, 0, Serializator.AchievementItemsInitData);
        ///1000 and 10 start values of crustals
    }
    public void AddKill() => _killsCount++;
    public void CountShieldUse() => _shieldUseCount++;
    public void CountContiuneUse() => _continueCount++;
}
