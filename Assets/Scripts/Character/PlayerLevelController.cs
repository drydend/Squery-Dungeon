using System;
using System.Collections;
using UnityEngine;

public class PlayerLevelController : MonoBehaviour
{
    [SerializeField]
    private Player _player;
    [SerializeField]
    private PowerUPHandler _powerUPHandler;
    private int _numberOfRewards;

    public int NumberOfRewards => _numberOfRewards;

    public event Action OnNumberOfRewardChanged;

    private void Awake()
    {
        _player.OnLevelChanged += () => 
        { 
            _numberOfRewards++;
            OnNumberOfRewardChanged?.Invoke();
        };
    }

    public void ShowRewards()
    {
        StartCoroutine(GiveRewardCoroutine());
    }

    private IEnumerator GiveRewardCoroutine()
    {
        while(_numberOfRewards > 0)
        {
            _powerUPHandler.ShowPowerUps();
            _numberOfRewards--;
            OnNumberOfRewardChanged?.Invoke();
            yield return new WaitUntil(() => _powerUPHandler._playerIsChoosing == false);
        }
    }
}
