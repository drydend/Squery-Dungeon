public abstract class EnemyUpgrade : Upgrade
{
    public abstract void ApplyUpgrade(Enemy enemy);

    public abstract void RevertUpgrade(Enemy enemy);
}