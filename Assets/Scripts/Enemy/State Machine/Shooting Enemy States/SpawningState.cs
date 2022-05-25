public class SpawningState : BaseEnemyState
{
    public SpawningState(Enemy controlableEnemy) : base(controlableEnemy) { }

    public override void OnEnter()
    {
        _controlableEnemy.Spawn();
    }

    public override void OnExit()
    {

    }

    public override void Update()
    {

    }
}

