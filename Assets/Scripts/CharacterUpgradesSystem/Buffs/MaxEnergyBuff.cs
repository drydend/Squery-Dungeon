using UnityEngine;

[CreateAssetMenu(menuName = "Character upgrades/Stat buff/Max energy ", fileName = "Max energy buff")]
public class MaxEnergyBuff : StatUpgrade
{
    public override void ApplyUpgrade(CharacterConfiguration characterConfiguration)
    {
        characterConfiguration.IncreaseMaxEnergy(_value);
    }

    public override void RevertUpgrade(CharacterConfiguration characterConfiguration)
    {
        characterConfiguration.DecreaseMaxEnergy(_value);
    }
}
