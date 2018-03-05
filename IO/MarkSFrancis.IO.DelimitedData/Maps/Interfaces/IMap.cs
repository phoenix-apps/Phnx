using System.Collections.Generic;

namespace MarkSFrancis.IO.DelimitedData.Maps.Interfaces
{
    public interface IMap<T> where T : new()
    {
        IEnumerable<string> ColumnHeadings { get; }
    }
}