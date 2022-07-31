using UnityEngine;

[CreateAssetMenu(menuName = "EnemyDuingBehaviour/Poisoning Explosion", fileName = "Poisoning explosion")]
public class EnemyPosoningExplosion : EnemyDyingBehaviour
{
    [SerializeField]
    private ParticleSystem _explosionParticle;
    [SerializeField]
    private float _explosionRadious;
    [SerializeField]
    private LayerMask _layerMask;

    protected override void OnEnemyDied()
    {
        Instantiate(_explosionParticle, _enemy.transform.position, Quaternion.identity);

        if (_enemy.TryGetEffect(out Poisoning effect))
        {
            var colliders = Physics2D.OverlapCircleAll(_enemy.transform.position, _explosionRadious, _layerMask);

            foreach (var collider in colliders)
            {
                if (collider.gameObject == _enemy.gameObject)
                {
                    continue;
                }

                if (collider.gameObject.TryGetComponent(out Enemy enemy))
                {
                    if (enemy.CanApplyEffect(effect))
                    {
                        enemy.ApplyEffect(effect);
                    }
                }

            }
        }

    }

}
