using System.IO;
using MarkSFrancis.IO.DelimitedData.Maps;
using MarkSFrancis.IO.DelimitedData.Maps.Interfaces;

namespace MarkSFrancis.IO.DelimitedData.Csv.Mapped
{
    public class CsvReaderMapped<T> where T : new()
    {
        private IMap<T> Map { get; }
        private DelimitedDataReader DataReader { get; }

        public string[] Headings => DataReader.Headings;
        public bool FileHasHeadings => DataReader.FileHasHeadings;

        public CsvReaderMapped(string fileLocation, MapColumnName<T> map)
        {
            Map = map;
            DataReader = DelimitedDataReader.CsvReader(fileLocation, true);
        }

        public CsvReaderMapped(string fileLocation, MapColumnId<T> map, bool fileHasHeaders = true)
        {
            Map = map;
            DataReader = DelimitedDataReader.CsvReader(fileLocation, fileHasHeaders);
        }

        public CsvReaderMapped(Stream source, MapColumnName<T> map, bool closeSourceWhenDisposed = false)
        {
            Map = map;
            DataReader = DelimitedDataReader.CsvReader(source, true, closeSourceWhenDisposed);
        }
        
        public CsvReaderMapped(Stream source, MapColumnId<T> map, bool fileHasHeaders = true, bool closeSourceWhenDisposed = false)
        {
            Map = map;
            DataReader = DelimitedDataReader.CsvReader(source, fileHasHeaders, closeSourceWhenDisposed);
        }

        public T ReadRecord()
        {
            var values = DataReader.ReadRecord();

            if (values == null)
            {
                throw new EndOfStreamException();
            }

            return Map.MapToObject(values, DataReader.Headings);
        }
    }
}