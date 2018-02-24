namespace MarkSFrancis.Random.Generator.Interfaces
{
    public interface IRandomNumberGenerator<T> : IRandomGenerator<T> where T : struct
    {
        T Get(T inclusiveMinValue, T inclusiveMaxValue);
    }
}