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
    public Transform Hero { get; private set; }
    private PlayerData _playerData;
    private int currentLevelIndex;
    private bool _nextLoading;
    private static int _indexHero;
    public static void SetHeroIndex(int index) => _indexHero = index;
    private void Awake()
    {
        InitHero();
        _playerData = PlayerData.GetPlayerData();
        UIInitialization();
        _thingsCounter.SetListener(_playerData);
        currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        Serializator.SetData(DataName.CurrentLevel, currentLevelIndex);
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
            _heroShield.SetActive(true);
        });
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
