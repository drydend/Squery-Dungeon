using UnityEngine;

public class PlayParticleAtStart : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem _particleSystem;

    private void Start()
    {
        _particleSystem.Play();
    }
}

