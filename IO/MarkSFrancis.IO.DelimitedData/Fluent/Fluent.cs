using System.IO;
using MarkSFrancis.IO.DelimitedData.Mapped;
using MarkSFrancis.IO.DelimitedData.Maps;
using MarkSFrancis.IO.DelimitedData.Maps.Interfaces;

namespace MarkSFrancis.IO.DelimitedData
{
    public static class Delimited
    {
        public static FluentCsv Csv(string fileLocation)
        {
            return new FluentCsv(fileLocation);
        }

        public static FluentCsv Csv(Stream dataStream, bool closeStreamWhenDisposed = true)
        {
            return new FluentCsv(dataStream, closeStreamWhenDisposed);
        }
    }

    public class FluentCsv
    {
        private bool UseStreams { get; }
        private Stream DataStream { get; }
        private bool CloseStreamWhenDisposed { get; }
        private string FileLocation { get; }

        internal FluentCsv(Stream dataStream, bool closeStreamWhenDisposed = true)
        {
            UseStreams = true;
            DataStream = dataStream;
            CloseStreamWhenDisposed = closeStreamWhenDisposed;
        }

        internal FluentCsv(string fileLocation)
        {
            FileLocation = fileLocation;
        }

        public FluentCsvWriter Writer(bool dataHasHeadings)
        {
            return new FluentCsvWriter(UseStreams, DataStream, CloseStreamWhenDisposed, FileLocation);
        }

        public FluentCsvWriter Writer<T>(bool dataHasHeadings) where T : new()
        {
            return new FluentCsvWriter(UseStreams, DataStream, CloseStreamWhenDisposed, FileLocation);
        }

        public FluentCsvWriter Writer<T>(bool dataHasHeadings, IMap<T> map) where T : new()
        {
            return new FluentCsvWriter(UseStreams, DataStream, CloseStreamWhenDisposed, FileLocation);
        }

        public FluentCsvReader Reader()
        {
            return new FluentCsvReader(UseStreams, DataStream, CloseStreamWhenDisposed, FileLocation);
        }
    }

    public class FluentCsvWriter
    {
        public bool UseStreams { get; }
        public Stream DataStream { get; }
        public bool CloseStreamWhenDisposed { get; }
        public string FileLocation { get; }

        internal FluentCsvWriter(bool useStreams, Stream dataStream, bool closeStreamWhenDisposed, string fileLocation)
        {
            UseStreams = useStreams;
            DataStream = dataStream;
            CloseStreamWhenDisposed = closeStreamWhenDisposed;
            FileLocation = fileLocation;
        }

        public DelimitedDataWriter WithoutMap(string[] headings)
        {
            if (UseStreams)
            {
                return DelimitedDataWriter.CsvWriter(DataStream, CloseStreamWhenDisposed, headings);
            }
            else
            {
                return DelimitedDataWriter.CsvWriter(FileLocation, headings);
            }
        }

        public DelimitedDataWriter<T> WithAutoMap<T>() where T : new()
        {
            if (UseStreams)
            {
                IMap<T> autoMap = MapColumnName<T>.AutoMap();

                return new DelimitedDataWriter<T>(DataStream, CloseStreamWhenDisposed, autoMap, true, DelimitedDataWriter.CsvWriter);
            }
            else
            {
                IMap<T> autoMap = MapColumnName<T>.AutoMap();

                return new DelimitedDataWriter<T>(FileLocation, autoMap, true, DelimitedDataWriter.CsvWriter);
            }
        }

        public DelimitedDataWriter<T> WithColumnIdMap<T>(MapColumnId<T> map, bool writeHeadings) where T : new()
        {
            if (UseStreams)
            {
                return new DelimitedDataWriter<T>(DataStream, CloseStreamWhenDisposed, map, writeHeadings, DelimitedDataWriter.CsvWriter);
            }
            else
            {
                return new DelimitedDataWriter<T>(FileLocation, map, writeHeadings, DelimitedDataWriter.CsvWriter);
            }
        }

        public DelimitedDataWriter<T> WithColumnNameMap<T>(MapColumnName<T> map) where T : new()
        {
            if (UseStreams)
            {
                return new DelimitedDataWriter<T>(DataStream, CloseStreamWhenDisposed, map, true, DelimitedDataWriter.CsvWriter);
            }
            else
            {
                return new DelimitedDataWriter<T>(FileLocation, map, true, DelimitedDataWriter.CsvWriter);
            }
        }
    }

    public class FluentCsvReader
    {
        public bool UseStreams { get; }
        public Stream DataStream { get; }
        public bool CloseStreamWhenDisposed { get; }
        public string FileLocation { get; }

        internal FluentCsvReader(bool useStreams, Stream dataStream, bool closeStreamWhenDisposed, string fileLocation)
        {
            UseStreams = useStreams;
            DataStream = dataStream;
            CloseStreamWhenDisposed = closeStreamWhenDisposed;
            FileLocation = fileLocation;
        }

        public DelimitedDataReader WithoutMap(bool dataHasHeadings)
        {
            if (UseStreams)
            {
                return DelimitedDataReader.CsvReader(DataStream, dataHasHeadings, CloseStreamWhenDisposed);
            }
            else
            {
                return DelimitedDataReader.CsvReader(FileLocation, dataHasHeadings);
            }
        }

        public DelimitedDataReader<T> WithAutoMap<T>() where T : new()
        {
            var autoMap = MapColumnName<T>.AutoMap();
            
            return WithColumnNameMap(autoMap);
        }

        public DelimitedDataReader<T> WithColumnIdMap<T>(MapColumnId<T> map, bool writeHeadings) where T : new()
        {
            return CreateMapped(map, writeHeadings);
        }

        public DelimitedDataReader<T> WithColumnNameMap<T>(MapColumnName<T> map) where T : new()
        {
            return CreateMapped(map, true);
        }

        private DelimitedDataReader<T> CreateMapped<T>(IMap<T> map, bool fileHasHeaders) where T : new()
        {
            if (UseStreams)
            {
                return new DelimitedDataReader<T>(DataStream, CloseStreamWhenDisposed, map, fileHasHeaders, DelimitedDataReader.CsvReader);
            }
            else
            {
                return new DelimitedDataReader<T>(FileLocation, map, fileHasHeaders, DelimitedDataReader.CsvReader);
            }
        }
    }
}
