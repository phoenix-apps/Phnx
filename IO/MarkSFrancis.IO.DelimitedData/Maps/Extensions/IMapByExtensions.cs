using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MarkSFrancis.IO.DelimitedData.Maps.Interfaces;
using MarkSFrancis.Reflection;

namespace MarkSFrancis.IO.DelimitedData.Maps.Extensions
{
    public static class IMapByExtensions
    {
        public static void Map<T, TMapBy>(this IMapBy<T, TMapBy> map, Expression<Func<T, object>> propFieldToMap, TMapBy mapTo) where T : new()
        {
            var propField = new PropertyFieldInfo<T, object>(propFieldToMap);
            map.Map(propField, mapTo);
        }

        public static void Map<T, TMapBy>(this IMapBy<T, TMapBy> map, KeyValuePair<Expression<Func<T, object>>, TMapBy> mapToAdd) where T : new()
        {
            Map(map, mapToAdd.Key, mapToAdd.Value);
        }

        public static void Map<T, TMapBy>(this IMapBy<T, TMapBy> map, KeyValuePair<PropertyFieldInfo<T, object>, TMapBy> mapToAdd) where T : new()
        {
            map.Map(mapToAdd.Key, mapToAdd.Value);
        }
    }
}
