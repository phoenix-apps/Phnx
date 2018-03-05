using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MarkSFrancis.IO.DelimitedData.Maps.Interfaces;
using MarkSFrancis.Reflection;

namespace MarkSFrancis.IO.DelimitedData.Maps.Read
{
    public class ReadMapColumnId<T> : BaseMapColumnId<T>, IReadMap<T> where T: new()
    {
        public bool TryAddMap(PropertyFieldInfo<T, object> propFieldToMap, int mapTo)
        {
            MemberToColumn.Add(propFieldToMap, mapTo);
            return true;
        }

        public bool TryAddMap(Expression<Func<T, object>> propFieldToMap, int mapTo)
        {
            var propField = new PropertyFieldInfo<T, object>(propFieldToMap);

            return TryAddMap(propField, mapTo);
        }

        public bool TryAddMap(KeyValuePair<Expression<Func<T, object>>, int> propFieldToMap)
        {
            return TryAddMap(propFieldToMap.Key, propFieldToMap.Value);
        }

        public bool TryAddMap(KeyValuePair<PropertyFieldInfo<T, object>, int> propFieldToMap)
        {
            return TryAddMap(propFieldToMap.Key, propFieldToMap.Value);
        }

        public T MapToObject(IList<string> values)
        {
            T record = new T();

            foreach (var mappedColumn in MemberToColumn)
            {
                if (values.Count <= mappedColumn.Value)
                {
                    // Skip this column as it's missing from this field
                    continue;
                }

                var converter = ConverterHelpers.GetDefaultConverter(typeof(string), mappedColumn.Key.Type);
                var convertedValue = converter(values[mappedColumn.Value]);

                mappedColumn.Key.SetValue(record, convertedValue);
            }

            return record;
        }
    }
}
