using System;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenager : MonoBehaviour
{
    public static PauseMenager Instance;

    private List<IPausable> _pausables = new List<IPausable>();
    private bool _isPaused;
    private float _previousTimeScale;

    public bool IsPaused => _isPaused;

    public event Action OnGamePaused;
    public event Action OnGameUnpaused;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Register(IPausable pausable)
    {
        _pausables.Add(pausable);
    }

    public void Unregister(IPausable pausable)
    {
        _pausables.Remove(pausable);
    }

    public void Pause()
    {   
        if (_isPaused)
            return;

        OnGamePaused?.Invoke();

        _previousTimeScale = Time.timeScale;
        Time.timeScale = 0;
        _isPaused = true;
        foreach (var item in _pausables)
        {
            item.Pause();
        }
    }

    public void Unpause()
    {
        if (!_isPaused)
            return;

        OnGameUnpaused?.Invoke();

        Time.timeScale = _previousTimeScale;
        _isPaused = false;
        foreach (var item in _pausables)
        {
            item.UnPause();
        }
    }

    private void OnDestroy()
    {
        Unpause();
    }
}
