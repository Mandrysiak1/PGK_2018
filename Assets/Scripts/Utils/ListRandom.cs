using System;
using System.Collections.Generic;

public static class ListRandom
{
    private static Random _Random = new Random();

    public static T Random<T>(this IList<T> list)
    {
        return Random(list, _Random);
    }

    public static T Random<T>(this IList<T> list, Random random)
    {
        if (random == null)
            random = _Random;

        int count = list.Count;
        if(count == 0)
            throw new ArgumentException("List is empty");

        int idx = random.Next(count);
        return list[idx];
    }
}
