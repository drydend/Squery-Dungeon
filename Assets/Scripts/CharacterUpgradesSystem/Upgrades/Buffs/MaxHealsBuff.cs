using UnityEngine;

[CreateAssetMenu(menuName = "Character upgrades/Stat buff/Max heals", fileName = "Max heals buff")]
public class MaxHealsBuff : StatUpgrade
{
    public override void ApplyUpgrade(Player player)
    {
        var percentOfHeals = player.CurrentHealsPoints / player.MaxHealsPoints;

        player.CharacterConfig.IncreaseMaxHeals(_value);

        player.CurrentCharacter.Heal((int)(player.MaxHealsPoints * percentOfHeals - player.CurrentHealsPoints));
    }

    public override void RevertUpgrade(Player player)
    {
        player.CharacterConfig.DecreaseMaxHeals(_value);
    }
}
