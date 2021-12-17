using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyUppLogic : MonoBehaviour
{
    [SerializeField] private List<Transform> _visualHeroes;
    [SerializeField] private CameraBuy _camera;
    [SerializeField] private StartUI _startUI;
    private int _currentHeroIndex;
    void Start()
    {
        _startUI.AddActionNext(NextHero);
        _startUI.AddActionPrevious(PreviousHero);
        _startUI.AddActionPlay(() => {
            Level.SetHeroIndex(_currentHeroIndex);
            Debug.Log("Set hero index " + _currentHeroIndex);
            }
        );
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void NextHero()
    {
        if (++_currentHeroIndex >= _visualHeroes.Count)
        {
            _currentHeroIndex = 0;
        }
        _camera.SetTarget(_visualHeroes[_currentHeroIndex]);
    }
    private void PreviousHero()
    {
        if(--_currentHeroIndex < 0)
        {
            _currentHeroIndex = _visualHeroes.Count - 1;
        }
        _camera.SetTarget(_visualHeroes[_currentHeroIndex]);
    }
}
