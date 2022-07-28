using UnityEngine;
using UnityEngine.UI;

public class DashCooldownBarUI : MonoBehaviour
{
    [SerializeField]
    private Player _player;
    [SerializeField]
    private Slider _slider;

    private void Update()
    {
        _slider.value = _player.CurrentDashColldown / _player.DashCooldownTime;
    }
}

