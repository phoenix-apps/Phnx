using System.IO;
using MarkSFrancis.IO.DelimitedData.Maps;

namespace MarkSFrancis.IO.DelimitedData.Csv.Mapped
{
    public class CsvReaderMapped<T> : CsvReader where T : new()
    {
        private NameMap<T> Map { get; }

        public CsvReaderMapped(string fileLocation)
            : base(fileLocation, fileHasHeaders: true)
        {
            Map = NameMap<T>.FromProperties();
        }

        public CsvReaderMapped(Stream source, bool closeSourceWhenDisposed = false)
            : base(source, closeStreamWhenDisposed: closeSourceWhenDisposed, fileHasHeaders: true)
        {
            Map = NameMap<T>.FromProperties();
        }

        public CsvReaderMapped(Stream source, NameMap<T> map, bool closeSourceWhenDisposed = false)
            : base(source, closeStreamWhenDisposed: closeSourceWhenDisposed, fileHasHeaders: true)
        {
            Map = map;
        }

        public T ReadRecord()
        {
            var values = ReadLine();

            if (values == null)
            {
                throw new EndOfStreamException();
            }

            lock (Map)
            {
                return Map.MapToObject(values, ColumnHeadings);
            }
        }
    }
}