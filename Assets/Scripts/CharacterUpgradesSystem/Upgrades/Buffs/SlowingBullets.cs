using UnityEngine;

[CreateAssetMenu(menuName = "Character upgrades/Special/Slowing bullets", fileName = "Slowing bullets")]
public class SlowingBullets : PlayerUpgrade
{
    [SerializeField]
    private Slowness _slownessEffect;

    public override void ApplyUpgrade(Player player)
    {
        player.CharacterConfig.AddEffectToProjectile(_slownessEffect);
    }

    public override void RevertUpgrade(Player player)
    {
    }
}
