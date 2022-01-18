using System;
[Serializable]
public class HeroData
{
    public int LifesCount { get => _lifesLevelsData[LifesLevel].GetValue(); }
    public int DamageCount { get => _damageLevelsData[DamageLevel].GetValue(); }
    public int ShieldTimeCount { get => _shieldTimeLevelsData[ShieldTimeLevel].GetValue(); }
    public int RateCount { get => _rateLevelsData[RateLevel].GetValue();}
    //stats
    public bool IsSelect { get; set; }
    public bool IsBuy {get; set; }
    public int Price { get; set; }
    public  int LifesLevel { get; set; }
    public int DamageLevel { get; set; }
    public int ShieldTimeLevel { get; set; }
    public int RateLevel { get; set; }

    private UpgradeLevel[] _lifesLevelsData;
    private UpgradeLevel[] _damageLevelsData;
    private UpgradeLevel[] _shieldTimeLevelsData;
    private UpgradeLevel[] _rateLevelsData;

    public HeroData(bool isBuy,bool isSelect,int price,int lifesLevel,int damageLevel,int shieldLevel,int rateLevel)          
    {
        IsBuy = isBuy;
        IsSelect = isSelect;
        Price = price;
        LifesLevel = lifesLevel;
        DamageLevel = damageLevel;
        ShieldTimeLevel = shieldLevel;
        RateLevel = rateLevel;
    }
    public void SetUpgradeData(UpgradeLevel[] lifesLevelsData, UpgradeLevel[] damageLevelsData, UpgradeLevel[] shieldLevelsData, UpgradeLevel[] rateLevelsData)
    {
       
        _lifesLevelsData = lifesLevelsData;
        _damageLevelsData = damageLevelsData;
        _shieldTimeLevelsData = shieldLevelsData;
        _rateLevelsData = rateLevelsData;
    }

    public UpgradeLevel[] GetLifesLevelsData => _lifesLevelsData;
    public UpgradeLevel[] GetDamageLevelsData => _damageLevelsData;
    public UpgradeLevel[] GetShieldTimeLevelsData => _shieldTimeLevelsData;
    public UpgradeLevel[] GetRateLevelsData => _rateLevelsData;
}

[Serializable]
public struct UpgradeLevel
{
    private byte _value;
    private int _price;

    public UpgradeLevel(byte value, int price)
    {
        _value = value;
        _price = price;
    }

    public int GetPrice() => _price;
    public byte GetValue() => _value;
}
