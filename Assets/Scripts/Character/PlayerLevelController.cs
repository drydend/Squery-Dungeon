using System;
using System.Collections;
using UnityEngine;

public class PlayerLevelController : MonoBehaviour
{
    [SerializeField]
    private Player _player;
    [SerializeField]
    private PowerUPHandler _powerUPHandler;
    private int _numberOfReward;

    private void Awake()
    {
        _player.OnLevelChanged += () => _numberOfReward++;
        _player.OnLevelUp += GiveReward;
    }

    private void GiveReward()
    {
        StartCoroutine(GiveRewardCoroutine());
    }

    private IEnumerator GiveRewardCoroutine()
    {
        while(_numberOfReward > 0)
        {
            _powerUPHandler.ShowPowerUps();
            _numberOfReward--;
            yield return new WaitUntil(() => _powerUPHandler._playerIsChoosing == false);
        }
    }
}
