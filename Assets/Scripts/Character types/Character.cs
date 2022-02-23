using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Character : MonoBehaviour
{
    [SerializeField]
    protected float _movementSpeed;
    protected Rigidbody2D _rigidbody2D;

    public float Speed => _movementSpeed;
    public CharacterType CharacterType { get; protected set; }

    public virtual void LookAt(Vector3 position) 
    {
        Vector3 diff = transform.position - position;
        diff.Normalize();

        float angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x , transform.rotation.y, angle));

    }

    public virtual void Move(Vector2 direction)
    {   
        _rigidbody2D.MovePosition(transform.position + (Vector3)direction * _movementSpeed * Time.fixedDeltaTime);
    }
    public virtual void OnCharacterEnable()
    {
        gameObject.SetActive(true);
    }

    public virtual void OnCharacterDisable()
    {
        gameObject.SetActive(false);
    }
    public virtual void Initialize() 
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public abstract void IncreaseStat(StatType statType, float value);
    public abstract void DecreaseStat(StatType statType, float value);
    public abstract void Attack();
    public abstract void Attack(Transform target);
    public abstract void UseAbility();
}

