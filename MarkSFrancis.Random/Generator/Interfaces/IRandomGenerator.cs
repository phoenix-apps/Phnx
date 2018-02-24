namespace MarkSFrancis.Random.Generator.Interfaces
{
    public interface IRandomGenerator<out T>
    {
        T Get();
    }
}