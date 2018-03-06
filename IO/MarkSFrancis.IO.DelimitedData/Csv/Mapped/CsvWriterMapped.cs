using System.IO;
using System.Linq;
using MarkSFrancis.IO.DelimitedData.Maps;
using MarkSFrancis.IO.DelimitedData.Maps.Interfaces;

namespace MarkSFrancis.IO.DelimitedData.Csv.Mapped
{
    public class CsvWriterMapped<T> : CsvWriter where T : new()
    {
        private IMap<T> Map { get; }

        public CsvWriterMapped(string fileLocation, MapColumnName<T> map)
            : base(fileLocation)
        {
            Map = map;
            WriteHeaders();
        }

        public CsvWriterMapped(string fileLocation, MapColumnId<T> map, bool writeColumnHeaders = false)
            : base(fileLocation)
        {
            Map = map;

            if (writeColumnHeaders)
            {
                WriteHeaders();
            }
        }

        public CsvWriterMapped(Stream source, MapColumnName<T> map, bool closeStreamWhenDisposed = false)
            : base(source, closeStreamWhenDisposed)
        {
            Map = map;
            
            WriteHeaders();
        }

        public CsvWriterMapped(Stream source, MapColumnId<T> map, bool closeStreamWhenDisposed = false, bool writeColumnHeaders = false)
            : base(source, closeStreamWhenDisposed)
        {
            Map = map;

            if (writeColumnHeaders)
            {
                WriteHeaders();
            }
        }

        private void WriteHeaders()
        {
            SetColumnHeadings(Map.MappedColumnNames.ToList());
        }

        public void WriteRecord(T data)
        {
            var values = Map.MapFromObject(data, ColumnHeadings);

            WriteRecord(values);
        }
    }
}