using MarkSFrancis.Extensions;
using System.Collections.Generic;
using System.Linq;
using MarkSFrancis.Collections;
using MarkSFrancis.Reflection;

namespace MarkSFrancis.IO.DelimitedData.Maps
{
    public abstract class BaseMapColumnName<T> : IMap<T, string> where T : new()
    {
        protected BaseMapColumnName()
        {
            _columnHeadings = new List<string>();
            _intermediateColumnHeadings = new List<string>();
            _memberToColumn = new MultiKeyDictionary<PropertyFieldInfo<T, object>, int>();
        }

        protected BaseMapColumnName(IList<string> columnHeadings)
        {
            _columnHeadings = columnHeadings;
            _intermediateColumnHeadings = _columnHeadings.Select(ToMapIntermediateValue).ToList();
            _memberToColumn = new MultiKeyDictionary<PropertyFieldInfo<T, object>, int>();
        }

        public IEnumerable<string> ColumnHeadings => _columnHeadings;
        protected IList<string> _columnHeadings { get; }
        protected IList<string> _intermediateColumnHeadings { get; }

        protected readonly MultiKeyDictionary<PropertyFieldInfo<T, object>, int> _memberToColumn;

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
