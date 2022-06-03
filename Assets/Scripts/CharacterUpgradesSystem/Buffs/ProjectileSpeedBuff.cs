using UnityEngine;

[CreateAssetMenu(menuName = "Character upgrades/Stat buff/Projectile speed", fileName = "Projectile speed buff")]
public class ProjectileSpeedBuff : StatUpgrade
{
    public override void ApplyUpgrade(Player player)
    {
        player.CharacterConfig.IncreaseProjectileSpeed(_value);
    }

    public override void RevertUpgrade(Player player)
    {
        player.CharacterConfig.DecreaseProjectileSpeed(_value);
    }
}
