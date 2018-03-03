using MarkSFrancis.Extensions;
using System.Collections.Generic;
using System.Linq;
using MarkSFrancis.Reflection;
using MarkSFrancis.Reflection.Extensions;

namespace MarkSFrancis.IO.DelimitedData.Maps
{
    class NameMap2<T> : IMap<T>
    {
        public NameMap2(IList<PropertyFieldInfo<T, object>> membersToMap)
        {
            _columnHeadings = RenderHeadings(membersToMap);

            CreateMapDictionaries(membersToMap, out _memberToColumn);
        }

        public NameMap2(IList<string> columnHeadings)
        {
            _columnHeadings = columnHeadings;

            var allMembers = typeof(T).GetPropertyFieldInfos<T>().Where(pf => pf.IsProperty);
            CreateMapDictionaries(allMembers, out _memberToColumn);
        }

        public IEnumerable<string> ColumnHeadings => _columnHeadings;
        private IList<string> _columnHeadings { get; }

        private readonly Dictionary<PropertyFieldInfo<T, object>, int> _memberToColumn;

        private bool SpaceSensitiveMap = false;
        private bool CaseSensitiveMap = false;

        private static IList<string> RenderHeadings(IEnumerable<PropertyFieldInfo<T, object>> membersToMap)
        {
            List<string> headings = new List<string>();

            foreach (var member in membersToMap.OrderBy(m => m.Name))
            {
                headings.Add(member.Name.FromCamelAndPascalCase(true));
            }

            return headings;
        }

        private void CreateMapDictionaries(IEnumerable<PropertyFieldInfo<T, object>> membersToMap, out Dictionary<PropertyFieldInfo<T, object>, int> memberToColumn)
        {
            memberToColumn = new Dictionary<PropertyFieldInfo<T, object>, int>();
            
            var columnsAsIntermediate = _columnHeadings.Select(ToMapIntermediateValue).ToList();

            foreach (var member in membersToMap)
            {
                var memberNameIntermediate = ToMapIntermediateValue(member.Name);
                var columnIndex = columnsAsIntermediate.IndexOf(memberNameIntermediate);

                if (columnIndex < 0)
                {
                    // Not found - skip
                    continue;
                }

                memberToColumn.Add(member, columnIndex);
            }
        }

        private string ToMapIntermediateValue(string name)
        {
            if (!SpaceSensitiveMap)
            {
                name = name.Remove(" ");
            }

            if (!CaseSensitiveMap)
            {
                name = name.ToUpperInvariant();
            }

            return name;
        }

        public IList<string> MapFromObject(T record)
        {
            string[] values = new string[_memberToColumn.Count];

            foreach (var mappedMember in _memberToColumn)
            {
                values[mappedMember.Value] = mappedMember.Key.GetValue(record).ToString();
            }

            return values;
        }

        public T MapToObject(IList<string> values)
        {
            T record = default(T);

            foreach (var mappedColumn in _memberToColumn)
            {
                var converter = ConverterHelpers.GetDefaultConverter(typeof(string), mappedColumn.Key.Type);
                var convertedValue = converter(values[mappedColumn.Value]);

                mappedColumn.Key.SetValue(record, convertedValue);
            }

            return record;
        }
    }
}
