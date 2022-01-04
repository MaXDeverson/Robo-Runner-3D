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
            new HeroData(true,true,0,1,1,new int[]{2,3,4},new int[]{1,2,3}),
            new HeroData(false,false,100,0,0,new int[]{4,5},new int[]{4,5,6}),
            new HeroData(false,false,200,0,0,new int[]{6,7,8},new int[]{7,8,9}),
            new HeroData(false,false,300,0,0,new int[]{9,10,12},new int[]{12,13,15})
    };
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
        catch(Exception ex)
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