using System;
using System.Collections.Generic;
using System.IO;
using MarkSFrancis.IO.Threaded;

namespace MarkSFrancis.IO.DelimitedData.Csv
{
    public class CsvWriter : IDisposable
    {
        private DelimitedDataWriter WriterStream { get; }
        private ThreadedWriter<IList<string>> ThreadedWriter { get; }
        private Stream SourceStream { get; }
        private bool CloseStreamWhenDisposed { get; }

        public string[] ColumnHeadings { get; private set; }

        public CsvWriter(string fileLocation, params string[] columnHeadings)
        {
            SourceStream = new FileStream(fileLocation, FileMode.Create, FileAccess.Write);
            WriterStream = DelimitedDataWriter.CsvWriter(SourceStream);

            ThreadedWriter = new ThreadedWriter<IList<string>>(WriteRecordThreaded);

            if (columnHeadings != null && columnHeadings.Length > 0)
            {
                SetColumnHeadings(columnHeadings);
            }

            CloseStreamWhenDisposed = true;
        }

        public CsvWriter(Stream source, bool closeStreamWhenDisposed = false, params string[] columnHeadings)
        {
            WriterStream = DelimitedDataWriter.CsvWriter(source);

            ThreadedWriter = new ThreadedWriter<IList<string>>(WriteRecordThreaded);

            if (columnHeadings != null && columnHeadings.Length > 0)
            {
                SetColumnHeadings(columnHeadings);
            }

            CloseStreamWhenDisposed = closeStreamWhenDisposed;
        }

        /// <summary>
        /// This method only works if it's called before writing any records
        /// </summary>
        protected void SetColumnHeadings(string[] columnHeadings)
        {
            ColumnHeadings = columnHeadings;

            if (ColumnHeadings != null && ColumnHeadings.Length > 0)
            {
                WriteRecord(ColumnHeadings);
            }
        }

        public void WriteRecord(IList<string> data)
        {
            ThreadedWriter.Write(data);
        }

        public void WriteRecord(params string[] data)
        {
            ThreadedWriter.Write(data);
        }

        private void WriteRecordThreaded(IList<string> data)
        {
            lock (WriterStream)
            {
                WriterStream.WriteRow(data);
            }
        }

        public void Dispose()
        {
            ThreadedWriter.Dispose(true);

            if (CloseStreamWhenDisposed)
            {
                SourceStream.Dispose();
            }
        }
    }
}