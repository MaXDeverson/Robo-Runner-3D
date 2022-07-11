using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Playables;
using UnityEngine.UI;

public class CatSceneController : MonoBehaviour
{
    [SerializeField] private PlayableDirector _timeLine;
    [SerializeField] private Transform _firstCamera;
    [SerializeField] private Transform _moveTo_1;
    [SerializeField] private Button _skipButton;
    [SerializeField] private float _skipTime;
    void Start()
    {
        StartCoroutine(MoveCamera());
        _skipButton.onClick.AddListener(() =>
        {
            _timeLine.time = _skipTime;
            Transform hero = Level.CurrentLevel.Hero;
            hero.GetChild(6).GetComponent<AudioSource>().mute = false;
            hero.GetChild(7).GetComponent<AudioSource>().mute = false;
        });
        MuteSoundHero();
    }
    private void MuteSoundHero()
    {
        Transform hero = Level.CurrentLevel.Hero;
        hero.GetChild(6).GetComponent<AudioSource>().mute = true;
        hero.GetChild(7).GetComponent<AudioSource>().mute = true;
    }
    private IEnumerator MoveCamera()
    {
        yield return new WaitForSeconds(1.8f);
        _firstCamera.DOMove(_moveTo_1.position, 3);
    }
    public void Signal()
    {
        Transform hero = Level.CurrentLevel.Hero;
        hero.GetChild(6).GetComponent<AudioSource>().mute = false;
        hero.GetChild(7).GetComponent<AudioSource>().mute = false;
    }
}
