using UnityEngine;

interface IPushable
{
    void ApplyForce(Vector2 direction, float force, float duration);
}
