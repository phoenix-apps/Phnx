using System;
using System.Collections.Generic;
using System.Linq;

namespace MarkSFrancis.Extensions
{
    public static class ObjectExtensions
    {
        public static T CastTo<T>(this object o)
        {
            return (T)o;
        }

        public static T As<T>(this object o)
        {
            return o.Is<T>() || o is T ? (T)o : default(T);
        }

        public static bool Is<T>(this object o)
        {
            return o is T;
        }

        public static bool Is(this object o, Type t)
        {
            Type oType = o.GetType();
            if (oType == t)
            {
                return true;
            }

            return oType.GenericTypeArguments.Contains(t);
        }

        public static T Chain<T>(this T t, Action<T> action)
        {
            action(t);
            return t;
        }

        /// <summary>
        /// Uses the default comparer to determine whether two objects are identical
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="me"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        public static bool IsEqual<T>(this T me, T other)
        {
            var comparer = EqualityComparer<T>.Default;

            return comparer.Equals(me, other);
        }

        public static IEnumerable<T> SingleToEnumerable<T>(this T single)
        {
            yield return single;
        }
    }
}