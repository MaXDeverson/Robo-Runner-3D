using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private UI _ui;
    [SerializeField] private HeroDestroyer _heroDestroyer;

    private void Awake()
    {
        _ui.SetHeroDestroyer(_heroDestroyer);
    }
    private void Start()
    {
        
    }
    private void Update()
    {
        
    }
}
