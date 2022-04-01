using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PowerUpCreator : MonoBehaviour
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
    private List<PowerUp> _commonPowerUps;
    [SerializeField]
    private List<PowerUp> _rarePowerUps;
    [SerializeField]
    private List<PowerUp> _epicPowerUps;
    [SerializeField]
    private List<PowerUp> _legendaryPowerUps;

    private Dictionary<PowerUpRarity, List<PowerUp>> _powerUps = new Dictionary<PowerUpRarity, List<PowerUp>>();
    private Dictionary<PowerUpRarity, float> _rarityWeight = new Dictionary<PowerUpRarity, float>();

    private float TotalWeight => _commonWeight + _rareWeight + _epicWeight + _legendaryWeight;

    private void Awake()
    {
        InitializeDictionaries();
    }

    public void AddPowerUp(PowerUp powerUp)
    {
        _powerUps[powerUp.Rarity].Add(powerUp);
    }

    public void SetWeightForRarity(PowerUpRarity powerUpRarity, float weight)
    {
        if(weight < 0)
        {
            throw new Exception("Weight can`t be less than zero");
        }

        _rarityWeight[powerUpRarity] = weight;
    }

    public PowerUp GetPowerUp(PowerUpRarity lowestRarity = PowerUpRarity.Common)
    {
        var rarity = GetRandomAvaibleRarity(lowestRarity);
        var currentPowerUps = _powerUps[rarity];
        var powerUp  = currentPowerUps.GetRandomValue();
        currentPowerUps.Remove(powerUp);

        return powerUp;
    }

    private PowerUpRarity GetRandomAvaibleRarity(PowerUpRarity lowestRarity)
    {
        float startWeight = 0;
        if(lowestRarity == PowerUpRarity.Rare)
        {
            startWeight += _commonWeight;
        }
        else if(lowestRarity == PowerUpRarity.Epic)
        {
            startWeight += _commonWeight;
            startWeight += _rareWeight;
        }
        else if(lowestRarity == PowerUpRarity.Legendary)
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
                if (_powerUps[rarityWeightPair.Key].Count > 0)
                {
                    return rarityWeightPair.Key;
                }
            }
        }

        return _powerUps.FirstOrDefault(keyValuePair => keyValuePair.Value.Count > 0).Key;
    }

    private void InitializeDictionaries()
    {
        _powerUps[PowerUpRarity.Common] = _commonPowerUps;
        _powerUps[PowerUpRarity.Rare] = _rarePowerUps;
        _powerUps[PowerUpRarity.Epic] = _epicPowerUps;
        _powerUps[PowerUpRarity.Legendary] = _legendaryPowerUps;

        _rarityWeight[PowerUpRarity.Common] = _commonWeight;
        _rarityWeight[PowerUpRarity.Rare] = _rareWeight;
        _rarityWeight[PowerUpRarity.Epic] = _epicWeight;
        _rarityWeight[PowerUpRarity.Legendary] = _legendaryWeight;
    }
}
