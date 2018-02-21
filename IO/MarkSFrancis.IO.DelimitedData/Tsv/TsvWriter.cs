using System;
using System.Collections.Generic;
using System.IO;
using MarkSFrancis.IO.Threaded;

namespace MarkSFrancis.IO.DelimitedData.Tsv
{
    public class TsvWriter : IDisposable
    {
        private DelimitedDataWriter WriterStream { get; }
        private ThreadedWriter<IList<string>> ThreadedWriter { get; }
        private Stream SourceStream { get; }
        private bool CloseStreamWhenDisposed { get; }

        public string[] ColumnHeadings { get; private set; }

        public TsvWriter(string fileLocation, params string[] columnHeadings)
        {
            SourceStream = new FileStream(fileLocation, FileMode.Create, FileAccess.Write);
            WriterStream = DelimitedDataWriter.TsvWriter(SourceStream);

            ThreadedWriter = new ThreadedWriter<IList<string>>(WriteRecordThreaded);

            SetColumnHeadings(columnHeadings);

            CloseStreamWhenDisposed = true;
        }

        public TsvWriter(Stream source, bool closeStreamWhenDisposed = false, params string[] columnHeadings)
        {
            WriterStream = DelimitedDataWriter.TsvWriter(source);

            ThreadedWriter = new ThreadedWriter<IList<string>>(WriteRecordThreaded);

            SetColumnHeadings(columnHeadings);

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