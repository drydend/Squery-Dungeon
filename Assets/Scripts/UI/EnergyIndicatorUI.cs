using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnergyIndicatorUI : MonoBehaviour
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

