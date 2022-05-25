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
        else if(typeof(BulletHitBehaviourUpgrade) == _upgrade.GetType())
        {
            var bulletHitUpgrade = (BulletHitBehaviourUpgrade)_upgrade;
            if(bulletHitUpgrade.NewHitBehaviour.GetType() == typeof(BulletRicochetHitBehaviour))
            {
                var ricochetHitBechaviour = (BulletRicochetHitBehaviour)bulletHitUpgrade.NewHitBehaviour;
                return _upgrade.Discription + $" Bounce number: {ricochetHitBechaviour.RicochetNumber}"
                    + $" Damage decreasing afte bounce: {ricochetHitBechaviour.DamageDecreasing}"; 
            }
            else
            {
                return _upgrade.Discription;
            }
        }
        else
        {
            return _upgrade.Discription;
        }
    }

}