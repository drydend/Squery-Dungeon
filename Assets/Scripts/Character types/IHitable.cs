using UnityEngine;

interface IHitable
{
    void RecieveHit(float damage, GameObject sender);
}

