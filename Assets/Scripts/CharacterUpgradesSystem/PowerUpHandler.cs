using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpHandler : MonoBehaviour, IRewardHandler
{
    [SerializeField]
    private PowerUpCreator _powerUpCreator;
    [SerializeField]
    private PowerUpChoiceMenuUI _powerUpUIRenderer;

    [SerializeField]
    private Player _player;

    private PowerUpBlank _selectedPowerUp;

    private List<PowerUp> _currentPowerUps = new List<PowerUp>();

    public void ApplySelectedPowerUp()
    {

        _currentPowerUps.Remove(_selectedPowerUp.CurrentPowerUp);
        
        foreach (var item in _currentPowerUps)
        {
            _powerUpCreator.AddPowerUp(item);
        }

        _player.ApplyPowerUp(_selectedPowerUp.CurrentPowerUp);

        _powerUpUIRenderer.OnPowerUpChoosen();
    }
    
    public void GiveReward()
    {

        for (int i = 0; i < 3; i++)
        {
            _currentPowerUps.Add(_powerUpCreator.GetPowerUp());
        }

        var powerUpsUI = _powerUpUIRenderer.Show(_currentPowerUps);

        foreach (var item in powerUpsUI)
        {
            item.OnSelected += () =>
            {
                _selectedPowerUp?.Unselect();
                _selectedPowerUp = item;
            };

        }
    }
}
