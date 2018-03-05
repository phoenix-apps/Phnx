using System.Collections.Generic;
using System.IO;
using System.Linq;
using MarkSFrancis.IO.DelimitedData.Maps;

namespace MarkSFrancis.IO.DelimitedData.Csv.Mapped
{
    public class CsvWriterMapped<T> : CsvWriter where T : new()
    {
        private NameMap<T> NameMap { get; }
        private NumberMap<T> NumberMap { get; }
        public bool MappedByNames => NameMap != null;

        public CsvWriterMapped(string fileLocation, NameMap<T> map)
            : base(fileLocation)
        {
            NameMap = map;
            WriteHeaders();
        }

        public CsvWriterMapped(string fileLocation, NumberMap<T> map, bool writeColumnHeaders = false)
            : base(fileLocation)
        {
            NumberMap = map;

            if (writeColumnHeaders)
            {
                WriteHeaders();
            }
        }

        public CsvWriterMapped(Stream source, NameMap<T> map, bool closeStreamWhenDisposed = false)
            : base(source, closeStreamWhenDisposed)
        {
            NameMap = map;
            
            WriteHeaders();
        }

        public CsvWriterMapped(Stream source, NumberMap<T> map, bool closeStreamWhenDisposed = false, bool writeColumnHeaders = false)
            : base(source, closeStreamWhenDisposed)
        {
            NumberMap = map;

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
            IEnumerable<string> columnHeadings;
            if (MappedByNames)
            {
                columnHeadings = NameMap.GetColumnHeadings();
            }
            else
            {
                columnHeadings = NumberMap.GetColumnHeadings();
            }

            SetColumnHeadings(columnHeadings.ToArray());
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

        public void WriteRecord(T data)
        {
            IList<string> recordToWrite;
            if (MappedByNames)
            {
                lock (NameMap)
                {
                    recordToWrite = NameMap.MapFromObject(data);
                }
            }
            else
            {
                lock (NumberMap)
                {
                    recordToWrite = NumberMap.MapFromObject(data);
                }
            }

            WriteRecord(recordToWrite);
        }
    }
}