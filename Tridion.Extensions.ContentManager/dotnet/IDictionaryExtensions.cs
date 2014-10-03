namespace System.Collections.Generic
{
    public static class ObjectExtension
    {
        public static OUTPUT IfContainsKey<TKey, TValue, OUTPUT>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TValue, OUTPUT> getResult)
        {
            return
                dictionary.ContainsKey(key)
                    ? getResult(dictionary[key])
                    : default(OUTPUT); 
        }
    }
}