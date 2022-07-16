using UnityEngine;
using TMPro;

public class LevelCompletingTimerUI : MonoBehaviour
{
    [SerializeField]
    private LevelTimer _levelTimer;
    [SerializeField]
    private TMP_Text _text;

    private void OnEnable()
    {
        UpdateTime();
    }

    private void UpdateTime()
    {
        var seconds = 0;
        var minutes = 0;

        _levelTimer.GetPlayedTime(out minutes, out seconds);

        _text.text = $"{minutes}:{seconds}";
    }
}

