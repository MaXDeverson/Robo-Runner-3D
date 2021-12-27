using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroData
{
    public bool IsSelect { get; set; }
    public bool IsBuy {get; private set; }
    public int Price { get; set; }
    public  int LifesLevel { get; set; }
    public int DamageLevel { get; set; }

    private int[] _lifesLevelsData;
    private int[] _damageLevelsData;
    
    public HeroData(bool isBuy,bool isSelect,int price,int lifesLevel,int damageLevel,int[] lifesLevelsData, int[] damageLevelsData)
    {
        IsBuy = isBuy;
        IsSelect = isSelect;
        Price = price;
        LifesLevel = lifesLevel;
        DamageLevel = damageLevel;
        _lifesLevelsData = lifesLevelsData;
        _damageLevelsData = damageLevelsData;
    }
}
