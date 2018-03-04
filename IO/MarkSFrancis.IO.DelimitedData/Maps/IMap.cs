using System.Collections.Generic;

namespace MarkSFrancis.IO.DelimitedData.Maps
{
    public interface IMap<T, TMapBy> where T : new()
    {
        IEnumerable<string> ColumnHeadings { get; }
    }
}