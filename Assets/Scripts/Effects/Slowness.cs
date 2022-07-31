using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Effects/Slowness", fileName = "Slowness effect")]
public class Slowness : Effect
{
    [SerializeField]
    private ParticleSystem _particleSystem;
    [SerializeField]
    private float _slownessValue;
    private float _realSlownessValue;

    private IMoveable _moveable;

    public override bool CanBeAppliedTo(IEntity entity)
    {
        return entity.Moveable != null;
    }

    public override void Initialize(IEntity entity)
    {
        base.Initialize(entity);
        _moveable = entity.Moveable;

        var difference = _moveable.MovementSpeed - _slownessValue;

        _realSlownessValue = difference < 0 ? -difference : _moveable.MovementSpeed - _slownessValue;

        if(_realSlownessValue == 0)
        {
            _realSlownessValue += 1;
        }

        _moveable.DecreaseSpeed(_realSlownessValue);
    }

    public override void Update()
    {
        base.Update();
    }

    protected override void OnEffectEnded()
    {
        base.OnEffectEnded();
        _moveable.IncreaseSpeed(_realSlownessValue);
    }
}
