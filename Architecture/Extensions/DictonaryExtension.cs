using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Architecture
{
    public static class DictonaryExtension
    {
        public static void AddIfNotExistOrUpdateIfExist<TKey, TValue>(this Dictionary<TKey, TValue> dic, TKey key, TValue value)
        {
            if (dic.ContainsKey(key))
            {
                dic[key] = value;
            }
            else
            {
                dic.Add(key, value);
            }
        }

        public static int FindIndex<T>(this IEnumerable<T> items, Func<T, bool> predicate)
        {
            var itemsWithIndices = items.Select((item, index) => new { Item = item, Index = index });
            var matchingIndices =
                from itemWithIndex in itemsWithIndices
                where predicate(itemWithIndex.Item)
                select (int?)itemWithIndex.Index;

            return matchingIndices.FirstOrDefault() ?? -1;
        }
    }
}
