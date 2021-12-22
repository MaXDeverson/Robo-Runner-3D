using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossHitPointsUI : MonoBehaviour
{
    [SerializeField] private EnemyDestroyer _destroyer;
    [SerializeField] private TextMeshProUGUI _textHP;
    [SerializeField] private Scrollbar _barHP;
    void Start()
    {
        _destroyer.ActionGetDamage += (updateCount, updateProcent) =>
        {
            _textHP.text = updateCount + "";
            _barHP.value = updateProcent;
        };

    }
}
