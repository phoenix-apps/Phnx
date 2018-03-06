using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MarkSFrancis.IO.Extensions;
using MarkSFrancis.IO.Factory;
using MarkSFrancis.IO.Threaded;

namespace MarkSFrancis.IO.DelimitedData
{
    public class DelimitedDataReader : IDisposable
    {
        public char Splitter { get; }
        private bool CloseStreamWhenDisposed { get; }

        private ThreadedReader<string> ThreadedReader { get; }
        private TextReader MyStream { get; }

        public string[] Headings => headings?.ToArray();
        private readonly IList<string> headings;
        public bool FileHasHeadings => headings != null;

        private bool _endOfStream;
        public bool EndOfStream
        {
            get
            {
                if (_endOfStream)
                {
                    return _endOfStream;
                }

                lock (MyStream)
                {
                    if (ThreadedReader.CacheCount == 0 && MyStream.ReachedEnd())
                    {
                        return _endOfStream = true;
                    }
                }

                return false;
            }
        }

        public DelimitedDataReader(string fileLocation, char splitter, bool dataHasHeadings = true) : 
            this(
                new FileStream(fileLocation, FileMode.Open, FileAccess.Read), 
                splitter, 
                dataHasHeadings,
                true)
        {
        }

        public DelimitedDataReader(Stream myStream, char splitter, bool dataHasHeadings = true, bool closeStreamWhenDisposed = false)
        {
            Splitter = splitter;

            var streamFactory = new MemoryStreamFactory();
            MyStream = new StreamReader(myStream, streamFactory.DefaultEncoding);

            CloseStreamWhenDisposed = closeStreamWhenDisposed;
            ThreadedReader = new ThreadedReader<string>(ReadLineToBuffer);

            if (dataHasHeadings)
            {
                headings = ReadRecord();
            }
        }

        public static DelimitedDataReader CsvReader(Stream myStream, bool dataHasHeadings = true, bool closeStreamWhenDisposed = false)
        {
            return new DelimitedDataReader(myStream, ',', dataHasHeadings, closeStreamWhenDisposed);
        }

        public static DelimitedDataReader CsvReader(string fileLocation, bool dataHasHeadings = true)
        {
            return new DelimitedDataReader(fileLocation, ',', dataHasHeadings);
        }

        public static DelimitedDataReader TsvReader(Stream myStream, bool dataHasHeadings = true, bool closeStreamWhenDisposed = false)
        {
            return new DelimitedDataReader(myStream, '\t', dataHasHeadings, closeStreamWhenDisposed);
        }

        public static DelimitedDataReader TsvReader(string fileLocation, bool dataHasHeadings = true)
        {
            return new DelimitedDataReader(fileLocation, '\t', dataHasHeadings);
        }

        public List<string> ReadRecord()
        {
            List<string> readRecord = new List<string>();
            StringBuilder currentField = new StringBuilder();

            bool insideDoubleQuotes = false;
            bool insideSingleQuotes = false;

            bool lastCharWasSplitter = true;

            string lineRead = ThreadedReader.Read();

            char[] lineBuffer = lineRead?.ToCharArray();
            if (lineBuffer == null)
            {
                return new List<string>();
            }

            int currentLineCharIndex = 0;

            while (true)
            {
                switch (lineBuffer[currentLineCharIndex])
                {
                    case '"':
                        // Peek next char to see if it's also a " character
                        if (lineBuffer.Length > currentLineCharIndex + 1 &&
                            lineBuffer[currentLineCharIndex + 1] == '"')
                        {
                            currentField.Append('"');
                            currentLineCharIndex++;
                            break;
                        }

                        if (lastCharWasSplitter)
                        {
                            insideDoubleQuotes = true;
                        }
                        else
                        {
                            insideDoubleQuotes = false;
                        }

                        lastCharWasSplitter = false;
                        break;
                    case '\'':
                        // Peek next char to see if it's also a ' character
                        if (lineBuffer.Length > currentLineCharIndex + 1 &&
                            lineBuffer[currentLineCharIndex + 1] == '\'')
                        {
                            currentField.Append('\'');
                            currentLineCharIndex++;
                            break;
                        }

                        if (lastCharWasSplitter)
                        {
                            insideSingleQuotes = true;
                        }
                        else
                        {
                            insideSingleQuotes = !insideSingleQuotes;
                        }

                        lastCharWasSplitter = false;
                        break;
                    case '\r':
                        if (insideSingleQuotes || insideDoubleQuotes)
                        {
                            currentField.Append('\r');
                        }
                        else
                        {
                            if (lineBuffer.Length > currentLineCharIndex + 1 &&
                                lineBuffer[currentLineCharIndex + 1] == '\n')
                            {
                                // Windows newline (\r\n), so skip \n

                                // Skip char
                                ++currentLineCharIndex;
                            }
                            // else Linux newline (\r), no extra action needed

                            readRecord.Add(currentField.ToString());
                            return readRecord;
                        }

                        lastCharWasSplitter = false;
                        break;
                    case '\n':
                        if (insideSingleQuotes || insideDoubleQuotes)
                        {
                            currentField.Append('\n');
                        }
                        else
                        {
                            // OS X newline (\n)
                            readRecord.Add(currentField.ToString());
                            return readRecord;
                        }

                        lastCharWasSplitter = false;
                        break;
                    default:
                        if (lineBuffer[currentLineCharIndex] == Splitter)
                        {
                            if (insideSingleQuotes || insideDoubleQuotes)
                            {
                                currentField.Append(Splitter);
                            }
                            else
                            {
                                lastCharWasSplitter = true;

                                readRecord.Add(currentField.ToString());
                                currentField.Clear();
                            }
                        }

                        currentField.Append(lineBuffer[currentLineCharIndex]);

                        lastCharWasSplitter = false;
                        break;
                }

                currentLineCharIndex++;

                if (currentLineCharIndex == lineBuffer.Length)
                {
                    currentLineCharIndex = 0;
                    lineBuffer = ThreadedReader.Read()?.ToCharArray();

                    if (lineBuffer == null)
                    {
                        // End of file
                        readRecord.Add(currentField.ToString());
                        return readRecord;
                    }
                }
            }
        }

        private string ReadLineToBuffer()
        {
            if (EndOfStream)
            {
                return null;
            }

            lock (MyStream)
            {
                return MyStream.ReadLine() + Environment.NewLine;
            }
        }

        public void Dispose()
        {
            ThreadedReader?.Dispose();

            if (CloseStreamWhenDisposed)
            {
                MyStream?.Dispose();
            }
        }
    }
}