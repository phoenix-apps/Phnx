using System.Collections.Generic;
using System.Linq;
using MarkSFrancis.Reflection;

namespace MarkSFrancis.IO.DelimitedData.Maps
{
    public class NumberMap<T> : BaseMap<NumberMap<T>, T, int> where T : new()
    {
        public override IEnumerable<string> GetColumnHeadings()
        {
            var mappedColumns = PropertyFieldToKey.OrderBy(cn => cn.Value).ToList();

            int columnIndex = 0;
            foreach (var mappedColumn in mappedColumns)
            {
                while (mappedColumn.Value > columnIndex)
                {
                    yield return "Column " + columnIndex;
                    columnIndex++;
                }

                yield return "Column " + columnIndex;
                columnIndex++;
            }
        }

        public override NumberMap<T> Map(PropertyFieldInfo<T, object> propFieldInfo, int columnId)
        {
            Unmap(columnId);

            PropertyFieldToKey.Add(propFieldInfo.Name, columnId);
            KeyToPropertyField.Add(columnId, propFieldInfo);

            return this;
        }

        public NumberMap<T> Unmap(int columnId)
        {
            if (!KeyToPropertyField.ContainsKey(columnId))
            {
                // Not found
                return this;
            }

            var propFieldInfo = KeyToPropertyField[columnId];

            PropertyFieldToKey.Remove(propFieldInfo.Name);
            KeyToPropertyField.Remove(columnId);

            return this;
        }

        public T MapToObject(IList<string> values)
        {
            T newT = new T();

            for (int columnId = 0; columnId < values.Count; columnId++)
            {
                TryAssignToMember(columnId, values[columnId], newT);
            }

            return newT;
        }

        protected override bool TryAppendSpecificValue(T record, PropertyFieldInfo<T, object> propFieldInfo, IList<string> values)
        {
            if (!TryGetMapping(propFieldInfo, out int columnId))
            {
                return false;
            }

            values[columnId] = GetFromMember(record, propFieldInfo);

            return true;
        }

        protected override bool TryGetMapping(PropertyFieldInfo<T, object> propField, out int columnId)
        {
            return PropertyFieldToKey.TryGetValue(propField.Name, out columnId);
        }

        protected override bool TryGetMapping(int columnId, out PropertyFieldInfo<T, object> propField)
        {
            return KeyToPropertyField.TryGetValue(columnId, out propField);
        }
    }
}