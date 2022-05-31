using UnityEngine;

[CreateAssetMenu(menuName = "Character upgrades/Stat buff/Projectile damage", fileName = "Projectile damage buff")]
public class ProjectileDamageBuff : StatUpgrade
{
    public override void ApplyUpgrade(CharacterConfiguration characterConfiguration)
    {
        characterConfiguration.IncreaseProjectileDamage(_value);
    }

    public override void RevertUpgrade(CharacterConfiguration characterConfiguration)
    {
        characterConfiguration.DecreaseProjectileDamage(_value);
    }
}

