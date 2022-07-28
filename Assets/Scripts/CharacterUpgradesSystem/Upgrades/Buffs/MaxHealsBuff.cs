using UnityEngine;

[CreateAssetMenu(menuName = "Character upgrades/Stat buff/Max heals", fileName = "Max heals buff")]
public class MaxHealsBuff : StatUpgrade
{
    public override void ApplyUpgrade(Player player)
    {
        player.CharacterConfig.IncreaseMaxHeals(_value);
    }

    public override void RevertUpgrade(Player player)
    {
        player.CharacterConfig.DecreaseMaxHeals(_value);
    }
}
