using System;
using System.IO;
using System.Linq;
using MarkSFrancis.IO.DelimitedData.Maps;
using MarkSFrancis.IO.DelimitedData.Maps.Interfaces;

namespace MarkSFrancis.IO.DelimitedData.Mapped
{
    public class DelimitedDataWriter<T> : IDisposable where T : new()
    {
        private IMap<T> Map { get; }
        private DelimitedDataWriter DataWriter { get; }

        public string[] Headings => DataWriter.Headings;
        public bool FileHasHeadings => DataWriter.FileHasHeadings;

        internal DelimitedDataWriter(string fileLocation, IMap<T> map, bool writeHeaders, Func<string, string[], DelimitedDataWriter> baseDelimiterConstructor)
        {
            Map = map;

            DataWriter = baseDelimiterConstructor(fileLocation, map.MappedColumnNames.ToArray());
        }

        internal DelimitedDataWriter(Stream data, bool closeStreamWhenDisposed, IMap<T> map, bool writeHeaders, Func<Stream, bool, string[], DelimitedDataWriter> baseDelimiterConstructor)
        {
            Map = map;

            if (writeHeaders)
            {
                DataWriter = baseDelimiterConstructor(data, closeStreamWhenDisposed, Map.MappedColumnNames.ToArray());
            }
            else
            {
                DataWriter = baseDelimiterConstructor(data, closeStreamWhenDisposed, null);
            }
        }

        public void WriteRecord(T data)
        {
            var values = Map.MapFromObject(data, DataWriter.Headings);

            DataWriter.WriteRecord(values);
        }

        public void Dispose()
        {
            DataWriter?.Dispose();
        }
    }
}