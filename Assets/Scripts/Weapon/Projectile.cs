using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    private Vector2 _direction;
    private float _speed;
    private float _damage = 1f;
    private GameObject _owner;
    private bool _hitSomething = false;

    [SerializeField]
    private ParticleSystem _hitWallParticle; 
    private Rigidbody2D _rigidbody2D;

    public void Initialize(Vector2 direction, float speed, float damage, GameObject owner)
    {
        _owner = owner;
        _direction = direction;
        _speed = speed;
        _damage = damage;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        LookInDirection();
        Move();
    }
    
    private void Move()
    {
        _rigidbody2D.AddForce( (Vector3)_direction * _speed, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.isTrigger || _hitSomething)
        {
            return;
        }

        if (_owner != collision.gameObject)
        {   
            _hitSomething = true;
            if (collision.gameObject.TryGetComponent(out IDamageable damageable))
            {
                damageable.RecieveDamage(_damage, gameObject);
                Destroy(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    private void LookInDirection()
    {
        float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg - 180 ;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

}
