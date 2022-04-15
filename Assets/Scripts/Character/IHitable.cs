using UnityEngine;

public interface IHitable
{
    void RecieveHit(float damage, GameObject sender);
}

