public abstract class PlayerUpgrade : Upgrade
{
    public abstract void ApplyUpgrade(Player player);

    public abstract void RevertUpgrade(Player player);
}

