using UnityEngine;

[CreateAssetMenu(menuName = "Character upgrades/Stat buff/Max heals", fileName = "Max heals buff")]
public class MaxHealsBuff : StatUpgrade
{
    public override void ApplyUpgrade(CharacterConfiguration characterConfiguration)
    {
        characterConfiguration.IncreaseMaxHeals(_value);
    }

    public override void RevertUpgrade(CharacterConfiguration characterConfiguration)
    {
        characterConfiguration.DecreaseMaxHeals(_value);
    }
}
