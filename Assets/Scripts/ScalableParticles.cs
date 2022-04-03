using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalableParticles : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem _explosionFragments;
    [SerializeField]
    private List<ParticleSystem> _particleSystems;

    public void Scale(float multiplier)
    {
        var shape = _explosionFragments.shape;
        shape.radius *= multiplier;

        foreach (var item in _particleSystems)
        {
            var particleMain = item.main;
            particleMain.startSizeMultiplier = multiplier;
        }
    }
}
