﻿using System;

public class Timer
{
    public event Action OnFinished;

    private float _secondsToFinish;
    private float _remainingSeconds;
    private bool _isPaused = false;

    public bool IsFinished { get; private set; }
    public float SecondsPassed { get; private set; }
    public float SecondsToFinish => _secondsToFinish;

    public Timer(float seconds)
    {
        _secondsToFinish = seconds;
        _remainingSeconds = _secondsToFinish;
        OnFinished += Pause;
        OnFinished += () => IsFinished = true;
    }

    public void SetSecondsToFinish(float seconds)
    {
        _secondsToFinish = seconds;
        _remainingSeconds = _secondsToFinish;
        ResetTimer();
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
        if (_isPaused)
        {
            return;
        }

        _remainingSeconds -= deltaTime;
        SecondsPassed += deltaTime;

        if (_remainingSeconds <= 0)
        {
            OnFinished?.Invoke();
        }
    }

    public void FinishTimer()
    {
        OnFinished?.Invoke();
        SecondsPassed = _secondsToFinish;
        IsFinished = true;
        Pause();
    }

    public void ResetTimer()
    {
        SecondsPassed = 0;
        IsFinished = false;
        _remainingSeconds = _secondsToFinish;
        Unpause();
    }
}

