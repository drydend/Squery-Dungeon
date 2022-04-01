using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.Linq;

public class PowerUpChoiceMenuUI : UIMenu
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
    private PowerUpBlank _powerUpBlankPrefab;
    [SerializeField]
    private GameObject _choiceMenu;
    [SerializeField]
    private AnimationCurve _powerUpCrationAnimation;
    [SerializeField]
    private float _powerUpCrationDuration = 0.7f;


    private List<PowerUpBlank> _currentPowerUpsUI = new List<PowerUpBlank>();

    private Dictionary<PowerUpRarity, Sprite> _powerUpBackLight = new Dictionary<PowerUpRarity, Sprite>();
    private Dictionary<PowerUpRarity, Sprite> _powerUpGem = new Dictionary<PowerUpRarity, Sprite>();

    public override bool CanBeClosed { get; set; }

    public override event Action OnOpened;

    private void Awake()
    {
        AddSpritesToDictionaries();
    }

    public List<PowerUpBlank> Show(List<PowerUp> powerUps)
    {
        Open();
        
        int positionRelatedToCentre = -(powerUps.Count / 2);

        for (int i = 0; i < 3; i++)
        {
            var createdPowerUp = Instantiate(_powerUpBlankPrefab, _choiceMenu.transform);
            var rectTransform = createdPowerUp.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(_distanceBettwenBlanks * positionRelatedToCentre, 50);
            createdPowerUp.Initialize(_powerUpBackLight[powerUps[i].Rarity], _powerUpGem[powerUps[i].Rarity], powerUps[i]);

            positionRelatedToCentre++;
            _currentPowerUpsUI.Add(createdPowerUp);
        }

        StartCoroutine(AnimateCreation(_currentPowerUpsUI));

        return _currentPowerUpsUI;
    }

    public void OnPowerUpChoosen()
    {
        CanBeClosed = true;
        Close();
        _currentPowerUpsUI.Clear();
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
        _powerUpBackLight[PowerUpRarity.Common] = _backLightCommon;
        _powerUpBackLight[PowerUpRarity.Rare] = _backLightRare;
        _powerUpBackLight[PowerUpRarity.Epic] = _backLightEpic;
        _powerUpBackLight[PowerUpRarity.Legendary] = _backLightLegendary;

        _powerUpGem[PowerUpRarity.Common] = _gemCommon;
        _powerUpGem[PowerUpRarity.Rare] = _gemRare;
        _powerUpGem[PowerUpRarity.Epic] = _gemEpic;
        _powerUpGem[PowerUpRarity.Legendary] = _gemLegendary;
    }

    private IEnumerator AnimateCreation(List<PowerUpBlank> powerUPBlanks)
    {
        float timeFromStart = 0;
        var startScale = Vector3.one;

        var rectTransforms = powerUPBlanks.Select(blank => blank.GetComponent<RectTransform>());

        while (timeFromStart < 1)
        {
            var currentScale = _powerUpCrationAnimation.Evaluate(timeFromStart);
            
            foreach (var rectTransform in rectTransforms)
            {
                rectTransform.localScale = new Vector3(currentScale, currentScale, currentScale);
            }

            timeFromStart += Time.deltaTime / _powerUpCrationDuration;
            yield return null;
        }

        foreach (var rectTransform in rectTransforms)
        {
            rectTransform.localScale = startScale;
        }
        
    }
}
