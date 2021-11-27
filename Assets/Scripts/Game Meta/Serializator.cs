using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Serializator
{
    private static string [] Names = {"CurrentLevel","CountCrystals","CountECystals","MaxContLives"};

    public static int GetData(DataName name)
    {
        int value = PlayerPrefs.GetInt(Names[(int)name]);
        //Additional conditions
        switch (name)
        {
            case DataName.CurrentLevel:
                if (value == 0)
                {
                    SetData(name, 1);
                    return 1;
                }
                break;
        }
        return value;
    }
    public static void SetData(DataName name, int value) => PlayerPrefs.SetInt(Names[(int)name], value);

    public static void ResetValues()
    {
        for(int i = 0; i < Names.Length; i++)
        {
            PlayerPrefs.SetInt(Names[i], 0);
        }
    }
}
public enum DataName
{
    CurrentLevel,
    CountCrystals,
    CountECrystals,
    MaxCounLives,
}