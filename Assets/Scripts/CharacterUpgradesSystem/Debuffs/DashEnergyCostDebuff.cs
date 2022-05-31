using UnityEngine;

[CreateAssetMenu(menuName = "Character upgrades/Stat debuff/Dash energy cost", fileName = "Dash energy cost debuff")]
public class DashEnergyCostDebuff : StatUpgrade
{
    public override void ApplyUpgrade(CharacterConfiguration characterConfiguration)
    {
        characterConfiguration.IncreaseDashEnergyCost(_value);
    }

    public override void RevertUpgrade(CharacterConfiguration characterConfiguration)
    {
        characterConfiguration.DecreaseDashEnergyCost(_value);
    }
}

