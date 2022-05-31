using UnityEngine;

[CreateAssetMenu(menuName = "Character upgrades/Stat buff/Movement speed", fileName = "Movement speed buff")]
public class MovementSpeedBuff : StatUpgrade
{
    public override void ApplyUpgrade(CharacterConfiguration characterConfiguration)
    {
        characterConfiguration.IncreaseMovementSpeed(_value);
    }

    public override void RevertUpgrade(CharacterConfiguration characterConfiguration)
    {
        characterConfiguration.DecreaseMovementSpeed(_value);
    }
}
