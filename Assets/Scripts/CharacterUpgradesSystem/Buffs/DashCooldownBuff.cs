using UnityEngine;

[CreateAssetMenu(menuName = "Character upgrades/Stat buff/Dash cooldown", fileName = "Dash cooldown buff")]
public class DashCooldownBuff : StatUpgrade
{
    public override void ApplyUpgrade(Player player)
    {
        player.CharacterConfig.DecreaseDashCooldown(_value);
    }

    public override void RevertUpgrade(Player player)
    {
        player.CharacterConfig.IncreaseDashCooldown(_value);
    }
}
