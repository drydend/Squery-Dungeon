using UnityEngine;

public class RewardHandlerOfFinalRoom : MonoBehaviour ,IRewardHandler
{
    [SerializeField]
    private WinController _winController;

    public void GiveReward()
    {
        _winController.PlayerWin();
    }
}