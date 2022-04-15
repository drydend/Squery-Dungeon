using System.Collections.Generic;
using UnityEngine;

public class UpgradeHandler : MonoBehaviour, IRewardHandler
{
    [SerializeField]
    private UpgradeCreator _upgradeCreator;
    [SerializeField]
    private UpgradeChoiceMenuUI _upgradeChoiceMenu;

    [SerializeField]
    private Player _player;

    private UpgradeBlank _selectedUpgrade;

    private List<Upgrade> _currentUpgrades = new List<Upgrade>();

    public void ApplySelectedPowerUp()
    {
        _currentUpgrades.Remove(_selectedUpgrade.CurrentUpgrade);
        
        foreach (var item in _currentUpgrades)
        {
            _upgradeCreator.AddPowerUp(item);
        }

        _currentUpgrades.Clear();

        _player.ApplyUpgrade(_selectedUpgrade.CurrentUpgrade);

        _selectedUpgrade = null;
        _upgradeChoiceMenu.OnPowerUpChoosen();
    }
    
    public void GiveReward()
    {
        for (int i = 0; i < 3; i++)
        {
            _currentUpgrades.Add(_upgradeCreator.GetPowerUp());
        }

        var powerUpsUI = _upgradeChoiceMenu.Show(_currentUpgrades);

        foreach (var item in powerUpsUI)
        {
            item.OnSelected += OnUpgradeSelected;
        }
    }

    private void OnUpgradeSelected(UpgradeBlank upgradeBlank)
    {
        _selectedUpgrade?.Unselect();
        _selectedUpgrade = upgradeBlank;
    }
}
