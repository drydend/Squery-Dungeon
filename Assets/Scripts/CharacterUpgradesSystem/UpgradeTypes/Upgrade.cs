using UnityEngine;

public class Upgrade : ScriptableObject
{
    [SerializeField]
    protected UpgradeRarity _upgradeRarity;
    [SerializeField]
    protected Sprite _icon;
    [SerializeField]
    protected string _discription;

    public UpgradeRarity Rarity => _upgradeRarity;
    public Sprite Icon => _icon;
    public string Discription => _discription;
}
