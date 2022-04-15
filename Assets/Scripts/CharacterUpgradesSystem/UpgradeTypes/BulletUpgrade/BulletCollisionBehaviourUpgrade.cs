using UnityEngine;

[CreateAssetMenu(menuName = "Character upgrades/Bullet collision behaviour upgrade", fileName = "Upgrade")]
public class BulletCollisionBehaviourUpgrade : BulletUpgrade
{
    [SerializeField]
    private BulletCollisionBehaviour _collisionBehaviour;
    [SerializeField]
    private ParticleSystem _collisionParticle;

    public ParticleSystem CollisionParticle => _collisionParticle;
    public BulletCollisionBehaviour NewCollisionBehaviour => _collisionBehaviour;
}

