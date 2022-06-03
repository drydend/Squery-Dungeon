using UnityEngine;

[CreateAssetMenu(menuName = "Character upgrades/Stat buff/Max energy ", fileName = "Max energy buff")]
public class MaxEnergyBuff : StatUpgrade
{
    public override void ApplyUpgrade(Player player)
    {
        player.CharacterConfig.IncreaseMaxEnergy(_value);
    }

    public override void RevertUpgrade(Player player)
    {
        player.CharacterConfig.DecreaseMaxEnergy(_value);
    }
}
