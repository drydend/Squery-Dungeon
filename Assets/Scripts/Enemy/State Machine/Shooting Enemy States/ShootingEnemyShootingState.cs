public class ShootingEnemyShootingState : BaseEnemyState
{
    protected Timer _timer;
    protected Character _target => _controlableEnemy.Target;

    public ShootingEnemyShootingState(Enemy controlableEnemy, Timer timer)
        : base(controlableEnemy)
    {
        _timer = timer;
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
            _controlableEnemy.Attack();
        }
    }
}

