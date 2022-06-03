using UnityEngine;

[CreateAssetMenu(menuName = "Character upgrades/Stat buff/Projectile damage", fileName = "Projectile damage buff")]
public class ProjectileDamageBuff : StatUpgrade
{
    public override void ApplyUpgrade(Player player)
    {
        player.CharacterConfig.IncreaseProjectileDamage(_value);
    }

    public override void RevertUpgrade(Player player)
    {
        player.CharacterConfig.DecreaseProjectileDamage(_value);
    }
}

