using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PowerUPCreator : MonoBehaviour
{
    [SerializeField]
    private float _commonWeight = 50;
    [SerializeField]
    private float _rareWeight = 30;
    [SerializeField]
    private float _epicWeight = 15;
    [SerializeField]
    private float _legendaryWeight = 5;

    [SerializeField]
    private List<CharacterModificator> _allModificators;
    
    private List<CharacterModificator> _commonModificators = new List<CharacterModificator>();
    private List<CharacterModificator> _rareModificators = new List<CharacterModificator>();
    private List<CharacterModificator> _epicModificators = new List<CharacterModificator>();
    private List<CharacterModificator> _legendaryModificators = new List<CharacterModificator>();

    private Dictionary<PowerUPRarity, List<CharacterModificator>> _modificators = 
        new Dictionary<PowerUPRarity, List<CharacterModificator>>();
    private Dictionary<PowerUPRarity, float> _rarityWeight = new Dictionary<PowerUPRarity, float>();

    private float TotalWeight => _commonWeight + _rareWeight + _epicWeight + _legendaryWeight;

    private void Awake()
    {
        Initialize();
    }

    public void AddPowerUp(CharacterModificator modificator)
    {
        _modificators[modificator.GetRarity()].Add(modificator);
    }

    public void SetWeightForRarity(PowerUPRarity powerUpRarity, float weight)
    {
        if(weight < 0)
        {
            throw new Exception("Weight can`t be less than zero");
        }

        _rarityWeight[powerUpRarity] = weight;
    }

    public CharacterModificator GetRandomModificator(PowerUPRarity lowestRarity = PowerUPRarity.Common)
    {
        var rarity = GetRandomAvaibleRarity(lowestRarity);
        var currentModificators = _modificators[rarity];
        var modificator  = currentModificators.GetRandomValue();
        currentModificators.Remove(modificator);

        return modificator;
    }

    private PowerUPRarity GetRandomAvaibleRarity(PowerUPRarity lowestRarity)
    {
        float startWeight = 0;
        if(lowestRarity == PowerUPRarity.Rare)
        {
            startWeight += _commonWeight;
        }
        else if(lowestRarity == PowerUPRarity.Epic)
        {
            startWeight += _commonWeight;
            startWeight += _rareWeight;
        }
        else if(lowestRarity == PowerUPRarity.Legendary)
        {
            startWeight += _commonWeight;
            startWeight += _rareWeight;
            startWeight += _epicWeight;
        }

        float randomWeight = UnityEngine.Random.Range(startWeight, TotalWeight - 1);
        float currentWeight = randomWeight;

        foreach (var rarityWeightPair in _rarityWeight)
        {
            if(rarityWeightPair.Value < currentWeight)
            {
                currentWeight -= rarityWeightPair.Value;
            }
            else
            {
                if (_modificators[rarityWeightPair.Key].Count > 0)
                {
                    return rarityWeightPair.Key;
                }
            }
        }

        return _modificators.FirstOrDefault(keyValuePair => keyValuePair.Value.Count > 0).Key;
    }

    private void Initialize()
    {
        foreach (var modificator in _allModificators)
        {
            switch (modificator.GetRarity())
            {
                case PowerUPRarity.Common:
                    _commonModificators.Add(modificator);
                    break;
                case PowerUPRarity.Rare:
                    _rareModificators.Add(modificator);
                    break;
                case PowerUPRarity.Epic:
                    _epicModificators.Add(modificator);
                    break;
                case PowerUPRarity.Legendary:
                    _legendaryModificators.Add(modificator);
                    break;
            }
        }

        _modificators[PowerUPRarity.Common] = _commonModificators;
        _modificators[PowerUPRarity.Rare] = _rareModificators;
        _modificators[PowerUPRarity.Epic] = _epicModificators;
        _modificators[PowerUPRarity.Legendary] = _legendaryModificators;

        _rarityWeight[PowerUPRarity.Common] = _commonWeight;
        _rarityWeight[PowerUPRarity.Rare] = _rareWeight;
        _rarityWeight[PowerUPRarity.Epic] = _epicWeight;
        _rarityWeight[PowerUPRarity.Legendary] = _legendaryWeight;
    }
}
