using UnityEngine;
using UnityEngine.UI;

public class SoundsVolumeAdjuster : MonoBehaviour
{
    [SerializeField]
    private Slider _slider;
    [SerializeField]
    private AudioTuner _audioTuner;
    [SerializeField]
    private SoundVolumeType _soundsVolumeType;

    private void Awake()
    {
        switch (_soundsVolumeType)
        {
            case SoundVolumeType.Master :
                _slider.onValueChanged.AddListener((value) => _audioTuner.SetMasterVolume(value));
                break;
            case SoundVolumeType.Music:
                _slider.onValueChanged.AddListener((value) => _audioTuner.SetMusicVolume(value));
                break;
            case SoundVolumeType.Sounds:
                _slider.onValueChanged.AddListener((value) => _audioTuner.SetSoundsVolume(value));
                break;
        }
    }

    private void OnEnable()
    {
        switch (_soundsVolumeType)
        {
            case SoundVolumeType.Master:
                _slider.value = _audioTuner.MasterVolume;
                break;
            case SoundVolumeType.Music:
                _slider.value = _audioTuner.MusicVolume;
                break;
            case SoundVolumeType.Sounds:
                _slider.value = _audioTuner.SoundsVolume;
                break;
        }
    }
}

