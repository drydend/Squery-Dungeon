using System.Collections.Generic;
using UnityEngine;

public class PowerUPHandler : MonoBehaviour
{
    [SerializeField]
    private PowerUPCreator _upgradeCreator;
    [SerializeField]
    private PowerUPChoiceMenuUI _upgradeChoiceMenu;
    [SerializeField]
    private ModificatorApplicator _modificatorApplicator;

    private PowerUPBlank _selectedUpgrade;

    private List<Modificator> _currentModificators = new List<Modificator>();
    private int _numberOfRewardsLeft = 0;

    public bool _playerIsChoosing { get; private set; }

    private void Start()
    {
        _upgradeChoiceMenu.OnConfrimedChoice += ConfrimChoice;
    }

    public void ApplySelectedModificator()
    {   
        _currentModificators.Remove(_selectedUpgrade.CurrentModificator);
        
        foreach (var item in _currentModificators)
        {
            _upgradeCreator.AddModificatorToPool(item);
        }

        foreach (var modificator in _selectedUpgrade.CurrentModificator.DerivedModificators)
        {
            _upgradeCreator.AddModificatorToPool(modificator);
        }

        _currentModificators.Clear();

        _modificatorApplicator.ApplyModificator(_selectedUpgrade.CurrentModificator);

        _selectedUpgrade = null;
        _upgradeChoiceMenu.OnModificatorChoosen();

        if(_numberOfRewardsLeft == 0)
        {
            _upgradeChoiceMenu.Close();
        }

        _playerIsChoosing = false;
    }
    
    public void ShowRawardsMenu(int numberOfRewardsLeft = 0)
    {
        _numberOfRewardsLeft = numberOfRewardsLeft;
        _playerIsChoosing = true;

        for (int i = 0; i < 3; i++)
        {
            _currentModificators.Add(_upgradeCreator.GetRandomModificator());
        }

        var powerUpsUI = _upgradeChoiceMenu.Show(_currentModificators);

        foreach (var item in powerUpsUI)
        {
            item.OnSelected += OnUpgradeSelected;
        }
    }

    private void OnUpgradeSelected(PowerUPBlank upgradeBlank)
    {
        _selectedUpgrade?.Unselect();
        _selectedUpgrade = upgradeBlank;
    }

    private void ConfrimChoice()
    {
        if(_selectedUpgrade == null)
        {
            return;
        }

        ApplySelectedModificator();
    }
}
