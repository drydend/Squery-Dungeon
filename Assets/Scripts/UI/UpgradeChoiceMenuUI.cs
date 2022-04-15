using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.Linq;

public class UpgradeChoiceMenuUI : UIMenu
{
    #region Sprites
    [SerializeField]
    private Sprite _backLightCommon;
    [SerializeField]
    private Sprite _backLightRare;
    [SerializeField]
    private Sprite _backLightEpic;
    [SerializeField]
    private Sprite _backLightLegendary;

    [SerializeField]
    private Sprite _gemCommon;
    [SerializeField]
    private Sprite _gemRare;
    [SerializeField]
    private Sprite _gemEpic;
    [SerializeField]
    private Sprite _gemLegendary;
    #endregion

    [SerializeField]
    private float _distanceBettwenBlanks = 150f;
    [SerializeField]
    private UpgradeBlank _upgradeBlankPrefab;
    [SerializeField]
    private GameObject _choiceMenu;
    [SerializeField]
    private AnimationCurve _upgradeCrationAnimation;
    [SerializeField]
    private float _upgradeCreationDuration = 0.7f;


    private List<UpgradeBlank> _currentUpgradesBlanks = new List<UpgradeBlank>();

    private Dictionary<UpgradeRarity, Sprite> _upgradeBackLight = new Dictionary<UpgradeRarity, Sprite>();
    private Dictionary<UpgradeRarity, Sprite> _upgradeGem = new Dictionary<UpgradeRarity, Sprite>();

    public override bool CanBeClosed { get; set; }

    public override event Action OnOpened;

    private void Awake()
    {
        AddSpritesToDictionaries();
    }

    public List<UpgradeBlank> Show(List<Upgrade> upgrades)
    {
        Open();
        
        int positionRelatedToCentre = -(upgrades.Count / 2);

        for (int i = 0; i < 3; i++)
        {
            var createdPowerUp = Instantiate(_upgradeBlankPrefab, _choiceMenu.transform);
            var rectTransform = createdPowerUp.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(_distanceBettwenBlanks * positionRelatedToCentre, 50);
            createdPowerUp.Initialize(_upgradeBackLight[upgrades[i].Rarity], _upgradeGem[upgrades[i].Rarity], upgrades[i]);

            positionRelatedToCentre++;
            _currentUpgradesBlanks.Add(createdPowerUp);
        }

        StartCoroutine(AnimateCreation(_currentUpgradesBlanks));

        return _currentUpgradesBlanks;
    }

    public void OnPowerUpChoosen()
    {
        CanBeClosed = true;
        
        foreach (var upgradeBlank in _currentUpgradesBlanks)
        {
            Destroy(upgradeBlank.gameObject);
        }

        _currentUpgradesBlanks.Clear();
        Close();
    }


    public override void Open()
    {
        CanBeClosed = false;
        OnOpened?.Invoke();
        _choiceMenu.SetActive(true);
    }

    public override void Close()
    {
        _choiceMenu.SetActive(false);
    }

    private void AddSpritesToDictionaries()
    {
        _upgradeBackLight[UpgradeRarity.Common] = _backLightCommon;
        _upgradeBackLight[UpgradeRarity.Rare] = _backLightRare;
        _upgradeBackLight[UpgradeRarity.Epic] = _backLightEpic;
        _upgradeBackLight[UpgradeRarity.Legendary] = _backLightLegendary;

        _upgradeGem[UpgradeRarity.Common] = _gemCommon;
        _upgradeGem[UpgradeRarity.Rare] = _gemRare;
        _upgradeGem[UpgradeRarity.Epic] = _gemEpic;
        _upgradeGem[UpgradeRarity.Legendary] = _gemLegendary;
    }

    private IEnumerator AnimateCreation(List<UpgradeBlank> powerUPBlanks)
    {
        float timeFromStart = 0;
        var startScale = Vector3.one;

        var rectTransforms = powerUPBlanks.Select(blank => blank.GetComponent<RectTransform>());

        while (timeFromStart < 1)
        {
            var currentScale = _upgradeCrationAnimation.Evaluate(timeFromStart);
            
            foreach (var rectTransform in rectTransforms)
            {
                rectTransform.localScale = new Vector3(currentScale, currentScale, currentScale);
            }

            timeFromStart += Time.deltaTime / _upgradeCreationDuration;
            yield return null;
        }

        foreach (var rectTransform in rectTransforms)
        {
            rectTransform.localScale = startScale;
        }
        
    }
}
