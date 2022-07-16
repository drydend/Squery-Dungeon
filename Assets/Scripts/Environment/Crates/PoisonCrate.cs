using UnityEngine;

public class PoisonCrate : Crate
{
    [SerializeField]
    private Effect _poisonEffect;
    [SerializeField]
    private float _poisoningRadius;

    private new void Awake()
    {
        base.Awake();
    }

    protected override void Destruct()
    {
        var colliders = Physics2D.OverlapCircleAll(transform.position, _poisoningRadius);

        foreach (var collider in colliders)
        {
            if (collider.gameObject.TryGetComponent(out IEffectable effectable))
            {
                if (effectable.CanApplyEffect(_poisonEffect))
                {
                    effectable.ApplyEffect(_poisonEffect);
                }
            }
        }

        base.Destruct();
    }
}

