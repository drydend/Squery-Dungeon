using UnityEngine;

public interface IHitable
{
    Transform Transform { get; }

    void RecieveHit(float damage, GameObject sender);
}

