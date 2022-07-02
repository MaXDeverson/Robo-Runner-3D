using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchieveItemData
{
    public Action GetAction;
    public string Header { get => _header; }
    public string Subscription { get => _subscription; }
    public int GiftCrystalsCount { get => _giftCrystals; }
    public int Count { get => _count; }
    public bool Done { get {
            bool done = false;
            switch (_type)
            {
                case AchieveType.EnemyKills:
                    done = PlayerData.GetPlayerData().KillsCount >= _count;
                    break;
                case AchieveType.ContiuneUse:
                    done = PlayerData.GetPlayerData().ContiuneCount >= _count;
                    break;
                case AchieveType.ShieldUse:
                    done = PlayerData.GetPlayerData().ShieldUseCount >= _count;
                    break;
            }
            return done;
        }}
    public bool GiftGeted { get => _giftGeted; }

    public AchieveType Type { get => _type; }
    private string _header;
    private string _subscription;
    private int _giftCrystals;
    private int _count;
    private AchieveType _type;
    private bool _giftGeted;

    public AchieveItemData(string header, string subscription, int giftCrystals, int count, AchieveType type)
    {
        _header = header;
        _subscription = subscription;
        _giftCrystals = giftCrystals;
        _count = count;
        _type = type;
    }
    public void SetGIftGeted(bool value) => _giftGeted = value;
    public void GetCrystals()
    {
        _giftGeted = true;
        PlayerData.GetPlayerData().AddUsualCrystals(GiftCrystalsCount);
        GetAction?.Invoke();
    }
}
public enum AchieveType
{
    EnemyKills,
    ShieldUse,
    ContiuneUse,
}