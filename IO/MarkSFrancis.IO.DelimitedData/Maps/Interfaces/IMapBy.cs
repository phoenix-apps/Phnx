using MarkSFrancis.Reflection;

namespace MarkSFrancis.IO.DelimitedData.Maps.Interfaces
{
    public interface IMapBy<T, TMapBy> : IMap<T> where T : new()
    {
        void Map(PropertyFieldInfo<T, object> propFieldToMap, TMapBy mapTo);
    }
}