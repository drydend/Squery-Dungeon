using UnityEngine;

[CreateAssetMenu(menuName = "Bullet collision behaviour/Ricochet collision behaviour", fileName = "RicochetCollisionBehaviour")]
public class RicochetCollisionBehaviour : BulletCollisionBehaviour
{
    [SerializeField]
    protected int _ricochetNumber = 1;
    [SerializeField]
    private LayerMask _raycastLayers;

    private Vector2 _previousRicochetNormal;

    public int RicochetNumber => _ricochetNumber;

    public override void HandleCollision(Collider2D collider)
    {
        if (_ricochetNumber == 0)
        {
            _projectile.DestroyProjectile();
            _projectile.PlayCollisionParticle();
        }

        var raycastHit = Physics2D.RaycastAll(_projectile.transform.position, 
            _projectile.MoveDirection, 
            1f, _raycastLayers);
        
        var normal = raycastHit[0].normal;

        if (_previousRicochetNormal == normal)
        {
            Debug.Log("Double collision");
            return;
        }

        _ricochetNumber--;

        _previousRicochetNormal = normal;
        var direction = Vector2.Reflect(_projectile.MoveDirection, normal);

        _projectile.ChangeMoveDirection(direction);
        _projectile.PlayCollisionParticle();
    }
}

