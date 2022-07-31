using UnityEngine;

[CreateAssetMenu(menuName = "Character upgrades/Stat buff/Dodge chance buff", fileName = "Dodge Chance")]
public class DodgeChance : StatUpgrade
{
    public override void ApplyUpgrade(Player player)
    {
        player.CharacterConfig.IncreaseDodgeChance(_value);
    }

    public override void RevertUpgrade(Player player)
    {
        player.CharacterConfig.DecreaseDodgeChance(_value);
    }

}
