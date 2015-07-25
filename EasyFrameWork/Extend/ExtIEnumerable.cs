using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Easy.Extend
{
    [DebuggerStepThrough]
    public static class ExtIEnumerable
    {
        public static void Each<T>(this IEnumerable<T> source, Action<T> fun)
        {
            foreach (T item in source)
            {
                fun(item);
            }
        }
        public static List<TResult> ToList<T, TResult>(this IEnumerable<T> source, Func<T, TResult> fun)
        {
            List<TResult> result = new List<TResult>();
            source.Each(m => result.Add(fun(m)));
            return result;
        }
        
    }
}
