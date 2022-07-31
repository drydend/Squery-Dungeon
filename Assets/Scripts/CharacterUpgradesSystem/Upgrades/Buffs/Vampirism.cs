using UnityEngine;

[CreateAssetMenu(menuName = "Character upgrades/Special/Vampirism", fileName = "Vampirism")]
public class Vampirism : PlayerEnemyUpgrade
{
    [SerializeField]
    [Range(0, 1f)]
    private float _chanceToHeal;
    [SerializeField]
    private int _healsToHeal;

    public override void ApplyUpgrade(Player player, Enemy enemy)
    {
        enemy.OnDied += () => TryHealPlayer(player);
    }

    public void TryHealPlayer(Player player)
    {
        if (Random.Range(0, 1f) > _chanceToHeal)
        {
            player.CurrentCharacter.Heal(_healsToHeal);
        }
    }

}
