using UnityEngine;

public class HomingProjectile : Projectile
{
    [SerializeField]
    private float _rotationSpeed;
    [SerializeField]
    private float _lifeTime;
    private Timer _timer;

    private void Awake()
    {
        _timer = new Timer(_lifeTime);
        _timer.OnFinished += DestroyProjectile;
    }

    private void Update()
    {
        _timer.UpdateTick(Time.deltaTime);

        var directionToTarget = _target.position - transform.position;
        directionToTarget.Normalize();

        var crossProduct = Vector3.Cross(directionToTarget, MoveDirection).z;
        var rotationAngle = _rotationSpeed * Time.deltaTime * -crossProduct;

        var direction = Quaternion.AngleAxis(rotationAngle, Vector3.forward) * MoveDirection;

        ChangeMoveDirection(direction);
    }

    private new void OnTriggerEnter2D(Collider2D collider)
    {
        base.OnTriggerEnter2D(collider);
    }
}

