using UnityEngine;

[CreateAssetMenu(menuName = "Character upgrades/Stat buff/Dash cooldown", fileName = "Dash cooldown buff")]
public class DashCooldownBuff : StatUpgrade
{
    public override void ApplyUpgrade(CharacterConfiguration characterConfiguration)
    {
        characterConfiguration.DecreaseDashCooldown(_value);
    }

    public override void RevertUpgrade(CharacterConfiguration characterConfiguration)
    {
        characterConfiguration.IncreaseDashCooldown(_value);
    }
}
