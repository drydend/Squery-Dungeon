using UnityEngine;

[CreateAssetMenu(menuName = "Character upgrades/Stat buff/Attack speed", fileName = "Attack speed buff")]
public class AttackSpeedBuff : StatUpgrade
{
    public override void ApplyUpgrade(Player player)
    {
        player.CharacterConfig.IncreaseAttackSpeed(_value);
    }

    public override void RevertUpgrade(Player player)
    {
        player.CharacterConfig.DecreaseAttackSpeed(_value);
    }
}

