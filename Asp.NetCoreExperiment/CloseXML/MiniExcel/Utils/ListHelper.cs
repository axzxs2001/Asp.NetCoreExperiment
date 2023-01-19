﻿namespace MiniExcelLibs.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class IEnumerableHelper
    {
        public static bool StartsWith<T>(this IList<T> span, IList<T> value) where T : IEquatable<T>
        {
            if (value.Count() == 0)
                return true;

            var b = span.Take(value.Count());
            if (b.Count() != value.Count())
                return false;

            for (int i = 0; i < b.Count(); i++)
                if (!span[i].Equals(value[i]))
                    return false;

            return true;
        }
    }
}
