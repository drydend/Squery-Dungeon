using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

