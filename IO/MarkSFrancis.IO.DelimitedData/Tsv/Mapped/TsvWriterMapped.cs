namespace MarkSFrancis.IO.DelimitedData.Tsv.Mapped
{
    public class TsvWriterMapped<T> : TsvWriter where T : new()
    {
        public TsvWriterMapped() : base(fileLocation: null)
        {
            throw ErrorFactory.Default.NotImplemented("Refactor class");
        }
    }
}