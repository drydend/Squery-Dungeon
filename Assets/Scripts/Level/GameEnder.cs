using UnityEngine;

public class GameEnder : MonoBehaviour
{
    [SerializeField]
    private Player _player;
    [SerializeField]
    private DeathScrene _deathScrene;

    private void Awake()
    {
        _player.OnCharacterDied += EndGame;
    }

    private void EndGame()
    {
        PauseMenager.Instance.Pause();
        _deathScrene.Open();
    }
}

