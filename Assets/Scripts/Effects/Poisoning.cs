using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Effects/Poisoning", fileName = "Poisoning effect")]
public class Poisoning : Effect
{
    [SerializeField]
    private float _effectDuration;
    [SerializeField]
    private float _damagePerTick;
    [SerializeField]
    private float _numberOfTicks;
    [SerializeField]
    private ParticleSystem _tickParticle;
    [SerializeField]
    private ParticleSystem _lastTickParticle;

    private int _numberOfTickPassed;
    private float _ticksTime;
    private Timer _timer;
    private IDamageable _damageable;
    private IEntity _entity;

    public float DamagePerTick => _damagePerTick;
    public float NumberOfTicks => _numberOfTicks;

    public override event Action<Effect> OnEnded;

    public override bool CanBeAppliedTo(IEntity entity)
    {
        return entity.Damageable != null;
    }

    public override void Initialize(IEntity entity)
    {
        _entity = entity;
        _ticksTime += _effectDuration / _numberOfTicks;
        _damageable = entity.Damageable;
        _timer = new Timer(_effectDuration + _ticksTime);
        _timer.OnFinished += OnEffectEnded;
    }

    public override void Update()
    {
        if (_timer.SecondsPassed > _ticksTime)
        {
            _damageable.ApplyDamage(_damagePerTick);
            _ticksTime += _effectDuration / _numberOfTicks;
            _numberOfTickPassed++;
            
            if(_numberOfTickPassed == _numberOfTicks)
            {   
                OnEffectEnded();
                Instantiate(_lastTickParticle, _entity.Transform.position, Quaternion.identity);
                _timer.Pause();
            }
            else
            {
                Instantiate(_tickParticle, _entity.Transform.position, Quaternion.identity);
            }
        }

        _timer.UpdateTick(Time.deltaTime);
    }

    private void OnEffectEnded()
    {
        OnEnded?.Invoke(this);
    }
}

