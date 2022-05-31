using UnityEngine;

[CreateAssetMenu(menuName = "Character upgrades/Stat buff/Attack energy cost", fileName = "Attack energy cost buff")]
public class AttackEnergyCostBuff : StatUpgrade
{
    public override void ApplyUpgrade(CharacterConfiguration characterConfiguration)
    {
        characterConfiguration.DecreaseAttackEnergyCost(_value);
    }

    public override void RevertUpgrade(CharacterConfiguration characterConfiguration)
    {
        characterConfiguration.IncreaseAttackEnergyCost(_value);
    }
}

