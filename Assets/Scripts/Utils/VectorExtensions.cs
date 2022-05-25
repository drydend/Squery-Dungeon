using UnityEngine;

public static class VectorExtensions
{
    public static Vector2 ClampMagnitude(Vector2 vector, float min, float max)
    {
        float sqrMagnitude = vector.sqrMagnitude;
        
        if (sqrMagnitude > max * max)
        {
            return vector.normalized * max;
        }
        else if (sqrMagnitude < min * min)
        {
            return vector.normalized * min;
        }

        return vector;
    }

}

