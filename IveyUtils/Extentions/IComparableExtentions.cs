using System;

namespace Ivey.Utils
{
    public static partial class Extentions
    {

        public static bool Between(this IComparable a, IComparable b, IComparable c, int i = 0)
        {
            return (a.CompareTo(b) >= i && a.CompareTo(c) <= 0);
        }

    }
}