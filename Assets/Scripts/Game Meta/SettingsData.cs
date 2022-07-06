using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsData
{
    public float LevelSoundValue { get => _levelSoundValue; set => _levelSoundValue = value; }
    public float Sensitivity { get => _sensitivity; set => _sensitivity = value; }
    public int Quality { get => _quality; set => _quality = value; }
    public bool ShowFPS { get => _showFPS; set => _showFPS = value; }

    private float _levelSoundValue;   
    private float _sensitivity;
    private int _quality;
    private bool _showFPS;

    public SettingsData(float levelSoundValue, float sensitivity, int quality,bool showFPS)
    {
        _levelSoundValue = levelSoundValue;
        _sensitivity = sensitivity;
        _quality = quality;
        _showFPS = showFPS;
    }
}
