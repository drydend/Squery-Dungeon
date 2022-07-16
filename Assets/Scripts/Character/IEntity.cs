using UnityEngine;

public interface IEntity
{   
    Transform Transform { get; }

    public IDamageable Damageable { get; }
    public IHitable Hitable { get; }
    public IMoveable Moveable { get; }
}

