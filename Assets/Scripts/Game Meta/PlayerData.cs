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
    public int CountUsualCrystals { get => _countUsualCrystals; }
    public int CountElectroCrystals { get => _countElectroCrystals; }

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
    private PlayerData(int countUCrystals,int countECrystal)
    {
        _countUsualCrystals = countUCrystals;
        _countElectroCrystals = countECrystal;
        _changeCountUsualCrystalAction?.Invoke(_countUsualCrystals);
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
    }
    public static PlayerData GetPlayerData()
    {
        if(_playerData == null)
        {
            bool isFirstLaunch = Serializator.IsFirstLaunching();
            int countUCrystals = isFirstLaunch?1000: Serializator.DeSerialize(DataName.CountCrystals);
            int countECrystals = isFirstLaunch?1: Serializator.DeSerialize(DataName.CountECrystals);
            if (isFirstLaunch)
            {
              
                Serializator.Serialize(DataName.CountCrystals, countUCrystals);
                Serializator.Serialize(DataName.CountECrystals, countECrystals);
            }
            _playerData = new PlayerData(countUCrystals, countECrystals);
            return _playerData;
        }
        else
        {
            return _playerData;
        }

    }
    public static void ResetValues()
    {
        _playerData = null;
        ///1000 and 10 start values of crustals
    }

}
