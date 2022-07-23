using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnergyBarUI : MonoBehaviour
{
    [SerializeField]
    private Player _player;
    [SerializeField]
    private Slider _slider;

    private void Update()
    {
        _slider.value = _player.CurrentEnergy / _player.MaxEnergy;
    }
}

