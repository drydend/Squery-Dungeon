using UnityEngine;

[CreateAssetMenu(menuName = "Character upgrades/Stat buff/Passsive energy recovery", fileName = "Passsive energy recovery buff")]
public class PassiveEnergyRecoveryBuff : StatUpgrade
{
    public override void ApplyUpgrade(CharacterConfiguration characterConfiguration)    
    {
        characterConfiguration.IncreasePassiveEnergyRecovery(_value);
    }

    public override void RevertUpgrade(CharacterConfiguration characterConfiguration)
    {
        characterConfiguration.DecreasePassiveEnergyRecovery(_value);
    }
}
