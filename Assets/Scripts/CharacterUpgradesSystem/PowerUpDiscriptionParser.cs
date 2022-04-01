public class PowerUpDiscriptionParser
{
    private PowerUp _powerUp;

    public PowerUpDiscriptionParser(PowerUp powerUp)
    {
        _powerUp = powerUp;
    }

    public string GetDiscription()
    {
        if (typeof(StatUpgrade) == _powerUp.GetType())
        {
            StatUpgrade statUpgrade = _powerUp as StatUpgrade;
            string statValue = "+" + statUpgrade.Value.ToString();

            return statUpgrade.Discription + " " + statValue;
        }
        else
        {
            return _powerUp.Discription;
        }
    }

}