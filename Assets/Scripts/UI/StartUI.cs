using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Api;

public class StartUI : INotifible
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _reset;
    [SerializeField] private GameObject _loadObjects;
    [SerializeField] private Text _logText;
    [SerializeField] private Button _upgradeButton;
    [SerializeField] private BuyUI _buyUI;
    [SerializeField] private Button _info;
    [SerializeField] private Button _hideInfo;
    [SerializeField] private GameObject _informationObj;
    [Header("Crystals")]
    [SerializeField] private TextMeshProUGUI _textUsualCrystals;
    [SerializeField] private TextMeshProUGUI _textElectroCrystals;
    [SerializeField] private Button _getCrystals;
    [Header("Cheat")]
    [SerializeField] private Button _addMoney;
    [SerializeField] private Text _moneyCount;
    [SerializeField] private GameObject _cheatObj;
    [SerializeField] private Text _currentLevel;
    [Header("Settings")]
    [SerializeField] private GameObject _settingsWindow;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _hideSettings;
    [SerializeField] private Slider _levelSound;
    [SerializeField] private Slider _sencetivity;
    [SerializeField] private Dropdown _quality;
    [SerializeField] private Toggle _showFps;
    [Header("Notification")]
    [SerializeField] private TextMeshProUGUI _notificationText;
    [SerializeField] private GameObject _notifiWindow;
    [SerializeField] private Transform _startPositon;
    [SerializeField] private Transform _finishAniamtionPosition;
    [Header("Sound")]
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private SoundList _soundList;
    private bool _inProcess;
    //Crystal animation
    private int _crystalCountAnimation;
    private int _counter;
    private bool _cryatalAnimationInProcess;
    private bool _crystalIncrease;
    private int step = 50;
    private bool _crystalWithAnimation;
    //Settings
    public float LevelSound { get => _levelSound.value; }
    public float Sencetivity { get => _sencetivity.value; }
    public int Quality { get => _quality.value; }
    public bool ShowFps { get => _showFps.isOn; }
    //add
    private InterstitialAd _add;
    private string _addId = "ca-app-pub-9558178408201758/8556519009";

    void Start()
    {
        PlayerData data = PlayerData.GetPlayerData();
        _addMoney.onClick.AddListener(() =>
        {
            data.AddUsualCrystals(int.Parse(_moneyCount.text));
            _textUsualCrystals.text = data.CountUsualCrystals + "";
            Serializator.Serialize(DataName.CountCrystals, data.CountUsualCrystals);
        });
        //cheat
        _buyUI.gameObject.SetActive(false);
        _playButton.onClick.AddListener(() =>
        {
            if (int.TryParse(_currentLevel.text, out int result))
            {
                SceneManager.LoadScene(result);
            }
            else
            {
                SceneManager.LoadScene(Serializator.DeSerialize(DataName.CurrentLevel));
                _loadObjects.SetActive(true);
            }
        });
        _info.onClick.AddListener(() => _informationObj.SetActive(true));
        _hideInfo.onClick.AddListener(() => _informationObj.SetActive(false));
        AddActionUpgrade(() =>
        {
            _buyUI.gameObject.SetActive(true);
            SetActiveUI(false);
        });
        //player data initialization;
        _textUsualCrystals.text = data.CountUsualCrystals + "";
        _textElectroCrystals.text = data.CountElectroCrystals + "";
        //settings init
        _settingsButton.onClick.AddListener(() => _settingsWindow.SetActive(true));
        _hideSettings.onClick.AddListener(() => _settingsWindow.SetActive(false));
        ///
        try
        {
            _add = new InterstitialAd(_addId);
            AdRequest request = new AdRequest.Builder().Build();
            _add.LoadAd(request);
            _logText.text = "Ad loading...";

        }
        catch(Exception ex)
        {
            _logText.text = ex.Message;
        }
        _getCrystals.onClick.AddListener(() =>
        {
            try
            {
                if (_add.IsLoaded())
                    _add.Show();
                else
                {
                    _logText.text = "Ad is not load";
                }
            }
            catch(Exception ex)
            {
                _logText.text = ex.Message;
            }
           
        });
        SoundInit();
    }
    private void FixedUpdate()
    {
        //Crystal animation
        //if(_crystalIncrease? _crystalCountAnimation > 0:_crystalCountAnimation < 0 && !_cryatalAnimationInProcess)
        //{
        //    _cryatalAnimationInProcess = true;
        //    StartCoroutine(CrystalAnimation());
        //    if (_crystalIncrease? _crystalCountAnimation < step:_crystalCountAnimation>step)
        //    {
        //        _counter += _crystalIncrease? _crystalCountAnimation: - _crystalCountAnimation;
        //        _crystalCountAnimation = 0;
        //    }
        //    else
        //    {
        //        _crystalCountAnimation -= _crystalIncrease ? step : -step ;   
        //        _counter += _crystalIncrease? step:-step;
        //    }
        //    _textUsualCrystals.text = _counter + "";
        //    _audioSource.PlayOneShot(_soundList.Coin);

        //}
        if (_crystalIncrease)
        {
            if (_crystalCountAnimation > 0 && !_cryatalAnimationInProcess)
            {
                _cryatalAnimationInProcess = true;
                StartCoroutine(CrystalAnimation());
                if (_crystalCountAnimation < step)
                {
                    _counter += _crystalCountAnimation;
                    _crystalCountAnimation = 0;
                }
                else
                {
                    _crystalCountAnimation -= step; ;
                    _counter += step;
                }
                _textUsualCrystals.text = _counter + "";
                _audioSource.PlayOneShot(_soundList.Coin);
            }
        }
        else
        {
            if (_crystalCountAnimation < 0 && !_cryatalAnimationInProcess)
            {
                _cryatalAnimationInProcess = true;
                StartCoroutine(CrystalAnimation());
                if (_crystalCountAnimation > step)
                {
                    _counter -= _crystalCountAnimation;
                    _crystalCountAnimation = 0;
                }
                else
                {
                    _crystalCountAnimation += step; ;
                    _counter -= step;
                }
                _textUsualCrystals.text = _counter + "";
                _audioSource.PlayOneShot(_soundList.Coin);
            }
        }

    }
    private void SoundInit()
    {
        _playButton.onClick.AddListener(() => _audioSource.Play());
        _upgradeButton.onClick.AddListener(() => _audioSource.Play());
    }
    public void InitializeSettings(float levelSound, float sencetivity, int quality, bool showFps)
    {
        _levelSound.value = levelSound;
        _sencetivity.value = sencetivity;
        _quality.value = quality;
        _showFps.isOn = showFps;
    }
    public void AddActionPlay(Action action) => _playButton.onClick.AddListener(() => action?.Invoke());
    public void AddActionUpgrade(Action action)
    {
        _upgradeButton.onClick.AddListener(() =>
        {
            //if (Serializator.DeSerialize(DataName.CurrentLevel) > 2)
            //{
                action?.Invoke();
            //}
            //else
            //{
            //    ShowNotification("Will be available after LV-2");
            //}
        });
    }
    public void AddActionResset(Action action) => _reset.onClick.AddListener(() => action?.Invoke());
    public void SetActiveUI(bool isActive)
    {
        _upgradeButton.gameObject.SetActive(isActive);
        _playButton.gameObject.SetActive(isActive);
        _reset.gameObject.SetActive(isActive);
        _info.gameObject.SetActive(isActive);
        //cheat
        _addMoney.gameObject.SetActive(isActive);
        _moneyCount.gameObject.SetActive(isActive);
        _cheatObj.SetActive(isActive);

    }
    public void UpdateUsualCrystals(int countUsualCrystals)
    {
        if (_crystalWithAnimation)
        {
            _crystalIncrease = _counter < countUsualCrystals;
            _crystalCountAnimation += countUsualCrystals - _counter;
            step = Mathf.Abs(_crystalCountAnimation / 10);
        }
        else
        {
            _crystalWithAnimation = true;
            _counter = countUsualCrystals;
            _textUsualCrystals.text = countUsualCrystals + "";
        }
       
    }//Animation
    private IEnumerator CrystalAnimation()
    {
        yield return new WaitForSeconds(0.03f);
        _cryatalAnimationInProcess = false;
    }
    public void UpdateElectroCrysals(int countCrystals) => _textElectroCrystals.text = countCrystals + "";
    //Notification logic;
    public override void ShowNotification(string text)
    {
        if (_inProcess)
        {
            _notificationText.text = text;
            return;
        }
        _audioSource.PlayOneShot(_soundList.NoMoney);
        _notificationText.text = text;
        _notifiWindow.SetActive(true);
        _notifiWindow.transform.DOMove(_finishAniamtionPosition.position, 0.5f);
        _inProcess = true;
        StartCoroutine(HideWindow());
    }
    private IEnumerator HideWindow()
    {
        yield return new WaitForSeconds(1.8f);
        _notifiWindow.transform.DOMove(_startPositon.position, 0.5f);
        StartCoroutine(FinishProcess());
    }
    private IEnumerator FinishProcess()
    {
        yield return new WaitForSeconds(0.5f);
        _notifiWindow.SetActive(false);
        _inProcess = false;
    }

}
