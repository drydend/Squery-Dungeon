using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealsBarUI : MonoBehaviour
{
    [SerializeField]
    private Player _player;
    [SerializeField]
    private Slider _healsBar;
    [SerializeField]
    private TMP_Text _text;

    private void Awake()
    {
        _player.OnCharacterHealsChanged += UpdateUI;
        _player.OnCharacterMaxHealsChanged += UpdateUI;
    }

    private void Start()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        _healsBar.value = _player.CurrentHealsPoints / _player.MaxHealsPoints;
        _text.text = $"{_player.CurrentHealsPoints}/{_player.MaxHealsPoints}";
    }
}
