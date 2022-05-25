using UnityEngine;

[CreateAssetMenu(menuName = "Character upgrades/Bullet collision behaviour upgrade", fileName = "Upgrade")]
public class BulletCollisionBehaviourUpgrade : BulletUpgrade
{
    [SerializeField]
    private BulletCollisionBehaviour _collisionBehaviour;

    public BulletCollisionBehaviour NewCollisionBehaviour => _collisionBehaviour;
}

