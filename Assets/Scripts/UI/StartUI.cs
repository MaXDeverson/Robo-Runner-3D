using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartUI : INotifible
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _reset;
    [SerializeField] private GameObject _loadObjects;
    [SerializeField] private Text _text;
    [SerializeField] private Button _upgradeButton;
    [SerializeField] private BuyUI _buyUI;
    [SerializeField] private Button _info;
    [SerializeField] private Button _hideInfo;
    [SerializeField] private GameObject _informationObj;
    [Header("Crystals")]
    [SerializeField] private TextMeshProUGUI _textUsualCrystals;
    [SerializeField] private TextMeshProUGUI _textElectroCrystals;
    [Header("Cheat")]
    [SerializeField] private Button _addMoney;
    [SerializeField] private Text _moneyCount;
    [SerializeField] private GameObject _cheatObj;
    [SerializeField] private Text _currentLevel;
    [Header("Notification")]
    [SerializeField] private TextMeshProUGUI _notificationText;
    [SerializeField] private GameObject _notifiWindow;
    [SerializeField] private Transform _startPositon;
    [SerializeField] private Transform _finishAniamtionPosition;
    [Header("Sound")]
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private SoundList _soundList;
    private bool _inProcess;

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
        SoundInit();
    }

    private void SoundInit()
    {
        _playButton.onClick.AddListener(() => _audioSource.Play());
        _upgradeButton.onClick.AddListener(() => _audioSource.Play());
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
            //    ShowNotification("Will be availible after 2 level");
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
    public void UpdateUsualCrystals(int countUsualCrystals) => _textUsualCrystals.text = countUsualCrystals + "";
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
