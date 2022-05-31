using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.Linq;

public class PowerUPChoiceMenuUI : UIMenu
{
    [SerializeField]
    private Color _backLightColorCommon;
    [SerializeField]
    private Color _backLightColorRare;
    [SerializeField]
    private Color _backLightColorEpic;
    [SerializeField]
    private Color _backLightColorLegendary;

    [SerializeField]
    private PowerUPBlank _upgradeBlankPrefab;
    [SerializeField]
    private float _distanceBettwenBlanks = 150f;
    
    [SerializeField]
    private AnimationCurve _upgradeCrationAnimation;
    [SerializeField]
    private float _upgradeCreationDuration = 0.7f;

    private List<PowerUPBlank> _currentModificatorsBlanks = new List<PowerUPBlank>();

    private Dictionary<PowerUPRarity, Color> _modificatorBackLightColor = new Dictionary<PowerUPRarity, Color>();
    
    [SerializeField]
    private GameObject _choiceMenu;

    public override bool CanBeClosed { get; set; }

    public override event Action OnOpened;

    private void Awake()
    {
        InitializeDictionaries();
    }

    public List<PowerUPBlank> Show(List<CharacterModificator> modificators)
    {
        Open();
        
        int positionRelatedToCentre = -(modificators.Count / 2);

        for (int i = 0; i < 3; i++)
        {
            var blank = Instantiate(_upgradeBlankPrefab, _choiceMenu.transform);
            var rectTransform = blank.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(_distanceBettwenBlanks * positionRelatedToCentre, 50);
            blank.Initialize(_modificatorBackLightColor[modificators[i].GetRarity()], modificators[i]);

            positionRelatedToCentre++;
            _currentModificatorsBlanks.Add(blank);
        }

        StartCoroutine(AnimateCreation(_currentModificatorsBlanks));

        return _currentModificatorsBlanks;
    }

    public void OnModificatorChoosen()
    {
        CanBeClosed = true;
        
        foreach (var blank in _currentModificatorsBlanks)
        {
            Destroy(blank.gameObject);
        }

        _currentModificatorsBlanks.Clear();
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

    private void InitializeDictionaries()
    {
        _modificatorBackLightColor[PowerUPRarity.Common] = _backLightColorCommon;
        _modificatorBackLightColor[PowerUPRarity.Rare] = _backLightColorRare;
        _modificatorBackLightColor[PowerUPRarity.Epic] = _backLightColorEpic;
        _modificatorBackLightColor[PowerUPRarity.Legendary] = _backLightColorLegendary;
    }

    private IEnumerator AnimateCreation(List<PowerUPBlank> powerUPBlanks)
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
