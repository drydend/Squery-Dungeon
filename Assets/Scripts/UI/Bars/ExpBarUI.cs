using UnityEngine;
using UnityEngine.UI;

public class ExpBarUI : MonoBehaviour
{
    [SerializeField]
    private Player _player;
    [SerializeField]
    private Slider _expBar;

    private void Awake()
    {
        _player.OnCurrentExpChanged += UpdateUI;
    }

    private void Start()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        _expBar.value = (float)_player.CurrentExp / (float)_player.ExpToNextLevel;
    }

}

