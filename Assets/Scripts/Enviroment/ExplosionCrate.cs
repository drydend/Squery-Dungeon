using UnityEngine;

public class ExplosionCrate : Crate
{
    [SerializeField]
    private float _explosionDamage = 1;
    [SerializeField]
    private float _explosionRadious;
    [SerializeField]
    private float _explosionPushForce = 40f;
    [SerializeField]
    private float _explosionPushDuration = 0.1f;
    [SerializeField]
    private LayerMask _wallLayer;

    private void Awake()
    {
        _particleScaleFactor = _explosionRadious;
    }

    protected override void Destruct()
    {
        if (_isDestructed)
        {
            return;
        }

        _isDestructed = true;
            
        var colliders = Physics2D.OverlapCircleAll(transform.position, _explosionRadious);

        foreach (var collider in colliders)
        {
            if (collider.gameObject == gameObject)
                continue;

            var directionToCollider = Vector2.ClampMagnitude(transform.position - collider.transform.position, 1);
            
            if(Physics2D.Raycast(transform.position, directionToCollider, _explosionRadious, _wallLayer) )
            {
                continue;
            }
            
            collider.GetComponent<IHitable>()?
                .RecieveHit(_explosionDamage, gameObject);
            collider.GetComponent<IPushable>()?
                .ApplyForce(directionToCollider,_explosionPushForce, _explosionPushDuration);
        }

        base.Destruct();
    }
}
