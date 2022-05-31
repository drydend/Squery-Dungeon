using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnergyIndicatorUI : MonoBehaviour
{
    [SerializeField]
    private Player _player;
    [SerializeField]
    private Slider _slider;
    [SerializeField]
    private TMP_Text _text;

    private void Update()
    {
        _slider.value = _player.CurrentEnergy / _player.MaxEnergy;
        _text.text = $"{Mathf.RoundToInt(_player.CurrentEnergy)}/{_player.MaxEnergy}";
    }
}

