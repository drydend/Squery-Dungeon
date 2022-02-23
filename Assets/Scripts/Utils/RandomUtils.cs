using UnityEngine;

public static class RandomUtils
{   
    public static bool RandomBoolean(float percentForTrue = 50f)
    {
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


}
