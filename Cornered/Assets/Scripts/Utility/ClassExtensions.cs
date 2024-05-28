using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ClassExtensions
{
    public static T GetRandom<T>(this IEnumerable<T> enumerable)
    {
        if (enumerable.Count() == 0)
        {
            return default(T);
        }

        return enumerable.ElementAt(Random.Range(0, enumerable.Count()));
    }
}
