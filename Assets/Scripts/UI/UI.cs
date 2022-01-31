
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using GoogleMobileAds.Api;
using System;

public class UI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textLifes;
    [SerializeField] private Slider _lifesSlider;
    [Header("For crystals")]
    [SerializeField] private TextMeshProUGUI _textCountUsualCrystals;
    [SerializeField] private TextMeshProUGUI _textCountElectricCrystal;
    [SerializeField] private Button _shieldMode;
    [SerializeField] private Image _damageEffect;
    [Header("Menu")]
    [SerializeField] private Transform _menuBoard;
    [SerializeField] private Button _menuButton;
    [SerializeField] private Transform _startPosition;
    [SerializeField] private Transform _finishPosition;
    [SerializeField] private Button _continue;
    [SerializeField] private Button _replay;
    [SerializeField] private Button _exit;
    [SerializeField] private GameObject _loadWindow;
    [Header("Loose")]
    [SerializeField] private Transform _looseBoard;
    [SerializeField] private Button _looseReplay;
    [SerializeField] private GameObject _panel;
    [SerializeField] private Button _contiuneButton;
    [Header("Shield")]
    [SerializeField] private Image _shieldImage;
    [SerializeField] private GameObject _shieldBackground;
    [SerializeField] private TextMeshProUGUI _shieldTimeText;
    private float _shieldImageFillCoeficient;
    private bool _shieldAnimatinIsPlaying;
    private float _waitTime;

    private Action _exitAction;

   // private InterstitialAd _add;
    private string _addId = "ca-app-pub-3940256099942544/1033173712";


    private Transform _animatiedObj;

    private HeroDestroyer _heroDestroyer;
    //damage animation
    private bool _damageAnimationIsPlayed;
    private bool _playDamageAniamtion;
    private bool _valueToUp;
    private float _transparentValue;
    public void ShowLoadView() => _loadWindow.SetActive(true);
    public void SetHeroDestroyer(HeroDestroyer heroDestroyer) => _heroDestroyer = heroDestroyer;
    public void SetUpdateDataUsualCrystal(PlayerData data)
    {
        data.SetChangeCounUCystal((int updateCount) =>
        {
            _textCountUsualCrystals.text = updateCount + "";
        });
        data.SetChangeCounECrystal ((int updateCount) =>
         {
             _textCountElectricCrystal.text = updateCount + "";
         });
    }
    public void SetShieldModeEvent(UnityAction action)
    {
        _shieldMode.onClick.AddListener(action);
    }
    private void Awake()
    {
        //_add = new InterstitialAd(_addId);
       // AdRequest request = new AdRequest.Builder().Build();
        //_add.LoadAd(request);
    }
    private void Start()
    {  
        _heroDestroyer.SetGetDamageAction( count => _textLifes.text = count + "",true);
        _heroDestroyer.SetGetDamageAction(count =>
        {
            PlayDamageAnimation();
        },false);
        _heroDestroyer.GetDamageActionProcent += (procent) =>
        {
            _lifesSlider.value = procent;
        };
        InitilizationMenu();
        InitializeationLoose();
    }
    private void InitilizationMenu()
    {
        _menuBoard.gameObject.SetActive(false);
        _menuButton.onClick.AddListener(() =>
        {
            _panel.SetActive(true);
            Show(_menuBoard);
        });
        _continue.onClick.AddListener(() =>
        {
            _panel.SetActive(false);
            HideMenu();
        });
        _exit.onClick.AddListener(Exit);
        _replay.onClick.AddListener(Replay);
    }
    private void InitializeationLoose()
    {
        Level.CurrentLevel.SetDestroyAction(() =>
        {
            _panel.SetActive(true);
            Show(_looseBoard,false);
        });
        _looseReplay.onClick.AddListener(Replay);
        _contiuneButton.onClick.AddListener(Contiune);
        //_add.OnAdClosed += _add_OnAdClosed;
    }

    private void FixedUpdate()
    {
        if (!_damageAnimationIsPlayed && _playDamageAniamtion)
        {
            StartCoroutine(DamageAnimation());
        }
    }
    private void PlayDamageAnimation()
    {
        _playDamageAniamtion = true;
        _valueToUp = true;
        _transparentValue = 0;
        StartCoroutine(DamageAnimation());

    }
    private IEnumerator DamageAnimation()
    {
        _damageAnimationIsPlayed = true;
        _transparentValue += _valueToUp ? 0.25f : -0.1f;
        if (_transparentValue >= 1)
        {
            _valueToUp = false;

        }
        if (_transparentValue <= 0)
        {
            _playDamageAniamtion = false;
            _damageEffect.color = new Color(1, 1, 1, 0);
        }
        if(_transparentValue >= 0 && _transparentValue<=1)
        {
            _damageEffect.color = new Color(255, 255, 255, _transparentValue);
        }
        yield return new WaitForSeconds(0.005f);
        _damageAnimationIsPlayed = false;
    }
    //Menu animation
    private void Show(Transform obj,bool stop = true)
    {
        obj.gameObject.SetActive(true);
        obj.DOMove(_finishPosition.position, 0.3f);
        if (stop)
        {
            StartCoroutine(Stop());
        }
    }
    private IEnumerator Stop()
    {
        yield return new WaitForSeconds(0.31f);
        Time.timeScale = 0;
    }
    private void HideMenu()
    {
        Time.timeScale = 1;
        _menuBoard.transform.DOMove(_startPosition.position, 0.2f);
        StartCoroutine(InvizibleMenu());
    }
    private IEnumerator InvizibleMenu()
    {
        yield return new WaitForSeconds(0.2f);
        _menuBoard.gameObject.SetActive(false);
    }
    //Shield Viewer
    public void ShowShieldTime(int seconds)
    {
        _shieldBackground.SetActive(true);
        _shieldImage.gameObject.SetActive(true);
        _shieldTimeText.gameObject.SetActive(true);
        _shieldTimeText.text = seconds + "";
        _shieldImageFillCoeficient = 1;
        _waitTime = 0.02f;
        //Show
    }
    private IEnumerator PlayShieldAnimationFrame()
    {
        _shieldAnimatinIsPlaying = true;
        _shieldImageFillCoeficient -= 0.03f;
        yield return new WaitForSeconds(_waitTime);//waitTime;
        _shieldImage.fillAmount = _shieldImageFillCoeficient;
    }

    private void Replay()
    {
        Time.timeScale = 1;
        _loadWindow.SetActive(true);
        Level.CurrentLevel.Restart();
    }
    private void Exit()
    {
        Time.timeScale = 1;
        _loadWindow.SetActive(true);
        _exitAction?.Invoke();
        SceneManager.LoadScene(0);
    }
    public void AddActionExit(Action action) => _exitAction += action;
    private void Contiune()
    {
        //if (_add.IsLoaded())
        //{
        //    _add.Show();
        //}
        //else
        //{
        //    Debug.Log("No load");
        //}
    }
}
