using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BackgroundMusicPlayer : MonoBehaviour
{
    [SerializeField]
    private List<AudioClip> _allTracks;
    private AudioSource _audioSource;
    private bool _isPaused;
    private int _currentPlayingClipIndex;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
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
