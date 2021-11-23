using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private UI _ui;
    [SerializeField] private HeroDestroyer _heroDestroyer;
    [SerializeField] private ThingsCounter _thingsCounter;
    private PlayerData _playerData;

    private void Awake()
    {
        _ui.SetHeroDestroyer(_heroDestroyer);
        _playerData = PlayerData.GetPlayerData();
        _ui.SetUpdateDataUsualCrystal(_playerData);
        _thingsCounter.SetListener(_playerData);
    }
    private void Start()
    {
        
    }
    private void Update()
    {
        
    }
}
