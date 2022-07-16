using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicPlayer : MonoBehaviour
{
    [SerializeField]
    private List<AudioClip> _allTracks;
    private bool _isPaused;
    private int _currentPlayingClipIndex;
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = AudioSourceProvider.Instance.GetMusicSource();
        RestartPlaying();
    }

    private void Update()
    {
        if (_isPaused || _audioSource.isPlaying)
        {
            return;
        }

        if (_allTracks.Count - 1 == _currentPlayingClipIndex)
        {
            RestartPlaying();
        }
        else
        {
            _currentPlayingClipIndex++;
            _audioSource.PlayOneShot(_allTracks[_currentPlayingClipIndex]);
        }
    }

    public void Pause()
    {
        _isPaused = true;
        _audioSource.Pause();
    }

    public void UnPause()
    {
        _isPaused = false;
        _audioSource.UnPause();
    }

    private void RestartPlaying()
    {
        _audioSource.Stop();
        RandomUtils.ShuffleList(_allTracks);
        _currentPlayingClipIndex = 0;
        _audioSource.PlayOneShot(_allTracks[_currentPlayingClipIndex]);
    }
}
