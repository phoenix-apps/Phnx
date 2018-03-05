using MarkSFrancis.Collections.Extensions;
using MarkSFrancis.Reflection;

namespace MarkSFrancis.IO.DelimitedData.Maps.Write
{
    public class WriteMapColumnId<T> : BaseMapColumnId<T>, IWriteMap<T, int> where T : new()
    {
        public void AddColumnAndMap(PropertyFieldInfo<T, object> propFieldToMap, int mapTo)
        {
            if (_memberToColumn.ContainsValue(mapTo))
            {
                throw ErrorFactory.Default.DuplicateKey(nameof(mapTo));
            }

            _memberToColumn.Add(propFieldToMap, mapTo);
        }

        public string[] MapFromObject(T record)
        {
            string[] values = new string[_memberToColumn.Count];

            foreach (var mappedMember in _memberToColumn)
            {
                values[mappedMember.Value] = mappedMember.Key.GetValue(record).ToString();
            }

            return values;
        }
    }
}
