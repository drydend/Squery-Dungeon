using UnityEngine;
using System.Collections.Generic;
using System.Text;
using System.Linq;

[CreateAssetMenu(menuName ="Character modificator" , fileName = "Character modificator" )]
public class CharacterModificator : ScriptableObject
{
    [SerializeField]
    private List<Upgrade> _powerUPs;

    public void ApplyModificator(Player player)
    {
        foreach (var powerUP in _powerUPs)
        {
            player.ApplyPowerUP(powerUP);
        }
    }

    public void RevertModificator(Player player)
    {
        foreach (var powerUP in _powerUPs)
        {
            player.ApplyPowerUP(powerUP);
        }
    }

    public string GetDiscription()
    {
        var stringBuilder = new StringBuilder();

        foreach (var upgrade in _powerUPs)
        {
            stringBuilder.Append(upgrade.GetDiscription());
            stringBuilder.Append(".");
            stringBuilder.Append("\n");
        }

        return stringBuilder.ToString();
    }

    public List<Sprite> GetIcons()
    {
        return _powerUPs.Select(powerUP => powerUP.Icon).ToList();
    }

    public PowerUPRarity GetRarity()
    {
        return _powerUPs.Max(x => x.Rarity);
    }
}
