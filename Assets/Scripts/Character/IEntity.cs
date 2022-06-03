public interface IEntity
{   
    public IDamageable Damageable { get; }
    public IHitable Hitable { get; }
    public IMoveable Moveable { get; }
}

