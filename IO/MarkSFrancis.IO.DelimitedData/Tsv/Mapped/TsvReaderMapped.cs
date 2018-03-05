namespace MarkSFrancis.IO.DelimitedData.Tsv.Mapped
{
    public class TsvReaderMapped<T> : TsvReader where T : new()
    {
        public TsvReaderMapped() : base(fileLocation: null)
        {
            throw ErrorFactory.Default.NotImplemented("Refactor class");
        }
    }
}