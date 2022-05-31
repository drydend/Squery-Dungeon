using UnityEngine;

[CreateAssetMenu(menuName = "Character upgrades/Stat buff/Projectile speed", fileName = "Projectile speed buff")]
public class ProjectileSpeedBuff : StatUpgrade
{
    public override void ApplyUpgrade(CharacterConfiguration characterConfiguration)
    {
        characterConfiguration.IncreaseProjectileSpeed(_value);
    }

    public override void RevertUpgrade(CharacterConfiguration characterConfiguration)
    {
        characterConfiguration.DecreaseProjectileSpeed(_value);
    }
}
