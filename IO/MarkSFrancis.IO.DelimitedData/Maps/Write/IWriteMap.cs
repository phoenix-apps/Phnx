using MarkSFrancis.Reflection;

namespace MarkSFrancis.IO.DelimitedData.Maps.Write
{
    public interface IWriteMap<T, TMapBy> : IMap<T, TMapBy> where T : new()
    {
        void AddColumnAndMap(PropertyFieldInfo<T, object> propFieldToMap, TMapBy mapTo);

        string[] MapFromObject(T record);
    }
}