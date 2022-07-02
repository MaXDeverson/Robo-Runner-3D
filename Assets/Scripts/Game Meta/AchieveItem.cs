using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchieveItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _header;
    [SerializeField] private TextMeshProUGUI _description;
    [SerializeField] private TextMeshProUGUI _target;
    [SerializeField] private Button _get;
    [SerializeField] private TextMeshProUGUI _giftCrystals;
    [SerializeField] private GameObject _doneVisual;
    void Start()
    {
        
    }
    public void SetData(AchieveItemData data)
    {
        _header.text = data.Header;
        _description.text = data.Subscription;
        int currentCount = 0;
        switch (data.Type)
        {
            case AchieveType.EnemyKills:
                currentCount = PlayerData.GetPlayerData().KillsCount;
                break;
            case AchieveType.ContiuneUse:
                currentCount = PlayerData.GetPlayerData().ContiuneCount;
                break;
            case AchieveType.ShieldUse:
                currentCount = PlayerData.GetPlayerData().ShieldUseCount;
                break;
        }
        _target.text = (data.Done? data.Count : currentCount) + "/" + data.Count;
        _giftCrystals.text = data.GiftCrystalsCount + "";
        _get.gameObject.SetActive(data.Done && !data.GiftGeted);
        _doneVisual.SetActive(data.GiftGeted);
        if (data.Done)
        {
            _get.onClick.AddListener(() => {
                _get.gameObject.SetActive(false);
                data.GetCrystals();
                _doneVisual.SetActive(true);
            });
        }
    }
}
