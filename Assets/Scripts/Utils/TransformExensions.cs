using UnityEngine;

public static class TransformExensions
{
    public static void LookAt2D(this Transform transform, Vector3 target)
    {
        Vector3 diff = transform.position - target;
        diff.Normalize();

        float angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y, angle));
    }

}

