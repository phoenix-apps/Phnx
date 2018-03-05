using System.IO;
using MarkSFrancis.IO.DelimitedData.Maps;

namespace MarkSFrancis.IO.DelimitedData.Csv.Mapped
{
    public class CsvReaderMapped<T> : CsvReader where T : new()
    {
        private NameMap<T> NameMap { get; }
        private NumberMap<T> NumberMap { get; }
        public bool MappedByNames => NameMap != null;

        public CsvReaderMapped(string fileLocation, NameMap<T> map)
            : base(fileLocation, fileHasHeaders: true)
        {
            NameMap = map;
        }

        public CsvReaderMapped(string fileLocation, NumberMap<T> map, bool fileHasHeaders = false)
            : base(fileLocation, fileHasHeaders: fileHasHeaders)
        {
            NumberMap = map;
        }

        public CsvReaderMapped(Stream source, NameMap<T> map, bool closeSourceWhenDisposed = false)
            : base(source, closeStreamWhenDisposed: closeSourceWhenDisposed, fileHasHeaders: true)
        {
            NameMap = map;
        }

        public CsvReaderMapped(Stream source, NumberMap<T> map, bool closeSourceWhenDisposed = false,
            bool fileHasHeaders = false)
            : base(source, closeStreamWhenDisposed: closeSourceWhenDisposed, fileHasHeaders: fileHasHeaders)
        {
            NumberMap = map;
        }

        public static CsvReaderMapped<T> AutoMapped(Stream source, bool closeSourceWhenDisposed = false,
            bool autoMapProperties = true, bool autoMapFields = false)
        {
            var map = AutoMap(autoMapProperties, autoMapFields);
            return new CsvReaderMapped<T>(source, map, closeSourceWhenDisposed);
        }

        public static CsvReaderMapped<T> AutoMapped(string fileLocation, bool autoMapProperties = true,
            bool autoMapFields = false)
        {
            var map = AutoMap(autoMapProperties, autoMapFields);
            return new CsvReaderMapped<T>(fileLocation, map);
        }

        private static NameMap<T> AutoMap(bool autoMapProperties, bool autoMapFields)
        {
            var map = new NameMap<T>();

            if (autoMapFields)
            {
                map.AutoMapFields();
            }
            if (autoMapProperties)
            {
                map.AutoMapProperties();
            }

            return map;
        }

        public T ReadRecord()
        {
            var values = ReadLine();

            if (values == null)
            {
                throw new EndOfStreamException();
            }

            if (MappedByNames)
            {
                lock (NameMap)
                {
                    return NameMap.MapToObject(values, ColumnHeadings);
                }
            }
            else
            {
                lock (NumberMap)
                {
                    return NumberMap.MapToObject(values);
                }
            }
        }
    }
}