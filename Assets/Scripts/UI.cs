
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

    private HeroDestroyer _heroDestroyer;

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
        _heroDestroyer.GetDamageAction += (count) =>
        {
            _textLifes.text = count + "";
        };
        _heroDestroyer.GetDamageActionProcent += (procent) =>
        {
            _lifesSlider.value = procent;
        };
        _menuButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(0);
        });
    }
}
