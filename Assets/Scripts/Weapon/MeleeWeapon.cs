using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    [SerializeField]
    private Character _owner;
    [SerializeField]
    private float _maxAttackRange = 10;
    [SerializeField]
    private float _attackRadius = 5;
    [SerializeField]
    private float _damage;
    [SerializeField]
    private ParticleSystem _attackParticle;
    [SerializeField]
    private LayerMask _whatCanBeDamaged;

    private void Start()
    {
        var attackParticleShape = _attackParticle.shape;
        attackParticleShape.radius = _attackRadius;
    }

    public void Attack()
    {
        Instantiate(_attackParticle, transform.position, Quaternion.identity);
        var colliders = Physics2D.OverlapCircleAll(transform.position, _attackRadius, _whatCanBeDamaged);
        foreach (var collider in colliders)
        {
            collider.GetComponent<IDamageable>()?.RecieveDamage(_damage, gameObject);
        }
    }


}
