using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Character upgrades/Special/Multi shoot", fileName = "Multi shot")]
public class MultiShot : PlayerUpgrade
{
    [SerializeField]
    private float _angleBetweenProjectiles;
    [SerializeField]
    private int _numberOfProjectiles;

    public override void ApplyUpgrade(Player player)
    {
        player.CharacterConfig.SetAngleBetweenProjectiles(_angleBetweenProjectiles);
        player.CharacterConfig.SetNumberOfProjectiles(_numberOfProjectiles);
    }

    public override void RevertUpgrade(Player player)
    {

    }
}
