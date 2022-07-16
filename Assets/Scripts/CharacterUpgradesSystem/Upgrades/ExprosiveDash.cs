using UnityEngine;

[CreateAssetMenu(menuName = "Character upgrades/Special/Explosive dash", fileName = "Explosive dash")]
public class ExprosiveDash : Upgrade
{   
    [SerializeField]
    private float _explosionDamage;
    [SerializeField]
    private float _explosionRadious;
    [SerializeField]
    private float _explosionPushForce;
    [SerializeField]
    private float _pushDuration;
    [SerializeField]
    private ScalableParticles _explosionParticle;
    [SerializeField]
    private LayerMask _wallLayer;
    private Player _player;

    public override void ApplyUpgrade(Player player)
    {
        _player = player;
        player.OnCharacterEndedDashing += Explode;
    }

    public override void RevertUpgrade(Player player)
    {
        player.OnCharacterEndedDashing -= Explode;
        _player = null;
    }

    private void Explode()
    {
        var explosionParticle = Instantiate(_explosionParticle, _player.CurrentCharacter.transform.position, Quaternion.identity);
        explosionParticle.Scale(_explosionRadious);

        var colliders = Physics2D.OverlapCircleAll(_player.CurrentCharacter.transform.position, _explosionRadious);
        CameraShaker.Instance.ShakeCamera(0.3f, 0.2f);

        foreach (var collider in colliders)
        {
            if (collider.gameObject == _player.CurrentCharacter.gameObject)
                continue;

            var directionToCollider = Vector2.ClampMagnitude(collider.transform.position -
                _player.CurrentCharacter.transform.position, 1);

            if (Physics2D.Raycast(_player.CurrentCharacter.transform.position, directionToCollider, _explosionRadious, _wallLayer))
            {
                continue;
            }

            if(collider.TryGetComponent(out Projectile projectile))
            {
                Destroy(projectile.gameObject);
            }

            collider.GetComponent<IHitable>()?
                .RecieveHit(_explosionDamage, _player.CurrentCharacter.gameObject);
            collider.GetComponent<IPushable>()?
                .ApplyForce(directionToCollider, _explosionPushForce, 0.3f);
        }

    }
}

