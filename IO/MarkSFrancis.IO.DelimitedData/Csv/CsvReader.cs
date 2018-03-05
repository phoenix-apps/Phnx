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

        public bool EndOfData => ReaderStream.EndOfStream;

        public List<string> ColumnHeadings { get; }

        public CsvReader(string fileLocation, bool fileHasHeaders = true) :
            this(new FileStream(fileLocation, FileMode.Open, FileAccess.Read), fileHasHeaders, true)
        {
        }

        public CsvReader(Stream source, bool fileHasHeaders = true, bool closeStreamWhenDisposed = false)
        {
            ReaderStream = DelimitedDataReader.CsvReader(source, closeStreamWhenDisposed);

            if (fileHasHeaders)
            {
                ColumnHeadings = ReadLine();
            }
        }

        public List<string> ReadLine()
        {
            return ReaderStream.ReadRow().ToList();
        }

        public void Dispose()
        {
            ReaderStream.Dispose();
        }
    }
}