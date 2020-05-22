using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Phnx.Console
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
            Input = input ?? throw new ArgumentNullException(nameof(input));
            Output = output ?? throw new ArgumentNullException(nameof(output));
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
        /// Write a number of new lines to the <see cref="Output"/>
        /// </summary>
        /// <param name="lines">The number of new lines to write</param>
        /// <exception cref="ObjectDisposedException"><see cref="Output"/> is closed</exception>
        /// <exception cref="IOException">An I/O error occured</exception>
        public void NewLine(int lines = 1)
        {
            for (int index = 0; index < lines; ++index)
            {
                Output.WriteLine();
            }

            DoAutoFlush();
        }

        /// <summary>
        /// Write a line of text to the <see cref="Output"/>, followed by a newline
        /// </summary>
        /// <param name="s">The text to write</param>
        /// <exception cref="ObjectDisposedException"><see cref="Output"/> is closed</exception>
        /// <exception cref="IOException">An I/O error occured</exception>
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
        /// <exception cref="IOException">An I/O error occured</exception>
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
        /// <exception cref="IOException">An I/O error occured</exception>
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
        /// <exception cref="IOException">An I/O error occured</exception>
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
        /// <exception cref="IOException">An I/O error occured</exception>
        public void WriteCollection(params string[] collection)
        {
            WriteCollection((IEnumerable<string>)collection);
        }

        /// <summary>
        /// Write a collection of objects to the <see cref="Output"/> using their default <see cref="object.ToString()"/>, with each value delimited by a newline
        /// </summary>
        /// <param name="collection">The collection of objects to write</param>
        /// <exception cref="ObjectDisposedException"><see cref="Output"/> is closed</exception>
        /// <exception cref="IOException">An I/O error occured</exception>
        public void WriteCollection(params object[] collection)
        {
            WriteCollection((IEnumerable<object>)collection);
        }

        /// <summary>
        /// Write a collection of text to the <see cref="Output"/>, with each value delimited by a newline
        /// </summary>
        /// <param name="collection">The collection of text to write</param>
        /// <exception cref="ObjectDisposedException"><see cref="Output"/> is closed</exception>
        /// <exception cref="IOException">An I/O error occured</exception>
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
        /// <exception cref="IOException">An I/O error occured</exception>
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
        /// <exception cref="IOException">An I/O error occured</exception>
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
        /// <exception cref="IOException">An I/O error occured</exception>
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
        /// <exception cref="IOException">An I/O error occured</exception>
        public void WriteCollection<T>(IEnumerable<T> collection, string delimiter, Func<T, string> toString)
        {
            WriteCollection(collection.Select(toString), delimiter);
        }

        /// <summary>
        /// Get a line of text from the <see cref="Input"/>
        /// </summary>
        /// <returns>The next line from the input stream, or <see langword="null"/> if the end of the input stream is reached</returns>
        /// <exception cref="IOException">An I/O error occured</exception>
        /// <exception cref="OutOfMemoryException">There is insufficient memory to allocate a buffer for the line read from <see cref="Input"/></exception>
        /// <exception cref="ObjectDisposedException"><see cref="Input"/> is closed</exception>
        /// <exception cref="ArgumentOutOfRangeException">The number of characters in the next line is greater than <see cref="int.MaxValue"/></exception>
        public string ReadLine()
        {
            return Input.ReadLine();
        }
    }
}
