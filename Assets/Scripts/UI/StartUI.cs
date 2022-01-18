using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

public class StartUI : INotifible
{
    [SerializeField] private Initializator _initializator;
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _reset;
    [SerializeField] private GameObject _loadObjects;
    [SerializeField] private Text _text;
    [SerializeField] private Button _upgradeButton;
    [SerializeField] private BuyUI _upgradeUI;
    [Header("Crystals")]
    [SerializeField] private TextMeshProUGUI _textUsualCrystals;
    [SerializeField] private TextMeshProUGUI _textElectroCrystals;
    [Header("Cheat")]
    [SerializeField] private Button _addMoney;
    [SerializeField] private Text _moneyCount;
    [SerializeField] private GameObject _cheatObj;
    [Header("Notification")]
    [SerializeField] private TextMeshProUGUI _notificationText;
    [SerializeField] private GameObject _notifiWindow;
    [SerializeField] private Transform _startPositon;
    [SerializeField] private Transform _finishAniamtionPosition;
    private bool _inProcess;

    void Start()
    {
        PlayerData data = PlayerData.GetPlayerData();
        _addMoney.onClick.AddListener(() => {
            data.AddUsualCrystals(int.Parse(_moneyCount.text));
            _textUsualCrystals.text = data.CountUsualCrystals + "";
            Serializator.Serialize(DataName.CountCrystals, data.CountUsualCrystals);
        });
        //cheat
        _upgradeUI.gameObject.SetActive(false);
        _playButton.onClick.AddListener(() =>
        {
            if(DateTime.Now < new DateTime(2022,2,14))
            {
                _initializator.Play();
                _loadObjects.SetActive(true);
            }
            else
            {
                _text.text = "Срок дії закінчився, напишіть в інстаграм waisempai (Власнику ігри)";
            }
        });
        _upgradeButton.onClick.AddListener(() => {
            _upgradeUI.gameObject.SetActive(true);
             SetActiveUI(false);
        });
        //player data initialization;
      
        _textUsualCrystals.text = data.CountUsualCrystals + "";
        _textElectroCrystals.text = data.CountElectroCrystals + "";
    }
    public void AddActionPlay(Action action) => _playButton.onClick.AddListener(() => action?.Invoke());
    public void AddActionUpgrade(Action action) => _upgradeButton.onClick.AddListener(() => action?.Invoke());
    public void AddActionResset(Action action) => _reset.onClick.AddListener(()=>action?.Invoke());
    public void SetActiveUI(bool isActive)
    {
        _upgradeButton.gameObject.SetActive(isActive);
        _playButton.gameObject.SetActive(isActive);
        _reset.gameObject.SetActive(isActive);
        //cheat
        _addMoney.gameObject.SetActive(isActive);
        _moneyCount.gameObject.SetActive(isActive);
        _cheatObj.SetActive(isActive);

    }
    public void UpdateUsualCrystals(int countUsualCrystals) => _textUsualCrystals.text = countUsualCrystals + "";
    public void UpdateElectroCrysals(int countCrystals)=> _textElectroCrystals.text = countCrystals + "";
    //Notification logic;
    public override void ShowNotification(string text)
    {
        if(_inProcess)
        {
            _notificationText.text = text;
            return;
        }
        _notificationText.text = text;
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
        _inProcess = false;
    }

}
