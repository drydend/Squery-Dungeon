using System;

public class Timer
{
    public event Action OnFinished;

    private float _secondsToFinish;
    private float _remainingSeconds;
    private bool _isPaused = false;

    public bool IsFinished { get; private set; }

    public Timer(float seconds)
    {
        _secondsToFinish = seconds;
        OnFinished += () => Pause();
        OnFinished += () => IsFinished = true;
    }

    public void SetSecondsToFinish(float seconds)
    {
        _secondsToFinish = seconds;
        _remainingSeconds = _secondsToFinish;
    }

    public void Unpause()
    {
        _isPaused = false;
    }

    public void Pause()
    {
        _isPaused = true;
    }

    public void UpdateTick(float deltaTime)
    {
        if (!_isPaused)
        {
            _remainingSeconds -= deltaTime;

            if (_remainingSeconds <= 0)
            {
                OnFinished?.Invoke();
            }
        }
    }

    public void ResetTimer()
    {
        IsFinished = false;
        _remainingSeconds = _secondsToFinish;
        Unpause();
    }
}

