using System;

[Serializable]
public class SettingsSaveData
{
    public float MasterVolume;
    public float MusicVolume;
    public float SoundsVolume;

    public SettingsSaveData(float masterVolume = 0.5f, float musicVolume = 0.5f, float soundsVolume = 0.5f)
    {
        MasterVolume = masterVolume;
        MusicVolume = musicVolume;
        SoundsVolume = soundsVolume;
    }
}