using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UpgradeCreator : MonoBehaviour
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
    private List<Upgrade> _commonPowerUps;
    [SerializeField]
    private List<Upgrade> _rarePowerUps;
    [SerializeField]
    private List<Upgrade> _epicPowerUps;
    [SerializeField]
    private List<Upgrade> _legendaryPowerUps;

    private Dictionary<UpgradeRarity, List<Upgrade>> _powerUps = new Dictionary<UpgradeRarity, List<Upgrade>>();
    private Dictionary<UpgradeRarity, float> _rarityWeight = new Dictionary<UpgradeRarity, float>();

    private float TotalWeight => _commonWeight + _rareWeight + _epicWeight + _legendaryWeight;

    private void Awake()
    {
        InitializeDictionaries();
    }

    public void AddPowerUp(Upgrade powerUp)
    {
        _powerUps[powerUp.Rarity].Add(powerUp);
    }

    public void SetWeightForRarity(UpgradeRarity powerUpRarity, float weight)
    {
        if(weight < 0)
        {
            throw new Exception("Weight can`t be less than zero");
        }

        _rarityWeight[powerUpRarity] = weight;
    }

    public Upgrade GetPowerUp(UpgradeRarity lowestRarity = UpgradeRarity.Common)
    {
        var rarity = GetRandomAvaibleRarity(lowestRarity);
        var currentPowerUps = _powerUps[rarity];
        var powerUp  = currentPowerUps.GetRandomValue();
        currentPowerUps.Remove(powerUp);

        return powerUp;
    }

    private UpgradeRarity GetRandomAvaibleRarity(UpgradeRarity lowestRarity)
    {
        float startWeight = 0;
        if(lowestRarity == UpgradeRarity.Rare)
        {
            startWeight += _commonWeight;
        }
        else if(lowestRarity == UpgradeRarity.Epic)
        {
            startWeight += _commonWeight;
            startWeight += _rareWeight;
        }
        else if(lowestRarity == UpgradeRarity.Legendary)
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
        _powerUps[UpgradeRarity.Common] = _commonPowerUps;
        _powerUps[UpgradeRarity.Rare] = _rarePowerUps;
        _powerUps[UpgradeRarity.Epic] = _epicPowerUps;
        _powerUps[UpgradeRarity.Legendary] = _legendaryPowerUps;

        _rarityWeight[UpgradeRarity.Common] = _commonWeight;
        _rarityWeight[UpgradeRarity.Rare] = _rareWeight;
        _rarityWeight[UpgradeRarity.Epic] = _epicWeight;
        _rarityWeight[UpgradeRarity.Legendary] = _legendaryWeight;
    }
}
