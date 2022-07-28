using UnityEngine;

[CreateAssetMenu(menuName = "Character upgrades/Stat buff/Attack energy cost", fileName = "Attack energy cost buff")]
public class AttackEnergyCostBuff : StatUpgrade
{
    public override void ApplyUpgrade(Player player)
    {
        player.CharacterConfig.DecreaseAttackEnergyCost(_value);
    }

    public override void RevertUpgrade(Player player)
    {
        player.CharacterConfig.IncreaseAttackEnergyCost(_value);
    }
}

