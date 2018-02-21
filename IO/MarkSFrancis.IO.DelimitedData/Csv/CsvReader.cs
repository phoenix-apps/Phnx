using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

// Guide on CSV format: https://www.wikiwand.com/en/Comma-separated_values#/Basic_rules

namespace MarkSFrancis.IO.DelimitedData.Csv
{
    public class CsvReader : IDisposable
    {
        private DelimitedDataReader ReaderStream { get; }
        private Stream SourceStream { get; }
        private bool CloseStreamWhenDisposed { get; }

        public bool EndOfData
        {
            get
            {
                lock (ReaderStream)
                {
                    return ReaderStream.EndOfStream;
                }
            }
        }

        public List<string> ColumnHeadings { get; }

        public CsvReader(string fileLocation, bool fileHasHeaders = true)
        {
            SourceStream = new FileStream(fileLocation, FileMode.Open, FileAccess.Read);
            ReaderStream = DelimitedDataReader.CsvReader(SourceStream);

            if (fileHasHeaders)
            {
                ColumnHeadings = ReadLine();
            }

            CloseStreamWhenDisposed = true;
        }

        public CsvReader(Stream source, bool fileHasHeaders = true, bool closeStreamWhenDisposed = false)
        {
            ReaderStream = DelimitedDataReader.CsvReader(source);

            if (fileHasHeaders)
            {
                ColumnHeadings = ReadLine();
            }

            CloseStreamWhenDisposed = closeStreamWhenDisposed;
        }

        public List<string> ReadLine()
        {
            return ReaderStream.ReadRow().ToList();
        }

        private List<string> ReadLineThreaded()
        {
            if (EndOfData)
            {
                throw new EndOfStreamException();
            }

            lock (ReaderStream)
            {
                return ReaderStream.ReadRow().ToList();
            }
        }

        public void Dispose()
        {
            ReaderStream.Dispose();

            if (CloseStreamWhenDisposed)
            {
                SourceStream.Dispose();
            }
        }
    }
}