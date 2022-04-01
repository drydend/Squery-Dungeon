using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : ScriptableObject
{
    [SerializeField]
    protected PowerUpRarity _powerUpRarity;
    [SerializeField]
    protected Sprite _icon;
    [SerializeField]
    protected string _discription;

    public PowerUpRarity Rarity => _powerUpRarity;
    public Sprite Icon => _icon;
    public string Discription => _discription;
}
