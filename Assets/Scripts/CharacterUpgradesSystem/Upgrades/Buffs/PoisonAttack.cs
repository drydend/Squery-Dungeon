using UnityEngine;

[CreateAssetMenu(menuName = "Character upgrades/Special/Poison attack", fileName = "Poison attack")]
public class PoisonAttack : PlayerUpgrade
{
    [SerializeField]
    private Poisoning _poisoningEffect;

    public override void ApplyUpgrade(Player player)
    {
        player.AddEffectToProjectile(_poisoningEffect);
    }

    public override void RevertUpgrade(Player player)
    {
       
    }
}

