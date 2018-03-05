using System.Collections.Generic;
using System.IO;
using System.Linq;
using MarkSFrancis.IO.DelimitedData.Maps;
using MarkSFrancis.IO.DelimitedData.Maps.Interfaces;
using MarkSFrancis.IO.DelimitedData.Maps.Write;

namespace MarkSFrancis.IO.DelimitedData.Csv.Mapped
{
    public class CsvWriterMapped<T> : CsvWriter where T : new()
    {
        private IWriteMap<T> Map { get; }

        public CsvWriterMapped(string fileLocation, WriteMapColumnName<T> map)
            : base(fileLocation)
        {
            Map = map;
            WriteHeaders();
        }

        public CsvWriterMapped(string fileLocation, WriteMapColumnId<T> map, bool writeColumnHeaders = false)
            : base(fileLocation)
        {
            Map = map;

            if (writeColumnHeaders)
            {
                WriteHeaders();
            }
        }

        public CsvWriterMapped(Stream source, WriteMapColumnName<T> map, bool closeStreamWhenDisposed = false)
            : base(source, closeStreamWhenDisposed)
        {
            Map = map;
            
            WriteHeaders();
        }

        public CsvWriterMapped(Stream source, WriteMapColumnId<T> map, bool closeStreamWhenDisposed = false, bool writeColumnHeaders = false)
            : base(source, closeStreamWhenDisposed)
        {
            Map = map;

            if (writeColumnHeaders)
            {
                WriteHeaders();
            }
        }

        public static CsvWriterMapped<T> AutoMapped(Stream source, bool closeSourceWhenDisposed = false,
            bool autoMapProperties = true, bool autoMapFields = false)
        {
            var map = AutoMap(autoMapProperties, autoMapFields);
            return new CsvWriterMapped<T>(source, map, closeSourceWhenDisposed);
        }

        public static CsvWriterMapped<T> AutoMapped(string fileLocation, bool autoMapProperties = true,
            bool autoMapFields = false)
        {
            var map = AutoMap(autoMapProperties, autoMapFields);
            return new CsvWriterMapped<T>(fileLocation, map);
        }

        private void WriteHeaders()
        {
            SetColumnHeadings(Map.ColumnHeadings.ToList());
        }

        private static WriteMapColumnName<T> AutoMap(bool autoMapProperties, bool autoMapFields)
        {
            return WriteMapColumnName<T>.AutoMap(autoMapProperties, autoMapFields);
        }

        public void WriteRecord(T data)
        {
            var values = Map.MapFromObject(data);

            WriteRecord(values);
        }
    }
}