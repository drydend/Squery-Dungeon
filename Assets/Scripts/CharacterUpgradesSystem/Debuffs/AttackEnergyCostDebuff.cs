using UnityEngine;

[CreateAssetMenu(menuName = "Character upgrades/Stat debuff/Attack energy cost", fileName = "Attack energy cost debuff")]
public class AttackEnergyCostDebuff : StatUpgrade
{
    public override void ApplyUpgrade(CharacterConfiguration characterConfiguration)
    {
        characterConfiguration.IncreaseAttackEnergyCost(_value);
    }

    public override void RevertUpgrade(CharacterConfiguration characterConfiguration)
    {
        characterConfiguration.DecreaseAttackEnergyCost(_value);
    }
}
