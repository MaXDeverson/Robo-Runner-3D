using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;


public static class Serializator
{
    private static string[] Names = { "CurrentLevel", "00dfdfdsfftddpdaccd45_nonf@1idable3$kk22", "023jjjdifnvdsd2%fisjj5ss1pjl^^f2iarddmad", "MaxContLives" };
    private const string FILE_NAME = "/gamedata.dat";
    private static List<HeroData> _initData = new List<HeroData>
    {
            new HeroData(true,true,0, 0,0,0,0),
            new HeroData(false,false,5000,0,0,0,0),
            new HeroData(false,false,1500,0,0,0,0),
            new HeroData(false,false,30000,0,0,0,0)
    };
    public static void InitializeHeroDataUpdates()
    {
        _initData[0].SetUpgradeData(
            new UpgradeLevel[] { new UpgradeLevel(3, 0), new UpgradeLevel(4, 200), new UpgradeLevel(5,500), new UpgradeLevel(6,700)}, //Lifes data
            new UpgradeLevel[] { new UpgradeLevel(1, 0), new UpgradeLevel(2, 550)}, //Damage data
            new UpgradeLevel[] { new UpgradeLevel(4, 0), new UpgradeLevel(5, 300), new UpgradeLevel(6, 450), }, //Shield data
            new UpgradeLevel[] { new UpgradeLevel(1, 0), new UpgradeLevel(2, 400), new UpgradeLevel(3, 700), });//Rate Data
        _initData[1].SetUpgradeData(
          new UpgradeLevel[] { new UpgradeLevel(5, 0), new UpgradeLevel(6, 800), new UpgradeLevel(7, 900), new UpgradeLevel(8,1000) }, //Lifes data
          new UpgradeLevel[] { new UpgradeLevel(3, 0), new UpgradeLevel(4, 1000), new UpgradeLevel(5, 1200), }, //Damage data
          new UpgradeLevel[] { new UpgradeLevel(6, 0), new UpgradeLevel(8, 500), new UpgradeLevel(10, 600), }, //Shield data
          new UpgradeLevel[] { new UpgradeLevel(1, 0), new UpgradeLevel(1, 700), new UpgradeLevel(1, 800), });//Rate Data
        _initData[2].SetUpgradeData(
          new UpgradeLevel[] { new UpgradeLevel(9, 0),new UpgradeLevel(10,1200), new UpgradeLevel(11, 1500), new UpgradeLevel(12, 2000), }, //Lifes data
          new UpgradeLevel[] { new UpgradeLevel(5, 0), new UpgradeLevel(6, 1500), new UpgradeLevel(7, 2000), }, //Damage data
          new UpgradeLevel[] { new UpgradeLevel(8, 0), new UpgradeLevel(9, 1450), new UpgradeLevel(10, 2100), }, //Shield data
          new UpgradeLevel[] { new UpgradeLevel(1, 0), new UpgradeLevel(1, 1), new UpgradeLevel(1, 1), });//Rate Data
        _initData[3].SetUpgradeData(
          new UpgradeLevel[] { new UpgradeLevel(12, 0), new UpgradeLevel(13, 3000), new UpgradeLevel(14, 5000), }, //Lifes data
          new UpgradeLevel[] { new UpgradeLevel(8, 0), new UpgradeLevel(10, 4000),new UpgradeLevel(12,7000), new UpgradeLevel(15, 10000), }, //Damage data
          new UpgradeLevel[] { new UpgradeLevel(1, 0), new UpgradeLevel(1, 5000), new UpgradeLevel(1, 1), }, //Shield data
          new UpgradeLevel[] { new UpgradeLevel(1, 0), new UpgradeLevel(1, 5000), new UpgradeLevel(1, 6000), });//Rate Data
    }
    public static int DeSerialize(DataName name)
    {
        int value = PlayerPrefs.GetInt(Names[(int)name]);
        //Additional conditions
        switch (name)
        {
            case DataName.CurrentLevel:
                if (value == 0)
                {
                    Serialize(name, 1);
                    return 1;
                }
                break;
        }
        return value;
    }
    public static void Serialize(DataName name, int value) => PlayerPrefs.SetInt(Names[(int)name], value);
    public static void ResetValues()
    {
        for (int i = 0; i < Names.Length; i++)
        {
            PlayerPrefs.SetInt(Names[i], 0);
        }
        Serialize(_initData);
    }
    public static void Serialize(List<HeroData> herosData)
    {
        try
        {
            using (Stream stream = File.Open(Application.persistentDataPath + FILE_NAME, FileMode.Create))
            {
                BinaryFormatter bformatter = new BinaryFormatter();
                bformatter.Serialize(stream, herosData);
            }
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }

    public static List<HeroData> DeSerialize()
    {
        List<HeroData> serialize = _initData;
        try
        {
            using (Stream stream = File.Open(Application.persistentDataPath + FILE_NAME, FileMode.Open))
            {
                BinaryFormatter bformatter = new BinaryFormatter();
                serialize = (List<HeroData>)bformatter.Deserialize(stream);
            }
        }
        catch (Exception ex)
        {
            Debug.Log("Data NO deserialize" + ex.Message);
        }
        return serialize;
    }
}
public enum DataName
{
    CurrentLevel,
    CountCrystals,
    CountECrystals,
    MaxCounLives,
}