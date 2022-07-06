using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThingsCounter : MonoBehaviour
{
    public Action GetECrystalAction;
    private PlayerData _playerData;
    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case Tag.Crystal:
                _playerData.AddUsualCrystals();
                break;
            case Tag.ElectroCrystal:
                _playerData.AddElectroCrystal();
                GetECrystalAction?.Invoke();
                Debug.Log("Electro crystal geted");
                break;
        }
    }
    public void SetListener(PlayerData playerData) => _playerData = playerData;
}
