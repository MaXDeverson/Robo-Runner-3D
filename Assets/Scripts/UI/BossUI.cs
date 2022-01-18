using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossUI : MonoBehaviour
{
    [SerializeField] private Slider _lifesSlider;
    [SerializeField] private EnemyDestroyer _bossDestroyer;
    [SerializeField] private TextMeshProUGUI _textHP;
    [SerializeField] private Image _damageEffect;
    private Color _damageEffectColor;
    //damage animation
    private bool _damageAnimationIsPlayed;
    private bool _playDamageAniamtion;
    private bool _valueToUp;
    private float _transparentValue;
    void Start()
    {
        _damageEffectColor = _damageEffect.color;
        _bossDestroyer.ActionGetDamage += (count,procent) =>
        {
            _lifesSlider.value = procent;
            _textHP.text = count + "";
            PlayDamageAnimation();
        };
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
        _transparentValue += _valueToUp ? 0.35f : -0.2f;
        _damageEffectColor.a = _transparentValue;
        if (_transparentValue >= 1)
        {
            _valueToUp = false;

        }
        if (_transparentValue <= 0)
        {
            _playDamageAniamtion = false;
            _damageEffect.color = _damageEffectColor;
        }
        if (_transparentValue >= 0 && _transparentValue <= 1)
        {
            _damageEffect.color = _damageEffectColor;
        }
        yield return new WaitForSeconds(0.005f);
        _damageAnimationIsPlayed = false;
    }
}
