using System.Collections.Generic;
using UnityEngine;

public static class RandomUtils
{   
    public static bool RandomBoolean(float percentForTrue = 50f)
    {
        if (percentForTrue >= 100)
            return true;

        return Random.Range(0, 100) <= percentForTrue ? true : false;
    }

    public static Vector2Int GetRandomAxisDirection()
    {
        var isHorizontal = RandomBoolean();
        if (isHorizontal)
        {
            return new Vector2Int(GetOneRandomValue(-1, 1), 0 );
        }
        else
        {
            return new Vector2Int( 0, GetOneRandomValue(-1, 1));
        }

    }

    public static T GetOneRandomValue<T>(params T[] values)
    {
        if (values.Length == 1)
            return values[0];

        return values[Random.Range(0, values.Length)];
    }

    public static void ShuffleList<T>( IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
