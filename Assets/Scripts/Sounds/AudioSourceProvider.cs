using System;
using UnityEngine;

public class AudioSourceProvider : MonoBehaviour
{   
    public static AudioSourceProvider Instance { get; private set; }

    [SerializeField]
    private AudioSource _soundsSource;
    [SerializeField]
    private AudioSource _musicSource;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            throw new Exception("Audio Source Provider can be only one");
        }
    }

    public AudioSource GetMusicSource()
    {
        return _musicSource;
    }
    
    public AudioSource GetSoundsSource()
    {
        return _soundsSource;
    }
}

