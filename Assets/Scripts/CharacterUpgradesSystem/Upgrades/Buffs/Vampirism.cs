using UnityEngine;

[CreateAssetMenu(menuName = "Character upgrades/Special/Vampirism", fileName = "Vampirism")]
public class Vampirism : PlayerEnemyUpgrade
{
    [SerializeField]
    [Range(0, 1f)]
    private float _chanceToHeal;
    [SerializeField]
    private int _healsToHeal;
    private Player _player;

    public override void ApplyUpgrade(Player player, Enemy enemy)
    {
        _player = player;
        enemy.OnDied += TryHealPlayer;
    }

    public override void RevertUpgrade(Player player, Enemy enemy)
    {
        enemy.OnDied -= TryHealPlayer;
    }

    public void TryHealPlayer()
    {
        if (Random.Range(0, 1f) < _chanceToHeal)
        {
            _player.CurrentCharacter.Heal(_healsToHeal);
        }
    }

}
