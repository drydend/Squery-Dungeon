using System;

[Serializable]
public class SettingsSaveData
{
    public float MasterVolume;
    public float MusicVolume;
    public float SoundsVolume;

    public SettingsSaveData(float masterVolume = 0.5f, float musicVolume = default, float soundsVolume = default)
    {
        MasterVolume = masterVolume;
        MusicVolume = musicVolume;
        SoundsVolume = soundsVolume;
    }
}