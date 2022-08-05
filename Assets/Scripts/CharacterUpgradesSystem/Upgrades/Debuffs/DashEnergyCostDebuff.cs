using UnityEngine;

[CreateAssetMenu(menuName = "Character upgrades/Stat debuff/Dash energy cost", fileName = "Dash Energy Cost debuff")]
public class DashEnergyCostDebuff : StatUpgrade
{
    public override void ApplyUpgrade(Player player)
    {
        player.CharacterConfig.IncreaseDashEnergyCost(_value);
    }

    public override void RevertUpgrade(Player player)
    {
        player.CharacterConfig.DecreaseDashEnergyCost(_value);
    }
}

