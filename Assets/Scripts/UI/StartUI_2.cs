using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartUI_2 : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private TextMeshProUGUI _textCountLifes;
    [SerializeField] private TextMeshProUGUI _textDamage;
    [SerializeField] private TextMeshProUGUI _textShieldTime;
    [SerializeField] private TextMeshProUGUI _textRate;
    //Sliders
    [SerializeField] private Slider _lifesSlider;
    [SerializeField] private Slider _damageSlider;
    [SerializeField] private Slider _shieldSlider;
    [SerializeField] private Slider _rateSlider;
    //Window animation
    [SerializeField] private GameObject _statsWindow;
    [SerializeField] private Transform _secondYPosition;
    [SerializeField] private Transform _thirdYPosition;
    [SerializeField] private Button _info;
    [SerializeField] List<Animation> _statsAnimations;
    private int _firstYposiiton;
    [Header("Kills")]
    [SerializeField] private TextMeshProUGUI _killsCount;
    [SerializeField] private Transform _killsVisualWindow;
    [SerializeField] private Transform _killsVisualSecondPostion;
    [SerializeField] private GameObject _achieveCount;
    [SerializeField] private TextMeshProUGUI _textAchieveCount;
    [SerializeField] private GameObject _blinkAchieves;
    private Vector3 _killsVisualStartPosition;
    [Header("Achievements")]
    [SerializeField] private Button _achivementButton;
    [SerializeField] private Button _achivementHide;
    [SerializeField] private GameObject _achivementWindow;
    [SerializeField] private Transform _finishPosition;
    [SerializeField] private GameObject _content;
    [SerializeField] private Transform _itemPrefab;

    private Vector3 _startPosition;
    [Header("Level Visual")]
    [SerializeField] private TextMeshProUGUI _currentLv;
    [SerializeField] private TextMeshProUGUI _previousLv;
    [SerializeField] private TextMeshProUGUI _nextLv;
    [SerializeField] private Transform _levelVisualWindow;
    [SerializeField] private Transform _secondLevelPosition;
    private Vector3 _startLevelVisPosition;
     private bool _inProcess;
    int countForGet = 0;


    private void Start()
    {
        _info.onClick.AddListener(() => _statsAnimations.ForEach(anim => anim.Play()));
        _achivementWindow.SetActive(false);
        _startPosition = _achivementWindow.transform.position;
        _achivementWindow.transform.position = _finishPosition.position;
        _achivementButton.onClick.AddListener(()=> {
            Show(_achivementWindow.transform);
        });
        _achivementHide.onClick.AddListener(() =>
        {
            HideWindow(_achivementWindow.transform);
        });
        AchievementInitialization();
        _firstYposiiton = (int)_statsWindow.transform.position.y;
        //Lv init
        int currentLvIndex = Serializator.DeSerialize(DataName.CurrentLevel);
        _currentLv.text = "LV-" + currentLvIndex;
        _previousLv.text = "LV-" + (currentLvIndex - 1);
        _nextLv.text = "LV-" + (currentLvIndex + 1);
        _startLevelVisPosition = _levelVisualWindow.position;
        _killsVisualStartPosition = _killsVisualWindow.position;
    }
    private void AchievementInitialization()
    {
        
        Serializator.AchievementItemsInitData.ForEach(item =>
        {
            Transform newItem = Instantiate(_itemPrefab);
            newItem.GetComponent<AchieveItem>().SetData(item);
            newItem.parent = _content.transform;
            newItem.localScale = new Vector3(1, 1, 1);
            if (item.Done && !item.GiftGeted) countForGet++;
            item.GetAction += UpdateAchievementsVisual;
        });
        _achieveCount.SetActive(countForGet > 0);
        _blinkAchieves.SetActive(countForGet > 0);
        if (countForGet > 0)
        {
            _textAchieveCount.text = countForGet + "";
        }

        
        _content.transform.localPosition = new Vector3(_content.transform.localRotation.x, -300, 0);
    }
    public void UpdateUI(HeroData currentHero, HeroData lastHero, int killsCount)
    {
        _textCountLifes.text = currentHero.LifesCount + "";
        _textDamage.text = currentHero.DamageCount + "";
        _textShieldTime.text = currentHero.ShieldTimeCount + "";
        _textRate.text = currentHero.RateCount + "";
        _lifesSlider.value = (float)currentHero.LifesCount / lastHero.MaxLifesCount;
        _damageSlider.value = (float)currentHero.DamageCount / lastHero.MaxDamageCount;
        _shieldSlider.value = (float)currentHero.ShieldTimeCount / lastHero.MaxShieldTime;
        _rateSlider.value = (float)currentHero.RateCount / lastHero.MaxRateValue;
        //Stats
        _killsCount.text = killsCount + "";
    }

    public void PlayStatsWindowAnimation(StatsWindowAnimation _animationType)
    {
        int yPotion = 0;
        switch (_animationType)
        {
            case StatsWindowAnimation.First:
                yPotion = _firstYposiiton;
                _killsVisualWindow.transform.DOMove(_killsVisualStartPosition,0.5f);
                _levelVisualWindow.transform.DOMove(_startLevelVisPosition, 0.5f);
                break;
            case StatsWindowAnimation.Second:
                yPotion = (int)_secondYPosition.position.y;
                _killsVisualWindow.transform.DOMove(_killsVisualSecondPostion.position, 0.5f);
                _levelVisualWindow.transform.DOMove(_secondLevelPosition.position, 0.5f);
                break;
            case StatsWindowAnimation.Third:
                yPotion = (int)_thirdYPosition.position.y;
                break;
        }
        _statsWindow.transform.DOMove(new Vector3(_statsWindow.transform.position.x, yPotion, 0), 0.5f);
    }

    //Showing Logic
    private void Show(Transform obj)
    {
        obj.gameObject.SetActive(true);
        obj.DOMove(_startPosition, 0.3f);
    }
    private void HideWindow(Transform obj)
    {
        obj.transform.DOMove(_finishPosition.position, 0.5f);
        StartCoroutine(FinishProcess(obj));
    }
    private IEnumerator FinishProcess(Transform obj)
    {
        yield return new WaitForSeconds(0.5f);
        obj.gameObject.SetActive(false);
        _inProcess = false;
    }
    // New logic
    private void Show(Transform obj, Vector3 position)
    {
        obj.gameObject.SetActive(true);
        obj.DOMove(position, 0.3f);
    }
    private void HideWindow(Transform obj, Vector3 position)
    {
        obj.transform.DOMove(position, 0.5f);
        StartCoroutine(FinishProcess(obj));
    }
    public enum StatsWindowAnimation
    {
        First,
        Second,
        Third,
    }
    private void UpdateAchievementsVisual()
    {
        this.countForGet--;
        _achieveCount.SetActive(this.countForGet > 0);
        _blinkAchieves.SetActive(this.countForGet > 0);
        if (countForGet > 0)
        {
            _textAchieveCount.text = countForGet + "";
        }
    }
    private void OnDestroy()
    {
        Debug.Log("Destroy");
        Serializator.AchievementItemsInitData.ForEach(item =>
        {
            item.GetAction -= UpdateAchievementsVisual;
        });
    }
}
