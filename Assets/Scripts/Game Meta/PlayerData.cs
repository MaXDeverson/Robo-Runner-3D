using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public Action<int> ChangeCountUsualCrystalAction;
    public Action<int> ChangeCountElectroCrystalAction;
    private int _countUsualCrystals;
    private int _countElectroCrystals;
    private int _maxCountLifes;//in future;

    private PlayerData(int countUCrystals,int countECrystal,int maxCountLifes)
    {
        _countUsualCrystals = countECrystal;
        _countElectroCrystals = countECrystal;
        _maxCountLifes = maxCountLifes;
    }
    public void AddUsualCrystals()
    {
        _countUsualCrystals ++;
        ChangeCountUsualCrystalAction.Invoke(_countUsualCrystals);
    }
    public void AddElectroCrystal()
    {
        _countElectroCrystals++;
        ChangeCountElectroCrystalAction.Invoke(_countElectroCrystals);
    }

    public static PlayerData GetPlayerData()
    {
        int countUCrystals = 0;
        int countECrystals = 0;
        int maxCountLifes = 3;
        //Geting data with seraialization;
        return new PlayerData(countUCrystals,countECrystals,maxCountLifes);
    }
}
