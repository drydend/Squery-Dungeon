using UnityEngine;

public class LevelTimer : MonoBehaviour
{
    private double _timePlayedInSeconds;
    
    private void Update()
    {
        if (PauseMenager.Instance.IsPaused)
        {
            return;
        }

        _timePlayedInSeconds += Time.deltaTime;
    }

    public void GetPlayedTime(out int minutes, out int seconds)
    {
        minutes = (int)(_timePlayedInSeconds / 60);
        seconds = (int)(_timePlayedInSeconds - minutes * 60);
    }

}

