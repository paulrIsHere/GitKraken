using System.Collections.Generic;

namespace Ivey.Utils
{
    public static partial class Extentions
    {

        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> source)
        {
            return new HashSet<T>(source);
        }
    }
}