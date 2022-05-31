using UnityEngine;

[CreateAssetMenu(menuName = "Character upgrades/Stat buff/Attack speed", fileName = "Attack speed buff")]
public class AttackSpeedBuff : StatUpgrade
{
    public override void ApplyUpgrade(CharacterConfiguration characterConfiguration)
    {
        characterConfiguration.IncreaseAttackSpeed(_value);
    }

    public override void RevertUpgrade(CharacterConfiguration characterConfiguration)
    {
        characterConfiguration.DecreaseAttackSpeed(_value);
    }
}

