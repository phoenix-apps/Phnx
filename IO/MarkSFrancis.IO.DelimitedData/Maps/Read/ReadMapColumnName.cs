using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MarkSFrancis.Extensions;
using MarkSFrancis.Reflection;
using MarkSFrancis.Reflection.Extensions;

namespace MarkSFrancis.IO.DelimitedData.Maps.Read
{
    public class ReadMapColumnName<T> : BaseMapColumnName<T>, IReadMap<T, string> where T : new()
    {
        public ReadMapColumnName(params string[] columnHeadings) : this((IList<string>)columnHeadings)
        {
        }

        public ReadMapColumnName(IList<string> columnHeadings) : base(columnHeadings)
        {
        }

        public static ReadMapColumnName<T> AutoMap(IList<string> columns, bool mapProperties = true, bool mapFields = false)
        {
            var membersToMap = typeof(T).GetPropertyFieldInfos<T>()
                .Where(pf => (pf.IsProperty && mapProperties) || (!pf.IsProperty && mapFields)).ToList();

            var newMap = new ReadMapColumnName<T>(columns);

            for (int index = 0; index < membersToMap.Count; index++)
            {
                newMap.TryAddMap(membersToMap[index], membersToMap[index].Name.FromCamelAndPascalCase());
            }

            return newMap;
        }
        
        public bool TryAddMap(Expression<Func<T, object>> propFieldToMap, string mapTo)
        {
            var propField = new PropertyFieldInfo<T,object>(propFieldToMap);

            return TryAddMap(propField, mapTo);
        }

        public bool TryAddMap(KeyValuePair<Expression<Func<T, object>>, string> propFieldToMap)
        {
            return TryAddMap(propFieldToMap.Key, propFieldToMap.Value);
        }

        public bool TryAddMap(PropertyFieldInfo<T, object> propFieldToMap, string mapTo)
        {
            if (_memberToColumn.ContainsKey(propFieldToMap))
            {
                return false;
            }

            var colIndex = _intermediateColumnHeadings.IndexOf(ToMapIntermediateValue(mapTo));

            if (colIndex < 0)
            {
                // Not found, not a known column name
                return false;
            }

            _memberToColumn.Add(propFieldToMap, colIndex);
            return true;
        }

        public bool TryAddMap(KeyValuePair<PropertyFieldInfo<T, object>, string> propFieldToMap)
        {
            return TryAddMap(propFieldToMap.Key, propFieldToMap.Value);
        }

        public T MapToObject(IList<string> values)
        {
            T record = new T();

            foreach (var mappedColumn in _memberToColumn)
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
