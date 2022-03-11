using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class ShootingState : BaseEnemyState
{
    protected RangeWeapon _rangeWeapon;
    protected Timer _timer;
    protected Character _target => _controlableEnemy.Target;

    public ShootingState(Enemy controlableEnemy, RangeWeapon rangeWeapon, Timer timer)
        :base(controlableEnemy)
    {
        _timer = timer;
        _rangeWeapon = rangeWeapon;
    }

    public override void OnEnter()
    {
        
    }

    public override void OnExit()
    {
        
    }

    public override void Update()
    {
        TryShoot();
    }

    private void TryShoot()
    {
        _controlableEnemy.transform.LookAt2D(_target.transform.position);
        if (_timer.IsFinished)
        {
            _rangeWeapon.Attack(_target.transform.position);
        }
    }
}

