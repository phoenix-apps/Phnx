using System.Collections.Generic;
using MarkSFrancis.Reflection;

namespace MarkSFrancis.IO.DelimitedData.Maps.Read
{
    public class ReadMapColumnId<T> : BaseMapColumnId<T>, IReadMap<T, int> where T: new()
    {
        public bool TryAddMap(PropertyFieldInfo<T, object> propFieldToMap, int mapTo)
        {
            _memberToColumn.Add(propFieldToMap, mapTo);
            return true;
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
