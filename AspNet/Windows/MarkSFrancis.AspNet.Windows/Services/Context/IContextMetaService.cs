namespace MarkSFrancis.AspNet.Windows.Services.Context
{
    public interface IContextMetaService<in TKey, TType>
    {
        TType Get(TKey key);

        void Set(TKey key, TType type);
    }
}
