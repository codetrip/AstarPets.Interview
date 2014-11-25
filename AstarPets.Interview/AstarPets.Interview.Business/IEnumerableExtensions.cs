using System;
using System.Collections.Generic;
using System.Linq;

namespace AstarPets.Interview.Business
{
    public static class IEnumerableExtensions
    {
        public static int MaxOrDefault<T>(this IEnumerable<T> enumerable, Func<T, int> selectMax)
        {
            if (!enumerable.Any())
                return default(int);

            return enumerable.Max(selectMax);
        }

        public static void RemoveWhere<T>(this IList<T> list, Func<T, bool> filter)
        {
            foreach(var t in list.Where(filter).ToList())
                list.Remove(t);
        }
    }
}