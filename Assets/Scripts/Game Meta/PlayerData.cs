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

    public void SaveResult()
    {
        Serializator.Serialize(DataName.CountCrystals, _countUsualCrystals);
        Serializator.Serialize(DataName.CountECrystals, _countElectroCrystals);
    }

    public static PlayerData GetPlayerData()
    {
        int countUCrystals = Serializator.DeSerialize(DataName.CountCrystals);
        int countECrystals = Serializator.DeSerialize(DataName.CountECrystals);
        //Geting data with seraialization;
        return new PlayerData(countUCrystals,countECrystals);
    }
}
