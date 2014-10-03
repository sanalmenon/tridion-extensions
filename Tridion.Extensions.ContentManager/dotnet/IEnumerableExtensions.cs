using System;
using System.Collections.Generic;

namespace Tridion.Templating.Extensions
{
    public static class IEnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
        {
            foreach (T item in enumeration)
            {
                action(item);
            }
        }
        
        public static string AsString(this IEnumerable<string> source, string separator = "\n")
        {
            return string.Join(separator, source);
        }
    }
}
