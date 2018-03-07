using System;
using System.IO;
using System.Linq;
using MarkSFrancis.IO.DelimitedData.Maps;
using MarkSFrancis.IO.DelimitedData.Maps.Interfaces;

namespace MarkSFrancis.IO.DelimitedData.Mapped
{
    public class DelimitedDataReader<T> : IDisposable where T : new()
    {
        private IMap<T> Map { get; }
        private DelimitedDataReader DataReader { get; }

        public string[] Headings => DataReader.Headings;
        public bool FileHasHeadings => DataReader.FileHasHeadings;

        internal DelimitedDataReader(string fileLocation, IMap<T> map, bool fileHasHeaders, Func<string, bool, DelimitedDataReader> baseDelimiterConstructor)
        {
            Map = map;

            DataReader = baseDelimiterConstructor(fileLocation, fileHasHeaders);
        }

        internal DelimitedDataReader(Stream data, bool closeStreamWhenDisposed, IMap<T> map, bool fileHasHeaders, Func<Stream, bool, bool, DelimitedDataReader> baseDelimiterConstructor)
        {
            Map = map;

            DataReader = baseDelimiterConstructor(data, closeStreamWhenDisposed, fileHasHeaders);
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

        public void Dispose()
        {
            DataReader?.Dispose();
        }
    }
}