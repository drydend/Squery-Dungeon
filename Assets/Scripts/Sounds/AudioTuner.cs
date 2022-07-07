using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioTuner : MonoBehaviour, ISaveable
{
    private const string MasterVolumeParameterName = "MasterVolume";
    private const string MusicVolumeParameterName = "MusicVolume";
    private const string SoundsVolumeParameterName = "SoundsVolume";

    [SerializeField]
    private AudioMixerGroup _audioMixer;

    public float MasterVolume { get; private set;  }
    public float MusicVolume { get; private set;  }
    public float SoundsVolume { get; private set;  }

    private void Awake()
    {
        SaveController.Instance.Subscribe(this);
    }

    private void OnDestroy()
    {
        SaveController.Instance.UnSubcribe(this);
    }

    public void SetMasterVolume(float value)
    {
        MasterVolume = value;
        _audioMixer.audioMixer.SetFloat(MasterVolumeParameterName, GetVolumeInDecibels(value));
    }

    public void SetMusicVolume(float value)
    {
        MusicVolume = value;
        _audioMixer.audioMixer.SetFloat(MusicVolumeParameterName, GetVolumeInDecibels(value));
    }

    public void SetSoundsVolume(float value)
    {
        SoundsVolume = value;
        _audioMixer.audioMixer.SetFloat(SoundsVolumeParameterName, GetVolumeInDecibels(value));
    }

    public void SaveData(SaveData saveData)
    {
        saveData.SettingsData = new SettingsSaveData(MasterVolume, MusicVolume, SoundsVolume);
    }

    public void LoadData(SaveData saveData)
    {
        var settingSaveData = saveData.SettingsData;

        SetMasterVolume(settingSaveData.MasterVolume);
        SetMusicVolume(settingSaveData.MusicVolume);
        SetSoundsVolume(settingSaveData.SoundsVolume);
    }

    private float GetVolumeInDecibels(float value)
    {
        return Mathf.Log10(value) * 20;
    }
}
