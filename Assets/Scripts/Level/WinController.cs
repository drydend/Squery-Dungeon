using UnityEngine;

public class WinController : MonoBehaviour
{
    [SerializeField]
    private WinScrene _winScrene;
    [SerializeField]
    private AudioClip _winSound;
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = AudioSourceProvider.Instance.GetSoundsSource();
    }

    public void PlayerWin()
    {
        _audioSource.PlayOneShot(_winSound);
        PauseMenager.Instance.Pause();
        _winScrene.Open();
    }
}

