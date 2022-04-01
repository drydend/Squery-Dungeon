using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public static class ListExtensions
{
    public static T GetRandomValue<T>(this List<T> list)
    {
        if(list.Count == 0)
        {
            throw new Exception("Can`t get value from list with no elements");
        }

        return list[UnityEngine.Random.Range(0, list.Count)];

    }

}

