using MarkSFrancis.Collections.Extensions;
using MarkSFrancis.IO.DelimitedData.Maps.Interfaces;
using MarkSFrancis.Reflection;

namespace MarkSFrancis.IO.DelimitedData.Maps.Write
{
    public class WriteMapColumnId<T> : BaseMapColumnId<T>, IWriteMap<T> where T : new()
    {
        public void AddColumnAndMap(PropertyFieldInfo<T, object> propFieldToMap, int mapTo)
        {
            if (MemberToColumn.ContainsValue(mapTo))
            {
                throw ErrorFactory.Default.DuplicateKey(nameof(mapTo));
            }

            MemberToColumn.Add(propFieldToMap, mapTo);
        }

        public string[] MapFromObject(T record)
        {
            string[] values = new string[MemberToColumn.Count];

            foreach (var mappedMember in MemberToColumn)
            {
                values[mappedMember.Value] = mappedMember.Key.GetValue(record).ToString();
            }

            return values;
        }
    }
}
