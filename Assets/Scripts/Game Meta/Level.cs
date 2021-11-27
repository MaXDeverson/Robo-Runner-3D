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
    private bool _nextLoading;
    private void Awake()
    {
        _ui.SetHeroDestroyer(_heroDestroyer);
        _playerData = PlayerData.GetPlayerData();
        _ui.SetUpdateDataUsualCrystal(_playerData);
        _thingsCounter.SetListener(_playerData);
        currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        Serializator.SetData(DataName.CurrentLevel, currentLevelIndex);
        Debug.Log("Load Scene: " + currentLevelIndex);
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

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag(Tag.Player))
        {
            LoadNextScene();
        }
    }

    private void LoadNextScene()
    {
        if (!_nextLoading)
        {
            _nextLoading = true;
            _playerData.SaveResult();
            SceneManager.LoadScene(++currentLevelIndex);
        }
    }
}
