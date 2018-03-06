using System.IO;
using MarkSFrancis.IO.DelimitedData.Maps;
using MarkSFrancis.IO.DelimitedData.Maps.Interfaces;

namespace MarkSFrancis.IO.DelimitedData.Csv.Mapped
{
    public class CsvReaderMapped<T> : CsvReader where T : new()
    {
        private IMap<T> Map { get; }

        private CsvReaderMapped(string fileLocation, bool autoMapProperties, bool autoMapFields) : base(fileLocation, fileHasHeaders: true)
        {
            Map = MapColumnName<T>.AutoMap(false, autoMapProperties, autoMapFields);
        }

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

        private CsvReaderMapped(Stream source, bool autoMapProperties, bool autoMapFields, bool closeSourceWhenDisposed = false)
            : base(source, closeStreamWhenDisposed: closeSourceWhenDisposed, fileHasHeaders: true)
        {
            Map = MapColumnName<T>.AutoMap(false, autoMapProperties, autoMapFields);
        }

        public CsvReaderMapped(Stream source, MapColumnId<T> map, bool closeSourceWhenDisposed = false, bool fileHasHeaders = false)
            : base(source, closeStreamWhenDisposed: closeSourceWhenDisposed, fileHasHeaders: fileHasHeaders)
        {
            Map = map;
        }

        public static CsvReaderMapped<T> AutoMapped(Stream source, bool autoMapProperties = true, bool autoMapFields = false, bool closeSourceWhenDisposed = false)
        {
            return new CsvReaderMapped<T>(source, autoMapProperties, autoMapFields, closeSourceWhenDisposed);
        }

        public static CsvReaderMapped<T> AutoMapped(string fileLocation, bool autoMapProperties = true, bool autoMapFields = false)
        {
            return new CsvReaderMapped<T>(fileLocation, autoMapProperties, autoMapFields);
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