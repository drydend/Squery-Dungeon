using UnityEngine;

[CreateAssetMenu(menuName = "Bullet collision behaviour/Ricochet collision behaviour", fileName = "RicochetCollisionBehaviour")]
public class RicochetCollisionBehaviour : BulletCollisionBehaviour
{
    [SerializeField]
    protected int _ricochetNumber = 1;
    private Vector2 _previousRicochetNormal;

    public int RicochetNumber => _ricochetNumber;

    public override void HandleCollision(Collision2D collision)
    {
        if (_ricochetNumber == 0)
        {
            _projectile.DestroyProjectile();
            _projectile.PlayCollisionParticle();
        }

        var normal = collision.contacts[0].normal;

        if (_previousRicochetNormal == normal)
        {
            return;
        }

        _ricochetNumber--;

        _previousRicochetNormal = normal;
        var direction = Vector2.Reflect(_projectile.MoveDirection, normal);

        _projectile.ChangeMoveDirection(direction);
        _projectile.PlayCollisionParticle();
    }
}

