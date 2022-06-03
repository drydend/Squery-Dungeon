using UnityEngine;

[CreateAssetMenu(menuName = "Character upgrades/Stat debuff/Attack energy cost", fileName = "Attack energy cost debuff")]
public class AttackEnergyCostDebuff : StatUpgrade
{
    public override void ApplyUpgrade(Player player)
    {
        player.CharacterConfig.IncreaseAttackEnergyCost(_value);
    }

    public override void RevertUpgrade(Player player)
    {
        player.CharacterConfig.DecreaseAttackEnergyCost(_value);
    }
}
