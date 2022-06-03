using UnityEngine;

[CreateAssetMenu(menuName = "Character upgrades/Stat buff/Movement speed", fileName = "Movement speed buff")]
public class MovementSpeedBuff : StatUpgrade
{
    public override void ApplyUpgrade(Player player)
    {
        player.CharacterConfig.IncreaseMovementSpeed(_value);
    }

    public override void RevertUpgrade(Player player)
    {
        player.CharacterConfig.DecreaseMovementSpeed(_value);
    }
}
