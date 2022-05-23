using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] private UI _ui;
    [SerializeField] private HeroDestroyer _heroDestroyer;
    [SerializeField] private ThingsCounter _thingsCounter;
    [SerializeField] private Shield _heroShield;
    [SerializeField] private MoverLogic _moverLogic;
    [SerializeField] private List<Transform> _heroes;
    [SerializeField] private Transform _startHero;
    [SerializeField] private CameraController _camera;
    public static Level CurrentLevel;
    private HeroData _heroData;
    public Transform Hero { get; private set; }
    public void SetDestroyAction(Action action)
    {
        _heroDestroyer.DieAction += action;
    }
    public HeroData HeroData => _heroData;
    private PlayerData _playerData;

    private int currentLevelIndex;
    private bool _nextLoading;
    private static int _indexHero;
    public static void SetHeroIndex(int index) => _indexHero = index;
    private void Awake()
    {
        InitHero();
        _playerData = PlayerData.GetPlayerData();
        _heroData = Serializator.DeSerialize()[_indexHero];
        UIInitialization();
        _thingsCounter.SetListener(_playerData);
        currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        Serializator.Serialize(DataName.CurrentLevel, currentLevelIndex);
        _ui.AddActionExit(OnApplicationQuit);
        CurrentLevel = this;
    }

    private void OnApplicationQuit()
    {
        _playerData.SaveData();
    }

    private void InitHero()
    {
            Hero = Instantiate(_heroes[_indexHero], _startHero.position, Quaternion.identity);
            Destroy(_startHero.gameObject);
            Hero.GetComponent<HeroMover>().SetMoverLogic(_moverLogic);
            _camera.SetFollowObj(Hero);
            _heroDestroyer = Hero.GetComponent<HeroDestroyer>();
            _thingsCounter = Hero.GetComponent<ThingsCounter>();
            _heroShield = Hero.GetComponent<Shield>();
        
    }
    private void Start()
    {
        _heroDestroyer.SetCountLifes(HeroData.LifesCount);
        Hero.GetComponent<ManagerAnimation>().SetRateValue(HeroData.RateCount);
    }
    private void UIInitialization()
    {
        _ui.SetHeroDestroyer(_heroDestroyer);
        _ui.SetUpdateDataUsualCrystal(_playerData);
        _ui.SetShieldModeEvent(() =>
        {
            if(!_heroShield.ShieldIsActive() && _playerData.SpendElectroCrystals(1))
            {
                _heroShield.SetActive(true);
                _ui.ShowShieldTime(_heroShield.ShieldTimeActive);
            }
                
        });
    }
    public void Restart()
    {
        Serializator.Serialize(DataName.CountCrystals, _playerData.CountUsualCrystals);
        Serializator.Serialize(DataName.CountECrystals, _playerData.CountElectroCrystals);
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
            _playerData.SaveData();
            _ui.ShowLoadView();
            SceneManager.LoadScene(++currentLevelIndex);
        }
    }

}
