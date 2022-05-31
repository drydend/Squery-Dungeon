using UnityEngine;

[CreateAssetMenu(menuName = "Character upgrades/Stat buff/Dash energy cost", fileName = "Dash energy cost buff")]
public class DashEnergyCostBuff : StatUpgrade
{
    public override void ApplyUpgrade(CharacterConfiguration characterConfiguration)
    {
        characterConfiguration.DecreaseDashEnergyCost(_value);
    }

    public override void RevertUpgrade(CharacterConfiguration characterConfiguration)
    {
        characterConfiguration.IncreaseDashEnergyCost(_value);
    }
}
