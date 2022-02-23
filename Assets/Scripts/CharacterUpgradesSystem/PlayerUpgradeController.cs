using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgradeController : MonoBehaviour
{
    [SerializeField]
    private Player _player;

    public void UpgradePlayersCharacterPermanent(CharacterPermanentStatsUpgrade upgrade)
    {
        if (upgrade.UpgradeType == UpgradeType.IncreaseStat) 
            _player.IncreaseCharactersStat(upgrade.CharacterType, upgrade.StatType, upgrade.Value);
        else if (upgrade.UpgradeType == UpgradeType.DecreaseStat)
            _player.DecreaseCharactersStat(upgrade.CharacterType, upgrade.StatType, upgrade.Value);
    }



}
