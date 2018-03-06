using System.IO;
using System.Linq;
using MarkSFrancis.IO.DelimitedData.Maps;
using MarkSFrancis.IO.DelimitedData.Maps.Interfaces;

namespace MarkSFrancis.IO.DelimitedData.Csv.Mapped
{
    public class DataWriterMapped<T> where T : new()
    {
        private IMap<T> Map { get; }
        private DelimitedDataWriter DataWriter { get; }

        public string[] Headings => DataWriter.Headings;
        public bool FileHasHeadings => DataWriter.FileHasHeadings;

        public DataWriterMapped(string fileLocation, MapColumnName<T> map)
        {
            Map = map;

            DataWriter = DelimitedDataWriter.CsvWriter(fileLocation, map.MappedColumnNames.ToArray());
        }

        public DataWriterMapped(string fileLocation, MapColumnId<T> map, bool writeColumnHeaders = false)
        {
            Map = map;

            DataWriter = writeColumnHeaders ? 
                DelimitedDataWriter.CsvWriter(fileLocation, map.MappedColumnNames.ToArray()) : 
                DelimitedDataWriter.CsvWriter(fileLocation);
        }

        public DataWriterMapped(Stream source, MapColumnName<T> map, bool closeStreamWhenDisposed = false)
        {
            Map = map;

            DataWriter = DelimitedDataWriter.CsvWriter(source, closeStreamWhenDisposed, map.MappedColumnNames.ToArray());
        }

        public DataWriterMapped(Stream source, MapColumnId<T> map, bool closeStreamWhenDisposed = false, bool writeColumnHeaders = false)
        {
            Map = map;

            DataWriter = writeColumnHeaders ? 
                DelimitedDataWriter.CsvWriter(source, closeStreamWhenDisposed, map.MappedColumnNames.ToArray()) : 
                DelimitedDataWriter.CsvWriter(source, closeStreamWhenDisposed);
        }

        public void WriteRecord(T data)
        {
            var values = Map.MapFromObject(data, DataWriter.Headings);

            DataWriter.WriteRecord(values);
        }
    }
}