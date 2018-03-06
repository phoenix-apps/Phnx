using System.Collections.Generic;

namespace MarkSFrancis.IO.DelimitedData.Maps.Interfaces
{
    public interface IMap<T> where T : new()
    {
        IEnumerable<string> MappedColumnNames { get; }

        T MapToObject(IList<string> values, IList<string> columnHeadings);

        string[] MapFromObject(T record, IList<string> columnHeadings);
    }
}