using System.Collections;
using UnityEngine;

public class ExplosionCrate : Crate
{
    [SerializeField]
    private float _destructionDelay = 0.2f;
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

    private Coroutine _destructionCoroutine;

    private float ExplosionCameraShakeStrenght => _explosionDamage / 10;

    private void Awake()
    {
        _particleScaleFactor = _explosionRadious;
    }

    private void DestructWithDelay()
    {
        if(_destructionCoroutine == null)
        {
            _destructionCoroutine = StartCoroutine(DestructionCoroutine());
        }
        else
        {
            return;
        }
    }

    private IEnumerator DestructionCoroutine()
    {
        yield return new WaitForSeconds(_destructionDelay);
        Destruct();
    }

    private void Explode()
    {
        var colliders = Physics2D.OverlapCircleAll(transform.position, _explosionRadious);
        CameraShaker.Instance.ShakeCamera(0.3f, ExplosionCameraShakeStrenght);

        foreach (var collider in colliders)
        {
            if (collider.gameObject == gameObject)
                continue;

            var directionToCollider = Vector2.ClampMagnitude(transform.position - collider.transform.position, 1);

            if (Physics2D.Raycast(transform.position, directionToCollider, _explosionRadious, _wallLayer))
            {
                continue;
            }

            if (collider.TryGetComponent(out ExplosionCrate explosionCrate))
            {
                explosionCrate.DestructWithDelay();
                continue;
            }

            collider.GetComponent<IHitable>()?
                .RecieveHit(_explosionDamage, gameObject);
            collider.GetComponent<IPushable>()?
                .ApplyForce(directionToCollider, _explosionPushForce, _explosionPushDuration);
        }

        base.Destruct();
    }

    protected override void Destruct()
    {
        if (_isDestructed)
        {
            return;
        }

        _isDestructed = true;

        Explode();
    }
}
