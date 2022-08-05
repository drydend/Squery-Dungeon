using UnityEngine;

[CreateAssetMenu(menuName = "Character upgrades/Stat debuff/Attack damage ", fileName = "Attack Damage Debuff")]
public class AttackDamageDebuffInPercent : StatUpgrade 
{
    public override void ApplyUpgrade(Player player)
    {
        player.CharacterConfig.DecreaseAttackDamageMultiplier(_value);
    }

    public override void RevertUpgrade(Player player)
    {
        player.CharacterConfig.IncreaseAttackDamageMultiplier(_value);
    }

}

