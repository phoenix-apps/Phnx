using System.Collections.Generic;
using System.Linq;
using MarkSFrancis.Collections;
using MarkSFrancis.IO.DelimitedData.Maps.Interfaces;
using MarkSFrancis.Reflection;
using MarkSFrancis.Reflection.Extensions;

namespace MarkSFrancis.IO.DelimitedData.Maps
{
    public class MapColumnId<T> : IMapBy<T, int> where T : new()
    {
        public MapColumnId()
        {
            MemberToColumn = new MultiKeyDictionary<PropertyFieldInfo<T, object>, int>();
        }

        public static MapColumnId<T> AutoMap(bool mapProperties = true, bool mapFields = false)
        {
            var membersToMap = typeof(T)
                .GetPropertyFieldInfos<T>(mapProperties, mapFields)
                .OrderBy(m => m.Name)
                .ToList();

            var newMap = new MapColumnId<T>();

            for (int index = 0; index < membersToMap.Count; index++)
            {
                var member = membersToMap[index];

                newMap.Map(member, index);
            }

            return newMap;
        }

        protected readonly MultiKeyDictionary<PropertyFieldInfo<T, object>, int> MemberToColumn;

        public IEnumerable<string> MappedColumnNames
        {
            get
            {
                var totalColumns = MemberToColumn.Max(m => m.Value);

                for (int index = 0; index < totalColumns; index++)
                {
                    yield return "Column " + index;
                }
            }
        }

        public void Map(PropertyFieldInfo<T, object> propFieldToMap, int mapTo)
        {
            MemberToColumn.Add(propFieldToMap, mapTo);
        }

        public T MapToObject(IList<string> values, IList<string> columnHeadings)
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

        public string[] MapFromObject(T record, IList<string> columnHeadings)
        {
            string[] values = new string[MemberToColumn.Count];

            foreach (var member in MemberToColumn)
            {
                values[member.Value] = member.Key.GetValue(record).ToString();
            }

            return values;
        }
    }
}
