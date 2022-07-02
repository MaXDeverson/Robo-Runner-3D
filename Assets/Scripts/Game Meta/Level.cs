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
    [SerializeField] public SoundList SoundList;
    [SerializeField] public AudioSourses AudioSourses;
    private List<EnemyMoverLogic> _enemyMoverLogics = new List<EnemyMoverLogic>();
    public static Level CurrentLevel;
    private HeroData _heroData;
    public Transform Hero { get; private set; }
    private int _countECrystalsOnLevel;
    private bool _hadRestart;
    private bool _levelIsFinish;

    /// <summary>
    /// The int parameter in action is for collected electro crystals
    /// </summary>
    /// <param name="action"></param>
    public void SetDieAction(Action<int> action)
    {
        _heroDestroyer.DieAction += () => action?.Invoke(_countECrystalsOnLevel);
    }
    public void Contiune()
    {
        Vector3 previousHeroPosition = Hero.position;
        InitHero(previousHeroPosition + new Vector3(0, 5, -2));
        _playerData = PlayerData.GetPlayerData();
        _heroData = Serializator.DeSerialize()[_indexHero];
        _thingsCounter.SetListener(_playerData);
        Debug.Log("Lifes count: " + _heroData.LifesCount);
        currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        ////
        ///
        _heroDestroyer.SetCountLifes(1);
        Hero.GetComponent<ManagerAnimation>().SetRateValue(HeroData.RateCount);
        ///
        _ui.AddActionExit(OnApplicationQuit);
        UIInitialization();
        _heroShield.Activate(2);
        _enemyMoverLogics.ForEach(x => x.SetAim(Hero));
        _playerData.CountContiuneUse();
    }
    public void AddEnemyMoverLogic(EnemyMoverLogic logic) => _enemyMoverLogics.Add(logic);
    public HeroData HeroData => _heroData;
    private PlayerData _playerData;
    private int currentLevelIndex;
    private bool _nextLoading;
    private static int _indexHero;
    public static void SetHeroIndex(int index) => _indexHero = index;
    private void Awake()
    {
        Destroy(_startHero.gameObject);
        InitHero(_startHero.position);
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
    private void InitHero(Vector3 heroPosition)
    {
        Hero = Instantiate(_heroes[_indexHero], heroPosition, Quaternion.identity);
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
        _thingsCounter.GetECrystalAction += () =>
        {
            _countECrystalsOnLevel++;
        };
        //Upgrade Notification
        StartCoroutine(CheckUpgrade());
    }
    private IEnumerator CheckUpgrade()
    {
        yield return new WaitForSeconds(1f);
        if (currentLevelIndex > 3)
        {
            if (_heroData.RateLevel + 1 < _heroData.GetRateLevelsData.Length &&
                _playerData.CountUsualCrystals >= _heroData.GetRateLevelsData[_heroData.RateLevel + 1].GetPrice())
            {
                _ui.ShowNotification("You can upgrade a level of shoot RATE! for " + _heroData.GetRateLevelsData[_heroData.RateLevel + 1].GetPrice());
            }
            else if (_heroData.DamageLevel + 1 < _heroData.GetDamageLevelsData.Length &&
                _playerData.CountUsualCrystals >= _heroData.GetDamageLevelsData[_heroData.DamageLevel + 1].GetPrice())
            {
                _ui.ShowNotification("You can upgrade a level of DAMAGE! for " + _heroData.GetDamageLevelsData[_heroData.DamageLevel + 1].GetPrice());
            }
            else if (_heroData.LifesLevel + 1 < _heroData.GetLifesLevelsData.Length &&
                _playerData.CountUsualCrystals >= _heroData.GetLifesLevelsData[_heroData.LifesLevel + 1].GetPrice())
            {

                _ui.ShowNotification("You can upgrade a level of count LIFES! for " + _heroData.GetLifesLevelsData[_heroData.LifesLevel + 1].GetPrice());

            }
            else if (_heroData.ShieldTimeLevel + 1 < _heroData.GetShieldTimeLevelsData.Length && _playerData.CountUsualCrystals >= _heroData.GetShieldTimeLevelsData[_heroData.ShieldTimeLevel + 1].GetPrice())
            {

                _ui.ShowNotification("You can upgrade a level of SHIELD time for " + _heroData.GetShieldTimeLevelsData[_heroData.ShieldTimeLevel + 1].GetPrice());
            }
        }
    }
    private void UIInitialization()
    {
        _ui.SetHeroDestroyer(_heroDestroyer);
        _ui.SetUpdateDataUsualCrystal(_playerData);
        _ui.SetShieldModeEvent(() =>
        {
            if (!_heroShield.ShieldIsActive() && _playerData.SpendElectroCrystals(1))
            {
                _heroShield.SetActive(true);
                _ui.ShowShieldTime(_heroShield.ShieldTimeActive);
            }

        });
    }
    public void Restart()
    {
        _playerData.SpendElectroCrystals(_countECrystalsOnLevel);
        _hadRestart = true;
        Serializator.Serialize(DataName.CountCrystals, _playerData.CountUsualCrystals);
        Serializator.Serialize(DataName.CountECrystals, _playerData.CountElectroCrystals);
        SceneManager.LoadScene(currentLevelIndex);


    }
    private void OnDestroy()
    {
        if (!_hadRestart && !_levelIsFinish) _playerData.SpendElectroCrystals(_countECrystalsOnLevel);
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
            _levelIsFinish = true;
            _playerData.SaveData();
            _ui.ShowLoadView();
            SceneManager.LoadScene(++currentLevelIndex);
        }
    }
}
