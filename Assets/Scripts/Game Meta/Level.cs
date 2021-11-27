using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] private UI _ui;
    [SerializeField] private HeroDestroyer _heroDestroyer;
    [SerializeField] private ThingsCounter _thingsCounter;
    public static Level CurrentLevel;
    private PlayerData _playerData;
    private int currentLevelIndex;
    private void Awake()
    {
        _ui.SetHeroDestroyer(_heroDestroyer);
        _playerData = PlayerData.GetPlayerData();
        _ui.SetUpdateDataUsualCrystal(_playerData);
        _thingsCounter.SetListener(_playerData);
        currentLevelIndex = Serializator.GetLevel();
        CurrentLevel = this;
    }
    private void Start()
    {
        
    }
    private void Update()
    {
        
    }

    public void Restart()
    {
        SceneManager.LoadScene(currentLevelIndex);
    }
}
