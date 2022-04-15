public class UpgradeDiscriptionParser
{
    private Upgrade _upgrade;

    public UpgradeDiscriptionParser(Upgrade powerUp)
    {
        _upgrade = powerUp;
    }

    public string GetDiscription()
    {
        if (typeof(StatUpgrade) == _upgrade.GetType())
        {
            StatUpgrade statUpgrade = _upgrade as StatUpgrade;
            string statValue = "+" + statUpgrade.Value.ToString();

            return statUpgrade.Discription + " " + statValue;
        }
        else
        {
            return _upgrade.Discription;
        }
    }

}