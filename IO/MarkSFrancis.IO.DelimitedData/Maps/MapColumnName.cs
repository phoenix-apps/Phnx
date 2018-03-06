using System.Collections.Generic;
using System.Linq;
using MarkSFrancis.Collections;
using MarkSFrancis.Collections.Extensions;
using MarkSFrancis.Extensions;
using MarkSFrancis.IO.DelimitedData.Maps.Interfaces;
using MarkSFrancis.Reflection;
using MarkSFrancis.Reflection.Extensions;

namespace MarkSFrancis.IO.DelimitedData.Maps
{
    public class MapColumnName<T> : IMapBy<T, string> where T : new()
    {
        public MapColumnName()
        {
            MemberToColumn = new MultiKeyDictionary<PropertyFieldInfo<T, object>, string>();
        }

        protected readonly MultiKeyDictionary<PropertyFieldInfo<T, object>, string> MemberToColumn;
        public IEnumerable<string> MappedColumnNames => MemberToColumn.Values;

        public static MapColumnName<T> AutoMap(bool mapProperties = true, bool mapFields = false)
        {
            var membersToMap = typeof(T)
                .GetPropertyFieldInfos<T>(mapProperties, mapFields)
                .OrderBy(m => m.Name)
                .ToList();

            var newMap = new MapColumnName<T>();

            foreach(var member in membersToMap)
            {
                var formattedHeading = member.Name.FromCamelAndPascalCase(true);

                newMap.Map(member, formattedHeading);
            }

            return newMap;
        }

        public void Map(PropertyFieldInfo<T, object> propFieldToMap, string mapTo)
        {
            MemberToColumn.Add(propFieldToMap, mapTo);
        }

        public string[] MapFromObject(T record, IList<string> columnHeadings)
        {
            string[] values = new string[MemberToColumn.Count];

            foreach (var member in MemberToColumn)
            {
                var outputIndex = columnHeadings.IndexOf(member.Value);

                values[outputIndex] = member.Key.GetValue(record).ToString();
            }

            return values;
        }

        public T MapToObject(IList<string> values, IList<string> columnHeadings)
        {
            T record = new T();

            foreach (var mappedColumn in MemberToColumn)
            {
                // Map to columns
                var columnId = columnHeadings.IndexOf(mappedColumn.Value);

                if (columnId < 0)
                {
                    // Not found
                    ErrorFactory.Default.KeyNotFound(mappedColumn.Value, nameof(MappedColumnNames));
                }

                var converter = ConverterHelpers.GetDefaultConverter(typeof(string), mappedColumn.Key.Type);
                var convertedValue = converter(values[columnId]);

                mappedColumn.Key.SetValue(record, convertedValue);
            }

            return record;
        }
    }
}
