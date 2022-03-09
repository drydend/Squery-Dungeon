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
    public abstract void Attack(Vector3 targetPosition);
    public abstract void UseAbility();
}

