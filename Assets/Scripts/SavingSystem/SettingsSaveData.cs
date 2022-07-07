using System;

[Serializable]
public class SettingsSaveData
{
    public float MasterVolume;
    public float MusicVolume;
    public float SoundsVolume;

    public SettingsSaveData(float masterVolume = default, float musicVolume = default, float soundsVolume = default)
    {
        MasterVolume = masterVolume;
        MusicVolume = musicVolume;
        SoundsVolume = soundsVolume;
    }
}