using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyUppLogic : MonoBehaviour
{
    [SerializeField] private List<Transform> _visualHeroes;
    [SerializeField] private CameraBuy _camera;
    [SerializeField] private UpgradeUI _upgradeUI;
    [SerializeField] private StartUI _startUI;
    private List<HeroData> _dataHeroes;
    private int _selectedVisualHeroIndex;
    private int _currentHeroIndex;
    void Start()
    {
        _startUI.gameObject.SetActive(true);////fortesting
        _upgradeUI.gameObject.SetActive(true);//
        ////////////////////////////////////////
        InitializationDataOfHero();
        InitializationUI();
        _camera.SetTarget(_visualHeroes[_currentHeroIndex]);
    }
    private void InitializationUI()
    {
        _upgradeUI.AddActionNext(NextHero);
        _upgradeUI.AddActionSelect(()=> {
            _dataHeroes.ForEach(hero => hero.IsSelect = false);
            _dataHeroes[_currentHeroIndex].IsSelect = true;
            _upgradeUI.UpdateUI(_dataHeroes[_currentHeroIndex]);
            _selectedVisualHeroIndex = _currentHeroIndex;
            Level.SetHeroIndex(_currentHeroIndex);
        });
        _upgradeUI.AddActionBack(BackToUI);
        _upgradeUI.AddActionPrevious(PreviousHero);
        _startUI.AddActionUpgrade(() => _upgradeUI.SetActive(true));
        _startUI.AddActionUpgrade(() => _upgradeUI.UpdateUI(_dataHeroes[_currentHeroIndex]));
        _startUI.AddActionUpgrade(_camera.AnimatePositionNear);
       

    }
    private void InitializationDataOfHero()
    {
        //her wil be serialization;
        _dataHeroes = new List<HeroData>
        {
            new HeroData(true,true,0,0,0,new int[]{2,3,4},new int[]{1,2,3}),
            new HeroData(true,false,0,0,0,new int[]{4,5},new int[]{4,5,6}),
            new HeroData(false,false,0,0,0,new int[]{6,7,8},new int[]{7,8,9}),
            new HeroData(false,false,0,0,0,new int[]{9,10,12},new int[]{12,13,15})
        };
        for(int i = 0; i < _dataHeroes.Count; i++)
        {
            if (_dataHeroes[i].IsSelect) _selectedVisualHeroIndex = i;
        }
    }
    private void BackToUI()
    {
        _startUI.SetActiveUI(true);
        _camera.SetTarget(_visualHeroes[_selectedVisualHeroIndex]);
        _camera.AnimatePositionFar();
        _currentHeroIndex = _selectedVisualHeroIndex;
    }
    private void NextHero()
    {
        if (++_currentHeroIndex >= _visualHeroes.Count)
        {
            _currentHeroIndex = 0;
        }
        _upgradeUI.UpdateUI(_dataHeroes[_currentHeroIndex]);
        _camera.SetTarget(_visualHeroes[_currentHeroIndex]);
    }
    private void PreviousHero()
    {
        if(--_currentHeroIndex < 0)
        {
            _currentHeroIndex = _visualHeroes.Count - 1;
        }
        _upgradeUI.UpdateUI(_dataHeroes[_currentHeroIndex]);
        _camera.SetTarget(_visualHeroes[_currentHeroIndex]);
    }
}
