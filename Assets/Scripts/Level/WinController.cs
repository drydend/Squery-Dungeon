using UnityEngine;

public class WinController : MonoBehaviour
{
    [SerializeField]
    private WinScrene _winScrene;

    public void PlayerWin()
    {
        PauseMenager.Instance.Pause();
        _winScrene.Open();
    }
}

