using MarkSFrancis.Collections.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace MarkSFrancis.Console
{
    /// <summary>
    /// Stream data input and output between a <see cref="TextReader"/> and <see cref="TextWriter"/> such as <see cref="System.Console.In"/>, a file, or a web stream
    /// </summary>
    public class TextIoHelper
    {
        /// <summary>
        /// The text reader to get input from
        /// </summary>
        public TextReader Input { get; }

        /// <summary>
        /// The text writer to write output to
        /// </summary>
        public TextWriter Output { get; }

        /// <summary>
        /// Whether to automatically flush the output stream when writing to it
        /// </summary>
        public bool AutoFlush { get; set; }

        /// <summary>
        /// Creates a new instance of <see cref="TextIoHelper"/>
        /// </summary>
        /// <param name="input">Where to get input from</param>
        /// <param name="output">Where to send output to</param>
        /// <param name="autoFlush">Whether to automatically flush the output stream when writing to it</param>
        /// <exception cref="ArgumentNullException"><paramref name="input"/> or <paramref name="output"/> is <see langword="null"/></exception>
        public TextIoHelper(TextReader input, TextWriter output, bool autoFlush = false)
        {
            Input = input ?? throw ErrorFactory.Default.ArgumentNull(nameof(input));
            Output = output ?? throw ErrorFactory.Default.ArgumentNull(nameof(output));
            AutoFlush = autoFlush;
        }

        /// <summary>
        /// Flushes <see cref="Output"/> if <see cref="AutoFlush"/> is set to <see langword="true"/>
        /// </summary>
        private void DoAutoFlush()
        {
            if (AutoFlush)
            {
                Output.Flush();
            }
        }

        /// <summary>
        /// Write a line of text to the <see cref="Output"/>, followed by a newline
        /// </summary>
        /// <param name="s">The text to write</param>
        /// <exception cref="ObjectDisposedException"><see cref="Output"/> is closed</exception>
        /// <exception cref="IOException">An I/O error occurs</exception>
        public void WriteLine(string s = "")
        {
            Output.WriteLine(s);
            DoAutoFlush();
        }

        /// <summary>
        /// Write an oject to the <see cref="Output"/> using its <see cref="Object.ToString()"/>, followed by a newline
        /// </summary>
        /// <param name="o">The object to write</param>
        /// <exception cref="ObjectDisposedException"><see cref="Output"/> is closed</exception>
        /// <exception cref="IOException">An I/O error occurs</exception>
        public void WriteLine(object o)
        {
            Output.WriteLine(o);
            DoAutoFlush();
        }

        /// <summary>
        /// Write text to the <see cref="Output"/>
        /// </summary>
        /// <param name="s">The text to write</param>
        /// <exception cref="ObjectDisposedException"><see cref="Output"/> is closed</exception>
        /// <exception cref="IOException">An I/O error occurs</exception>
        public void Write(string s)
        {
            Output.Write(s);
            DoAutoFlush();
        }

        /// <summary>
        /// Write an oject to the <see cref="Output"/> using its <see cref="Object.ToString()"/>
        /// </summary>
        /// <param name="o">The object to write</param>
        /// <exception cref="ObjectDisposedException"><see cref="Output"/> is closed</exception>
        /// <exception cref="IOException">An I/O error occurs</exception>
        public void Write(object o)
        {
            Output.Write(o);
            DoAutoFlush();
        }

        /// <summary>
        /// Write a collection of text to the <see cref="Output"/>, with each value delimited by a newline
        /// </summary>
        /// <param name="collection">The collection of text to write</param>
        /// <exception cref="ObjectDisposedException"><see cref="Output"/> is closed</exception>
        /// <exception cref="IOException">An I/O error occurs</exception>
        public void WriteCollection(params string[] collection)
        {
            WriteCollection((IEnumerable<string>)collection);
        }

        /// <summary>
        /// Write a collection of objects to the <see cref="Output"/> using their default <see cref="object.ToString()"/>, with each value delimited by a newline
        /// </summary>
        /// <param name="collection">The collection of objects to write</param>
        /// <exception cref="ObjectDisposedException"><see cref="Output"/> is closed</exception>
        /// <exception cref="IOException">An I/O error occurs</exception>
        public void WriteCollection(params object[] collection)
        {
            WriteCollection((IEnumerable<object>)collection);
        }

        /// <summary>
        /// Write a collection of text to the <see cref="Output"/>, with each value delimited by a newline
        /// </summary>
        /// <param name="collection">The collection of text to write</param>
        /// <exception cref="ObjectDisposedException"><see cref="Output"/> is closed</exception>
        /// <exception cref="IOException">An I/O error occurs</exception>
        public void WriteCollection(IEnumerable<string> collection)
        {
            WriteCollection(collection, Environment.NewLine);
        }

        /// <summary>
        /// Write a collection of text to the <see cref="Output"/>, with each value delimited with custom text
        /// </summary>
        /// <param name="collection">The collection of text to write</param>
        /// <param name="delimiter">The delimiter to write between each value in the collection</param>
        /// <exception cref="ObjectDisposedException"><see cref="Output"/> is closed</exception>
        /// <exception cref="IOException">An I/O error occurs</exception>
        public void WriteCollection(IEnumerable<string> collection, string delimiter)
        {
            Output.Write(string.Join(delimiter, collection));
            DoAutoFlush();
        }

        /// <summary>
        /// Write a collection of objects to the <see cref="Output"/> using their default <see cref="object.ToString()"/>, with each value delimited by a newline
        /// </summary>
        /// <param name="collection">The collection of objects to write</param>
        /// <exception cref="ObjectDisposedException"><see cref="Output"/> is closed</exception>
        /// <exception cref="IOException">An I/O error occurs</exception>
        public void WriteCollection<T>(IEnumerable<T> collection)
        {
            WriteCollection(collection.Select(o => o.ToString()));
        }

        /// <summary>
        /// Write a collection of objects to the <see cref="Output"/> using their default <see cref="object.ToString()"/>, with each value delimited with text
        /// </summary>
        /// <param name="collection">The collection of objects to write</param>
        /// <param name="delimiter">The delimiter to write between each value in the collection</param>
        /// <exception cref="ObjectDisposedException"><see cref="Output"/> is closed</exception>
        /// <exception cref="IOException">An I/O error occurs</exception>
        public void WriteCollection<T>(IEnumerable<T> collection, string delimiter)
        {
            Output.Write(string.Join(delimiter, collection));
            DoAutoFlush();
        }

        /// <summary>
        /// Write a collection of objects to the <see cref="Output"/> using their default <see cref="object.ToString()"/>, with each value delimited with text
        /// </summary>
        /// <param name="collection">The collection of objects to write</param>
        /// <param name="delimiter">The delimiter to write between each value in the collection</param>
        /// <param name="toString">The function to use when converting each <typeparamref name="T"/> to a <see cref="string"/></param>
        /// <exception cref="ObjectDisposedException"><see cref="Output"/> is closed</exception>
        /// <exception cref="IOException">An I/O error occurs</exception>
        public void WriteCollection<T>(IEnumerable<T> collection, string delimiter, Func<T, string> toString)
        {
            WriteCollection(collection.Select(toString), delimiter);
        }

        /// <summary>
        /// Get a line of text from the <see cref="Input"/>
        /// </summary>
        /// <returns>The next line from the input stream, or <see langword="null"/> if the end of the input stream is reached</returns>
        /// <exception cref="IOException">An I/O error occurs</exception>
        /// <exception cref="OutOfMemoryException">There is insufficient memory to allocate a buffer for the line read from <see cref="Input"/></exception>
        /// <exception cref="ObjectDisposedException"><see cref="Input"/> is closed</exception>
        /// <exception cref="ArgumentOutOfRangeException">The number of characters in the next line is larger than <see cref="int.MaxValue"/></exception>
        public string GetString()
        {
            return Input.ReadLine();
        }

        /// <summary>
        /// Write a question to the <see cref="Output"/>, and then get a line of text from the <see cref="Input"/>
        /// </summary>
        /// <param name="question">The question to write</param>
        /// <returns>The next line from the input stream, or <see langword="null"/> if the end of the input stream is reached</returns>
        /// <exception cref="IOException">An I/O error occurs</exception>
        /// <exception cref="ObjectDisposedException"><see cref="Output"/> or <see cref="Input"/> is closed</exception>
        /// <exception cref="OutOfMemoryException">There is insufficient memory to allocate a buffer for the line read from <see cref="Input"/></exception>
        /// <exception cref="ArgumentOutOfRangeException">The number of characters in the next line is larger than <see cref="int.MaxValue"/></exception>
        public string GetString(string question)
        {
            Write(question);
            return GetString();
        }

        /// <summary>
        /// Write a question to the <see cref="Output"/>, and then attempt to get an <see cref="int"/> from the <see cref="Input"/> by reading a line from <see cref="Input"/> and converting it. This process repeats if the received input is invalid. Any errors are written to <see cref="Debug"/>
        /// </summary>
        /// <param name="question">The question to write</param>
        /// <returns>The <see cref="int"/> entered by the user</returns>
        /// <exception cref="IOException">An I/O error occurs</exception>
        /// <exception cref="ObjectDisposedException"><see cref="Output"/> or <see cref="Input"/> is closed</exception>
        /// <exception cref="OutOfMemoryException">There is insufficient memory to allocate a buffer for the line read from <see cref="Input"/></exception>
        /// <exception cref="ArgumentOutOfRangeException">The number of characters in the next line is larger than <see cref="int.MaxValue"/></exception>
        /// <exception cref="EndOfStreamException">Reached the end of <see cref="Input"/></exception>
        /// <exception cref="InvalidCastException">An error occurred converting the input to a <see cref="int"/></exception>
        public int GetInt(string question = null)
        {
            return Get(int.Parse, question);
        }

        /// <summary>
        /// Write a question to the <see cref="Output"/> and the each entry in <paramref name="options"/> prepended with their 1-based index, and then attempt to get an <see cref="int"/> by reading a line from <see cref="Input"/> and converting it to a 0-based index within the range of <paramref name="options"/>
        /// </summary>
        /// <param name="options">The range of options to choose from</param>
        /// <param name="question">The question to write</param>
        /// <returns>The index of the selected option</returns>
        /// <exception cref="ArgumentNullException"><paramref name="options"/> is <see langword="null"/></exception>
        /// <exception cref="IOException">An I/O error occurs</exception>
        /// <exception cref="ObjectDisposedException"><see cref="Output"/> or <see cref="Input"/> is closed</exception>
        /// <exception cref="OutOfMemoryException">There is insufficient memory to allocate a buffer for the line read from <see cref="Input"/></exception>
        /// <exception cref="ArgumentOutOfRangeException">The number of characters in the next line is larger than <see cref="int.MaxValue"/></exception>
        /// <exception cref="EndOfStreamException">Reached the end of <see cref="Input"/></exception>
        /// <exception cref="InvalidCastException">An error occurred converting the input to a <see cref="int"/></exception>
        public int GetSelection(IEnumerable<string> options, string question = null)
        {
            if (options == null)
            {
                throw ErrorFactory.Default.ArgumentNull(nameof(options));
            }

            // Format question
            var questionWithRange = new StringBuilder();

            if (question != null)
            {
                questionWithRange.AppendLine(question);
            }

            var optionCount = 0;

            foreach (var option in options)
            {
                questionWithRange.AppendLine(++optionCount + ": " + option);
            }

            if (optionCount == 0)
            {
                throw ErrorFactory.Default.ArgumentEmpty(nameof(options));
            }

            var questionWithRangeAsString = questionWithRange.ToString();

            int converter(string enteredValue)
            {
                var valueInt = int.Parse(enteredValue) - 1;

                if (valueInt < 0 || valueInt >= optionCount)
                {
                    throw ErrorFactory.Default.ArgumentOutOfRange(nameof(enteredValue));
                }

                return valueInt;
            }

            return Get(converter);
        }

        /// <summary>
        /// Write a question to the <see cref="Output"/>, and then get attempt to get a <see cref="long"/> by reading a line from <see cref="Input"/> and converting it
        /// </summary>
        /// <param name="question">The question to write</param>
        /// <returns>The <see cref="long"/> entered by the user</returns>
        /// <exception cref="IOException">An I/O error occurs</exception>
        /// <exception cref="ObjectDisposedException"><see cref="Output"/> or <see cref="Input"/> is closed</exception>
        /// <exception cref="OutOfMemoryException">There is insufficient memory to allocate a buffer for the line read from <see cref="Input"/></exception>
        /// <exception cref="ArgumentOutOfRangeException">The number of characters in the next line is larger than <see cref="int.MaxValue"/></exception>
        /// <exception cref="EndOfStreamException">Reached the end of <see cref="Input"/></exception>
        /// <exception cref="InvalidCastException">An error occurred converting the input to a <see cref="long"/></exception>
        public long GetLong(string question = null)
        {
            return Get(long.Parse, question);
        }

        /// <summary>
        /// Write a question to the <see cref="Output"/>, and then get attempt to get a <see cref="decimal"/> by reading a line from <see cref="Input"/> and converting it
        /// </summary>
        /// <param name="question">The question to write</param>
        /// <returns>The <see cref="decimal"/> entered by the user</returns>
        /// <exception cref="IOException">An I/O error occurs</exception>
        /// <exception cref="ObjectDisposedException"><see cref="Output"/> or <see cref="Input"/> is closed</exception>
        /// <exception cref="OutOfMemoryException">There is insufficient memory to allocate a buffer for the line read from <see cref="Input"/></exception>
        /// <exception cref="ArgumentOutOfRangeException">The number of characters in the next line is larger than <see cref="int.MaxValue"/></exception>
        /// <exception cref="EndOfStreamException">Reached the end of <see cref="Input"/></exception>
        /// <exception cref="InvalidCastException">An error occurred converting the input to a <see cref="decimal"/></exception>
        public decimal GetDecimal(string question = null)
        {
            return Get(decimal.Parse, question);
        }

        /// <summary>
        /// Write a question to the <see cref="Output"/>, and then get attempt to get a <see cref="DateTime"/> by reading a line from <see cref="Input"/> and converting it using <see cref="DateTime.Parse(string)"/>, which uses the current system's culture to convert
        /// </summary>
        /// <param name="question">The question to write</param>
        /// <returns>The <see cref="DateTime"/> entered by the user</returns>
        /// <exception cref="IOException">An I/O error occurs</exception>
        /// <exception cref="ObjectDisposedException"><see cref="Output"/> or <see cref="Input"/> is closed</exception>
        /// <exception cref="OutOfMemoryException">There is insufficient memory to allocate a buffer for the line read from <see cref="Input"/></exception>
        /// <exception cref="ArgumentOutOfRangeException">The number of characters in the next line is larger than <see cref="int.MaxValue"/></exception>
        /// <exception cref="EndOfStreamException">Reached the end of <see cref="Input"/></exception>
        /// <exception cref="InvalidCastException">An error occurred converting the input to a <see cref="DateTime"/></exception>
        public DateTime GetDateTime(string question = null)
        {
            return Get(DateTime.Parse, question);
        }

        /// <summary>
        /// Write a question to the <see cref="Output"/>, and then get attempt to get a value by reading a line from <see cref="Input"/> and converting it using <paramref name="converter"/>
        /// </summary>
        /// <param name="converter">The method to use when converting from the text to the desired type</param>
        /// <param name="question">The question to write</param>
        /// <returns>The <typeparamref name="T"/> converted from the line from <see cref="Input"/></returns>
        /// <exception cref="ArgumentNullException"><paramref name="converter"/> is <see langword="null"/></exception>
        /// <exception cref="IOException">An I/O error occurs</exception>
        /// <exception cref="ObjectDisposedException"><see cref="Output"/> or <see cref="Input"/> is closed</exception>
        /// <exception cref="OutOfMemoryException">There is insufficient memory to allocate a buffer for the line read from <see cref="Input"/></exception>
        /// <exception cref="ArgumentOutOfRangeException">The number of characters in the next line is larger than <see cref="int.MaxValue"/></exception>
        /// <exception cref="EndOfStreamException">Reached the end of <see cref="Input"/></exception>
        /// <exception cref="InvalidCastException">An error occurred in <paramref name="converter"/>. The exception from <paramref name="converter"/> is captured in the inner exception</exception>
        public virtual T Get<T>(Func<string, T> converter, string question = null)
        {
            if (converter == null)
            {
                throw ErrorFactory.Default.ArgumentNull(nameof(converter));
            }

            if (question != null)
            {
                Write(question);
            }

            var valueEntered = GetString();

            if (valueEntered == null)
            {
                throw new EndOfStreamException("Reached the end of " + nameof(Input));
            }

            try
            {
                return converter(valueEntered);
            }
            catch (Exception ex)
            {
                throw new InvalidCastException($"An error occurred converting from {typeof(string).FullName} to {typeof(T).FullName}: {ex.Message}", ex);
            }
        }
    }
}
