using UnityEngine;
using UnityEngine.UI;

public class PlayerHealsUI : MonoBehaviour
{
    [SerializeField]
    private Player _player;
    [SerializeField]
    private Slider _healsBar;

    private void Awake()
    {
        _player.OnCurrentCharacterHealsChanged += UpdateUI;
        _player.OnCurrentCharacterMaxHealsChanged += UpdateUI;
    }

    private void Start()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        _healsBar.value = _player.CurrentHealsPoints / _player.MaxHealsPoints;
    }
}
