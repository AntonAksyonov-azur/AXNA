using System;
using System.Collections.Generic;
using System.Linq;

namespace AXNAEngine.com.axna.extension.net
{
    public static class DictionaryExtension
    {
        private static readonly Random Rnd = new System.Random();

        public static TValue GetRandomItem<TKey, TValue>(this Dictionary<TKey, TValue> dictionary)
        {
            TValue[] flatten = dictionary.Select(x => x.Value).ToArray();
            return flatten[Rnd.Next(0, flatten.Length)];
        }

        //
        public static TKey GetRandomKey<TKey, TValue>(this Dictionary<TKey, TValue> dictionary)
        {
            TKey[] flatten = dictionary.Keys.ToArray();
            return flatten[Rnd.Next(0, dictionary.Keys.Count)];
        }
    }
}