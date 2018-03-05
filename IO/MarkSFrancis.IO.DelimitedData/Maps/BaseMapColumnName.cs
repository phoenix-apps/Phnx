using MarkSFrancis.Extensions;
using System.Collections.Generic;
using System.Linq;
using MarkSFrancis.Collections;
using MarkSFrancis.IO.DelimitedData.Maps.Interfaces;
using MarkSFrancis.Reflection;

namespace MarkSFrancis.IO.DelimitedData.Maps
{
    public abstract class BaseMapColumnName<T> : IMap<T> where T : new()
    {
        protected BaseMapColumnName()
        {
            _columnHeadings = new List<string>();
            IntermediateColumnHeadings = new List<string>();
            MemberToColumn = new MultiKeyDictionary<PropertyFieldInfo<T, object>, int>();
        }

        protected BaseMapColumnName(IList<string> columnHeadings)
        {
            _columnHeadings = columnHeadings;
            IntermediateColumnHeadings = _columnHeadings.Select(ToMapIntermediateValue).ToList();
            MemberToColumn = new MultiKeyDictionary<PropertyFieldInfo<T, object>, int>();
        }

        public IEnumerable<string> ColumnHeadings => _columnHeadings;
        protected readonly IList<string> _columnHeadings;
        protected readonly IList<string> IntermediateColumnHeadings;

        protected readonly MultiKeyDictionary<PropertyFieldInfo<T, object>, int> MemberToColumn;

        private bool SpaceSensitiveMap = false;
        private bool CaseSensitiveMap = false;

        protected string ToMapIntermediateValue(string name)
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
    }
}
