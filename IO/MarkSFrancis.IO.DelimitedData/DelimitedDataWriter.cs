using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MarkSFrancis.IO.Factory;
using MarkSFrancis.IO.Threaded;

namespace MarkSFrancis.IO.DelimitedData
{
    public class DelimitedDataWriter : IDisposable
    {
        public char Splitter { get; }
        private bool CloseStreamWhenDisposed { get; }

        private ThreadedWriter<string> ThreadedWriter { get; }
        private TextWriter MyStream { get; }

        public string[] Headings => headings?.ToArray();
        private readonly IList<string> headings;
        public bool FileHasHeadings => headings != null;

        public DelimitedDataWriter(string fileLocation, char splitter, params string[] headings)
            : this(new FileStream(fileLocation, FileMode.Create, FileAccess.Write), splitter, true, headings)
        {
        }

        public DelimitedDataWriter(Stream myStream, char splitter, bool closeStreamWhenDisposed = false, params string[] headings)
        {
            Splitter = splitter;

            var streamFactory = new MemoryStreamFactory();
            MyStream = new StreamWriter(myStream, streamFactory.DefaultEncoding);

            CloseStreamWhenDisposed = closeStreamWhenDisposed;
            ThreadedWriter = new ThreadedWriter<string>(WriteLineFromBuffer);

            this.headings = headings;
            WriteRecord(headings);
        }

        public static DelimitedDataWriter CsvWriter(Stream myStream, bool closeStreamWhenDisposed = false, params string[] headings)
        {
            return new DelimitedDataWriter(myStream, ',', closeStreamWhenDisposed, headings);
        }
        
        public static DelimitedDataWriter TsvWriter(Stream myStream, bool closeStreamWhenDisposed = false, params string[] headings)
        {
            return new DelimitedDataWriter(myStream, '\t', closeStreamWhenDisposed, headings);
        }

        public static DelimitedDataWriter CsvWriter(string fileLocation, params string[] headings)
        {
            return new DelimitedDataWriter(fileLocation, ',', headings);
        }

        public static DelimitedDataWriter TsvWriter(string fileLocation, params string[] headings)
        {
            return new DelimitedDataWriter(fileLocation, '\t', headings);
        }

        public void WriteRecord(IList<string> values)
        {
            StringBuilder textToWrite = new StringBuilder();

            for (int valueIndex = 0; valueIndex < values.Count; valueIndex++)
            {
                var sanitisedField = SanitiseFieldForWriting(values[valueIndex], Splitter);

                textToWrite.Append(sanitisedField);
                textToWrite.Append(Splitter);
            }

            if (textToWrite.Length > 0)
            {
                ThreadedWriter.Write(textToWrite.ToString(0, textToWrite.Length - 1 /* Remove trailing splitter */));
            }
            else
            {
                ThreadedWriter.Write("");
            }
        }

        public void WriteRecord(params string[] values)
        {
            WriteRecord((IList<string>)values);
        }

        private void WriteLineFromBuffer(string data)
        {
            lock (MyStream)
            {
                MyStream.WriteLine(data);
            }
        }

        public static string SanitiseFieldForWriting(string field, char fieldSplitter)
        {
            string valueToPrint = field ?? "";

            valueToPrint = valueToPrint.Replace("\"", "\"\"");
            valueToPrint = valueToPrint.Replace("'", "''");

            if (valueToPrint.Contains(fieldSplitter) || valueToPrint.Contains(Environment.NewLine) || valueToPrint.Contains("\"") || valueToPrint.Contains("'"))
            {
                // Surround with speech marks
                return "\"" + valueToPrint + "\"";
            }

            return valueToPrint;
        }

        public void Dispose()
        {
            ThreadedWriter?.Dispose();

            if (CloseStreamWhenDisposed)
            {
                MyStream?.Dispose();
            }
        }
    }
}