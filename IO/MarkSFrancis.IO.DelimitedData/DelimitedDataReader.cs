using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MarkSFrancis.IO.Threaded;

namespace MarkSFrancis.IO.DelimitedData
{
    public class DelimitedDataReader : IDisposable
    {
        public string Splitter { get; }

        private ThreadedReader<string> ThreadedReader { get; }
        private TextReader MyStream { get; }

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
                    if (ThreadedReader.CacheCount == 0 && MyStream.Peek() == -1)
                    {
                        return _endOfStream = true;
                    }
                }

                return false;
            }
        }

    public DelimitedDataReader(string textToRead, string splitter)
        {
            Splitter = splitter;
            MyStream = CreateStreamReaderFromText(textToRead);

            ThreadedReader = new ThreadedReader<string>(ReadLineToBuffer);
        }

        public DelimitedDataReader(Stream myStream, string splitter)
        {
            MyStream = new StreamReader(myStream, EncodingHelper.DefaultEncoding);
            Splitter = splitter;

            ThreadedReader = new ThreadedReader<string>(ReadLineToBuffer);
        }

        public static DelimitedDataReader CsvReader(Stream myStream)
        {
            return new DelimitedDataReader(myStream, ",");
        }

        public static DelimitedDataReader CsvReader(string textToRead)
        {
            return new DelimitedDataReader(textToRead, ",");
        }

        public static DelimitedDataReader TsvReader(Stream myStream)
        {
            return new DelimitedDataReader(myStream, "\t");
        }

        public static DelimitedDataReader TsvReader(string textToRead)
        {
            return new DelimitedDataReader(textToRead, "\t");
        }

        private static StreamReader CreateStreamReaderFromText(string text)
        {
            MemoryStream ms = new MemoryStream();
            StreamWriter writer = new StreamWriter(ms);
            writer.Write(text);
            writer.Flush();
            ms.Position = 0;

            return new StreamReader(ms, EncodingHelper.DefaultEncoding);
        }

        public IEnumerable<string> ReadRow()
        {
            StringBuilder currentField = new StringBuilder();

            bool insideDoubleQuotes = false;
            bool insideSingleQuotes = false;

            bool lastCharWasSplitter = true;

            string lineRead = ThreadedReader.Read();

            char[] lineBuffer = lineRead?.ToCharArray();
            if (lineBuffer == null) yield break;

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
                    case ',':
                        if (insideSingleQuotes || insideDoubleQuotes)
                        {
                            currentField.Append(',');
                        }
                        else
                        {
                            lastCharWasSplitter = true;

                            yield return currentField.ToString();
                            currentField.Clear();
                        }
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

                            yield return currentField.ToString();
                            yield break;
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
                            yield return currentField.ToString();
                            yield break;
                        }

                        lastCharWasSplitter = false;
                        break;
                    default:
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
                        yield return currentField.ToString();
                        yield break;
                    }
                }
            }
        }

        public string ReadLineToBuffer()
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
            MyStream?.Dispose();
        }
    }
}