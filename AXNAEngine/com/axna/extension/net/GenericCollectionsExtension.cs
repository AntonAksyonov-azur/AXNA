using System;
using System.Collections.Generic;

namespace AXNAEngine.com.axna.extension.net
{
    public static class GenericCollectionsExtension
    {
        private static readonly Random Rnd = new System.Random();

        public static TValue GetRandomItem<TValue>(this TValue[] array)
        {
            return array[Rnd.Next(0, array.Length - 1)];
        }

        #region List

        public static TValue GetRandomItem<TValue>(this List<TValue> list)
        {
            return list[Rnd.Next(0, list.Count - 1)];
        }

        public static TValue RemoveRandomItem<TValue>(this List<TValue> list)
        {
            var value = list[Rnd.Next(0, list.Count - 1)];
            list.Remove(value);

            return value;
        }

        #endregion
    }
}