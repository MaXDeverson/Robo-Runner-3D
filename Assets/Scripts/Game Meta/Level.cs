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
        CurrentLevel = this;
    }
    private void InitHero()
    {
            Transform hero = Instantiate(_heroes[_indexHero], _startHero.position, Quaternion.identity);
            Hero = hero;
            Destroy(_startHero.gameObject);
            hero.GetComponent<HeroMover>().SetMoverLogic(_moverLogic);
            _camera.SetFollowObj(hero);
            _heroDestroyer = hero.GetComponent<HeroDestroyer>();
            _thingsCounter = hero.GetComponent<ThingsCounter>();
            _heroShield = hero.GetComponent<Shield>();
    }
    private void UIInitialization()
    {
        _ui.SetHeroDestroyer(_heroDestroyer);
        _ui.SetUpdateDataUsualCrystal(_playerData);
        _ui.SetShieldModeEvent(() =>
        {
            if(!_heroShield.ShieldIsActive() && _playerData.SpendElectroCrystals(1))
                _heroShield.SetActive(true);
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
            _playerData.SaveResult();
            _ui.ShowLoadView();
            SceneManager.LoadScene(++currentLevelIndex);
        }
    }
}
