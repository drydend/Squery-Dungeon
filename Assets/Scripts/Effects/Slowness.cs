using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Effects/Slowness", fileName = "Slowness effect")]
public class Slowness : Effect
{
    [SerializeField]
    private ParticleSystem _particlePrefab;
    [SerializeField]
    private float _slownessValue;
    private float _realSlownessValue;

    private ParticleSystem _particle;
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

        _particle = Instantiate(_particlePrefab, entity.Transform);
        _moveable.DecreaseSpeed(_realSlownessValue);
    }

    public override void Update()
    {
        base.Update();
    }

    protected override void OnEffectEnded()
    {
        _particle.Stop();
        Destroy(_particle, 0.3f);
        base.OnEffectEnded();
        _moveable.IncreaseSpeed(_realSlownessValue);
    }
}
