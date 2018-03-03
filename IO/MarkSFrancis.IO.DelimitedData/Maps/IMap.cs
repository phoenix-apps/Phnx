using System.Collections.Generic;

namespace MarkSFrancis.IO.DelimitedData.Maps
{
    public interface IMap<T>
    {
        IEnumerable<string> ColumnHeadings { get; }

        IList<string> MapFromObject(T record);

        T MapToObject(IList<string> values);
    }
}