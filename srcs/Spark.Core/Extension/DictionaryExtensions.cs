﻿using System.Collections.Generic;

namespace Spark.Core.Extension
{
    public static class DictionaryExtensions
    {
        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue = default) =>
            dictionary.TryGetValue(key, out TValue value) ? value : defaultValue;
    }
}