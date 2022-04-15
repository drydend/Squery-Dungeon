using UnityEngine;

[CreateAssetMenu(menuName = "Character upgrades/Bullet hit behaviour upgrade", fileName = "Upgrade")]
public class BulletHitBehaviourUpgrade : BulletUpgrade
{
    [SerializeField]
    private ParticleSystem _hitParticles;
    [SerializeField]
    private BulletHitBehaviour _bulletHitBehaviour;

    public BulletHitBehaviour NewHitBehaviour => _bulletHitBehaviour;
    public ParticleSystem HitParticles => _hitParticles;
}
