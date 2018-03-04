using System.Collections.Generic;
using System.Linq;
using MarkSFrancis.Collections;
using MarkSFrancis.Reflection;

namespace MarkSFrancis.IO.DelimitedData.Maps
{
    public abstract class BaseMapColumnId<T> : IMap<T, int> where T : new()
    {
        protected BaseMapColumnId()
        {
            _memberToColumn = new MultiKeyDictionary<PropertyFieldInfo<T, object>, int>();
        }

        public IEnumerable<string> ColumnHeadings
        {
            get
            {
                var totalColumns = _memberToColumn.Max(m => m.Value);

                for (int index = 0; index < totalColumns; index++)
                {
                    yield return "Column " + index;
                }
            }
        }

        protected readonly MultiKeyDictionary<PropertyFieldInfo<T, object>, int> _memberToColumn;
    }
}
