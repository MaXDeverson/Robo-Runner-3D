using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Serializator
{
    public static int GetLevel()
    {
        int levelIndex = PlayerPrefs.GetInt(DataName.CurrentLevel);
        if(levelIndex == 0)
        {
            levelIndex = 1;
            SetLevel(levelIndex);
        }
        return levelIndex;

    }
 

    public static void SetLevel(int level) => PlayerPrefs.SetInt(DataName.CurrentLevel,level);

    private class DataName
    {
        public const string CurrentLevel = "CurrentLevel";
    }
}
