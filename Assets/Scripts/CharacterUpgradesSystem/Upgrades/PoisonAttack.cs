using UnityEngine;

[CreateAssetMenu(menuName = "Character upgrades/Special/Poison attack", fileName = "Poison attack")]
public class PoisonAttack : Upgrade
{
    [SerializeField]
    private Poisoning _poisoningEffect;

    public override void ApplyUpgrade(Player player)
    {
        player.AddEffectToProjectile(_poisoningEffect);
    }

    public override string GetDiscription()
    {
        return base.GetDiscription() + $"Deal {_poisoningEffect.DamagePerTick} damage {_poisoningEffect.NumberOfTicks} times.";
    }
}

