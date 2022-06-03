using UnityEngine;

[CreateAssetMenu(menuName = "Character upgrades/Stat buff/Passsive energy recovery", fileName = "Passsive energy recovery buff")]
public class PassiveEnergyRecoveryBuff : StatUpgrade
{
    public override void ApplyUpgrade(Player player)
    {
        player.CharacterConfig.IncreasePassiveEnergyRecovery(_value);
    }

    public override void RevertUpgrade(Player player)
    {
        player.CharacterConfig.DecreasePassiveEnergyRecovery(_value);
    }
}
