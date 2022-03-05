using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class ArrayExtensions
{
    public static IEnumerable<Vector2Int> GetIndexesOfAllAdjacentElements<T>(this T[,] array, Vector2Int point)
    {
        Vector2Int deltaPoint = Vector2Int.right;

        for (int dX = 0; dX < 4; dX++)
        {
            bool fitArrayX = point.x + deltaPoint.x >= 0 && point.x + deltaPoint.x < array.GetLength(0);
            bool fitArrayY = point.y + deltaPoint.y >= 0 && point.y + deltaPoint.y < array.GetLength(1);
            bool fitArray = fitArrayX && fitArrayY;
            if (fitArray)
            {
                yield return new Vector2Int(point.x + deltaPoint.x, point.y+ deltaPoint.y);
            }

            //rotates delta point by 90 degrees
            deltaPoint = new Vector2Int(deltaPoint.y, -deltaPoint.x);
        }
    }
}

