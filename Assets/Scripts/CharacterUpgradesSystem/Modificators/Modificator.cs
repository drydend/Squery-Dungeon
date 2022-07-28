using UnityEngine;
using System.Collections.Generic;
using System.Text;
using System.Linq;

[CreateAssetMenu(menuName ="Modificator" , fileName = "Modificator" )]
public class Modificator : ScriptableObject
{
    [SerializeField]
    private List<Modificator> _derivedModificators = new List<Modificator>(0);

    [SerializeField]
    private List<Upgrade> _upgrades;
    [SerializeField]
    private Sprite _icon;
    [SerializeField]
    private string _discription;
    [SerializeField]
    private PowerUPRarity _rarity;

    public List<Upgrade> Upgrades => _upgrades;
    public List<Modificator> DerivedModificators => _derivedModificators;
    public Sprite Icon => _icon;

    public string GetDiscription()
    {
        return _discription;
    }

    public PowerUPRarity GetRarity()
    {
        return _rarity;
    }
}
