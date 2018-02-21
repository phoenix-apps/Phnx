using System.IO;
using MarkSFrancis.IO.DelimitedData.Maps;

namespace MarkSFrancis.IO.DelimitedData.Tsv.Mapped
{
    public class TsvReaderMapped<T> : TsvReader where T : new()
    {
        private NameMap<T> Map { get; }

        public TsvReaderMapped(string fileLocation)
            : base(fileLocation, fileHasHeaders: true)
        {
            Map = NameMap<T>.FromProperties();
        }

        public TsvReaderMapped(Stream source, bool closeSourceWhenDisposed = false)
            : base(source, closeStreamWhenDisposed: closeSourceWhenDisposed, fileHasHeaders: true)
        {
            Map = NameMap<T>.FromProperties();
        }

        public TsvReaderMapped(Stream source, NameMap<T> map, bool closeSourceWhenDisposed = false)
            : base(source, closeStreamWhenDisposed: closeSourceWhenDisposed, fileHasHeaders: true)
        {
            Map = map;
        }

        public T ReadRecord()
        {
            var values = ReadLine();

            lock (Map)
            {
                return Map.MapToObject(values, ColumnHeadings);
            }
        }
    }
}