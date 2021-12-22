
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textLifes;
    [SerializeField] private Slider _lifesSlider;
    [Header("For crystals")]
    [SerializeField] private TextMeshProUGUI _textCountUsualCrystals;
    [SerializeField] private TextMeshProUGUI _textCountElectricCrystal;
    [SerializeField] private Button _replyButton;
    [SerializeField] private Button _menuButton;
    [SerializeField] private Button _shieldMode;
    [SerializeField] private Image _damageEffect;

    private HeroDestroyer _heroDestroyer;
    //damage animation
    private bool _damageAnimationIsPlayed;
    private bool _playDamageAniamtion;
    private bool _valueToUp;
    private float _transparentValue;

    public void SetHeroDestroyer(HeroDestroyer heroDestroyer) => _heroDestroyer = heroDestroyer;
    public void SetUpdateDataUsualCrystal(PlayerData data)
    {
        data.SetChangeCounUCystal((int updateCount) =>
        {
            _textCountUsualCrystals.text = updateCount + "";
        });
        data.SetChangeCounECrystal ((int updateCount) =>
         {
             _textCountElectricCrystal.text = updateCount + "";
         });
    }
    public void SetShieldModeEvent(UnityAction action)
    {
        _shieldMode.onClick.AddListener(action);
    }
    private void Start()
    {
        _replyButton.onClick.AddListener(() =>
        {
            Level.CurrentLevel.Restart();
        });
        _heroDestroyer.SetGetDamageAction( count => _textLifes.text = count + "",true);
        _heroDestroyer.SetGetDamageAction(count =>
        {
            PlayDamageAnimation();
        },false);
        _heroDestroyer.GetDamageActionProcent += (procent) =>
        {
            _lifesSlider.value = procent;
        };
        _menuButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(0);
        });
    }
    private void FixedUpdate()
    {
        if (!_damageAnimationIsPlayed && _playDamageAniamtion)
        {
            StartCoroutine(DamageAnimation());
        }
    }

    private void PlayDamageAnimation()
    {
        _playDamageAniamtion = true;
        _valueToUp = true;
        _transparentValue = 0;
        StartCoroutine(DamageAnimation());

    }

    private IEnumerator DamageAnimation()
    {
        _damageAnimationIsPlayed = true;
        _transparentValue += _valueToUp ? 0.25f : -0.1f;
        if (_transparentValue >= 1)
        {
            _valueToUp = false;

        }
        if (_transparentValue <= 0)
        {
            _playDamageAniamtion = false;
            _damageEffect.color = new Color(1, 1, 1, 0);
        }
        if(_transparentValue >= 0 && _transparentValue<=1)
        {
            _damageEffect.color = new Color(255, 255, 255, _transparentValue);
        }
        yield return new WaitForSeconds(0.005f);
        _damageAnimationIsPlayed = false;
    }
}
