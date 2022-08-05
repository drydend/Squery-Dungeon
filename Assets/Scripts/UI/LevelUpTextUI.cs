using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelUpTextUI : MonoBehaviour
{
    [SerializeField]
    private PlayerLevelRewarder _playerLevelController;
    [SerializeField]
    private List<TMP_Text> _text;

    private void Awake()
    {
        _playerLevelController.OnNumberOfRewardChanged += UpdateText;
    }

    private void Start()
    {
        UpdateText();
    }

    private void UpdateText()
    {
        foreach (var text in _text)
        {
            text.gameObject.SetActive(_playerLevelController.NumberOfRewards > 0);
        }
    }
}
