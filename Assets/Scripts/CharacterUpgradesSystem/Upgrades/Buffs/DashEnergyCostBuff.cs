using UnityEngine;

[CreateAssetMenu(menuName = "Character upgrades/Stat buff/Dash energy cost", fileName = "Dash energy cost buff")]
public class DashEnergyCostBuff : StatUpgrade
{
    public override void ApplyUpgrade(Player player)
    {
        player.CharacterConfig.DecreaseDashEnergyCost(_value);
    }

    public override void RevertUpgrade(Player player)
    {
        player.CharacterConfig.IncreaseDashEnergyCost(_value);
    }
}
