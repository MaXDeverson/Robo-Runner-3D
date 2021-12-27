using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    private Action<int> _changeCountUsualCrystalAction;
    private Action<int> _changeCountElectroCrystalAction;
    private int _countUsualCrystals;
    private int _countElectroCrystals;
    private int _maxCountLifes;//in future;

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
    private PlayerData(int countUCrystals,int countECrystal,int maxCountLifes)
    {
        _countUsualCrystals = countUCrystals;
        _countElectroCrystals = countECrystal;
        _maxCountLifes = maxCountLifes;
        _changeCountUsualCrystalAction?.Invoke(_countUsualCrystals);
    }
    public void AddUsualCrystals()
    {
        _countUsualCrystals ++;
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

    public void SaveResult()
    {
        Serializator.SetData(DataName.CountCrystals, _countUsualCrystals);
        Serializator.SetData(DataName.CountECrystals, _countElectroCrystals);
    }

    public static PlayerData GetPlayerData()
    {
        int countUCrystals = Serializator.GetData(DataName.CountCrystals);
        int countECrystals = Serializator.GetData(DataName.CountECrystals);
        int maxCountLifes = Serializator.GetData(DataName.MaxCounLives);
        //Geting data with seraialization;
        return new PlayerData(countUCrystals,countECrystals,maxCountLifes);
    }
}
