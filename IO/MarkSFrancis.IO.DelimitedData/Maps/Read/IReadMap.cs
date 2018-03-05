using System.Collections.Generic;
using MarkSFrancis.Reflection;

namespace MarkSFrancis.IO.DelimitedData.Maps.Read
{
    public interface IReadMap<T, TMapBy> : IMap<T, TMapBy> where T : new()
    {
        bool TryAddMap(PropertyFieldInfo<T, object> propFieldToMap, TMapBy mapTo);

        T MapToObject(IList<string> values);
    }
}