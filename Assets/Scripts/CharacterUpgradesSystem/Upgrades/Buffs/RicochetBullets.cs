using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Character upgrades/Special/Ricochet bullets", fileName = "Ricochet Bullets")]
public class RicochetBullets : PlayerUpgrade
{
    [SerializeField]
    private BulletRicochetHitBehaviour bulletHitBehaviour;

    public override void ApplyUpgrade(Player player)
    {
        player.CharacterConfig.SetBulletsHitBehaviour(bulletHitBehaviour);
    }

    public override void RevertUpgrade(Player player)
    {

    }
}

