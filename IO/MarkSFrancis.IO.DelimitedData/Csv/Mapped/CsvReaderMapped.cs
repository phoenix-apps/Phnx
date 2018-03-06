using System.IO;
using MarkSFrancis.IO.DelimitedData.Maps;
using MarkSFrancis.IO.DelimitedData.Maps.Interfaces;

namespace MarkSFrancis.IO.DelimitedData.Csv.Mapped
{
    public class CsvReaderMapped<T> : CsvReader where T : new()
    {
        private IMap<T> Map { get; }

        public CsvReaderMapped(string fileLocation, MapColumnName<T> map)
            : base(fileLocation, fileHasHeaders: true)
        {
            Map = map;
        }

        public CsvReaderMapped(string fileLocation, MapColumnId<T> map, bool fileHasHeaders = false)
            : base(fileLocation, fileHasHeaders: fileHasHeaders)
        {
            Map = map;
        }

        public CsvReaderMapped(Stream source, MapColumnName<T> map, bool closeSourceWhenDisposed = false)
            : base(source, closeStreamWhenDisposed: closeSourceWhenDisposed, fileHasHeaders: true)
        {
            Map = map;
        }
        
        public CsvReaderMapped(Stream source, MapColumnId<T> map, bool closeSourceWhenDisposed = false, bool fileHasHeaders = false)
            : base(source, closeStreamWhenDisposed: closeSourceWhenDisposed, fileHasHeaders: fileHasHeaders)
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

            return Map.MapToObject(values, ColumnHeadings);
        }
    }
}