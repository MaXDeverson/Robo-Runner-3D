using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThingsCounter : MonoBehaviour
{
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
                break;
        }
    }
    public void SetListener(PlayerData playerData) => _playerData = playerData;
}
