using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MarkSFrancis.Collections.Extensions;
using MarkSFrancis.Extensions;
using MarkSFrancis.Reflection;
using MarkSFrancis.Reflection.Extensions;

namespace MarkSFrancis.IO.DelimitedData.Maps.Write
{
    public class WriteMapColumnName<T> : BaseMapColumnName<T>, IWriteMap<T, string> where T : new()
    {
        public static WriteMapColumnName<T> AutoMap(bool mapProperties = true, bool mapFields = false)
        {
            var membersToMap = typeof(T).GetPropertyFieldInfos<T>()
                .Where(pf => (pf.IsProperty && mapProperties) || (!pf.IsProperty && mapFields)).OrderBy(m => m.Name).ToList();

            var autoHeadings = AutoHeadings(membersToMap);

            var newMap = new WriteMapColumnName<T>();

            for (int index = 0; index < membersToMap.Count; index++)
            {
                newMap.AddColumnAndMap(membersToMap[index], autoHeadings[index]);
            }

            return newMap;
        }

        private static IList<string> AutoHeadings(IEnumerable<PropertyFieldInfo<T, object>> membersToMap)
        {
            List<string> headings = new List<string>();

            foreach (var member in membersToMap)
            {
                headings.Add(member.Name.FromCamelAndPascalCase(true));
            }

            return headings;
        }

        public void AddColumnAndMap(Expression<Func<T, object>> propFieldToMap, string mapTo)
        {
            var propField = new PropertyFieldInfo<T,object>(propFieldToMap);

            AddColumnAndMap(propField, mapTo);
        }

        public void AddColumnAndMap(KeyValuePair<Expression<Func<T, object>>, string> propFieldToMap)
        {
            AddColumnAndMap(propFieldToMap.Key, propFieldToMap.Value);
        }

        public void AddColumnAndMap(PropertyFieldInfo<T, object> propFieldToMap, string mapTo)
        {
            var colIndex = _intermediateColumnHeadings.IndexOf(ToMapIntermediateValue(mapTo));

            _columnHeadings.Add(mapTo);
            _intermediateColumnHeadings.Add(ToMapIntermediateValue(mapTo));

            _memberToColumn.Add(propFieldToMap, _columnHeadings.Count - 1);
        }

        public void AddColumnAndMap(KeyValuePair<PropertyFieldInfo<T, object>, string> propFieldToMap)
        {
            AddColumnAndMap(propFieldToMap.Key, propFieldToMap.Value);
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
