using UnityEngine;

public class Upgrade : ScriptableObject
{
    [SerializeField]
    protected PowerUPRarity _rarity;
    [SerializeField]
    protected Sprite _icon;
    [SerializeField]
    protected string _discription;

    public PowerUPRarity Rarity => _rarity;
    public Sprite Icon => _icon;
    public string Discription => _discription;

    public virtual void ApplyUpgrade(CharacterConfiguration characterConfiguration)
    {

    }

    public virtual void RevertUpgrade(CharacterConfiguration characterConfiguration)
    {

    }

    public virtual string GetDiscription()
    {
        return _discription;
    }

}
