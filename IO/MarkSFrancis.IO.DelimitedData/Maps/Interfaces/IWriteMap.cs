namespace MarkSFrancis.IO.DelimitedData.Maps.Interfaces
{
    public interface IWriteMap<T> : IMap<T> where T : new()
    {
        string[] MapFromObject(T record);
    }
}