using System.Collections.Generic;
using MarkSFrancis.Reflection;

namespace MarkSFrancis.IO.DelimitedData.Maps.Interfaces
{
    public interface IReadMap<T> : IMap<T> where T : new()
    {
        T MapToObject(IList<string> values);
    }
}