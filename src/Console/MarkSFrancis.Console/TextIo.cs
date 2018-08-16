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
    public class TextIo
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
        /// Creates a new instance of <see cref="TextIo"/>
        /// </summary>
        /// <param name="input">Where to get input from</param>
        /// <param name="output">Where to send output to</param>
        public TextIo(TextReader input, TextWriter output)
        {
            Input = input;
            Output = output;
        }

        /// <summary>
        /// Write a line of text to the <see cref="Output"/>, followed by a newline
        /// </summary>
        /// <param name="s">The text to write</param>
        public void WriteLine(string s = "")
        {
            Output.WriteLine(s);
        }

        /// <summary>
        /// Write an oject to the <see cref="Output"/> using its <see cref="Object.ToString()"/>, followed by a newline
        /// </summary>
        /// <param name="o">The object to write</param>
        public void WriteLine(object o)
        {
            Output.WriteLine(o);
        }

        /// <summary>
        /// Write text to the <see cref="Output"/>
        /// </summary>
        /// <param name="s">The text to write</param>
        public void Write(string s)
        {
            Output.Write(s);
        }

        /// <summary>
        /// Write an oject to the <see cref="Output"/> using its <see cref="Object.ToString()"/>
        /// </summary>
        /// <param name="o">The object to write</param>
        public void Write(object o)
        {
            Output.Write(o);
        }

        /// <summary>
        /// Write a collection of text to the <see cref="Output"/>, with each value delimited by a newline
        /// </summary>
        /// <param name="collection">The collection of text to write</param>
        public void WriteCollection(params string[] collection)
        {
            WriteCollection((IEnumerable<string>)collection);
        }

        /// <summary>
        /// Write a collection of objects to the <see cref="Output"/> using their default <see cref="object.ToString()"/>, with each value delimited by a newline
        /// </summary>
        /// <param name="collection">The collection of objects to write</param>
        public void WriteCollection(params object[] collection)
        {
            WriteCollection((IEnumerable<object>)collection);
        }

        /// <summary>
        /// Write a collection of text to the <see cref="Output"/>, with each value delimited by a newline
        /// </summary>
        /// <param name="collection">The collection of text to write</param>
        public void WriteCollection(IEnumerable<string> collection)
        {
            WriteCollection(collection, Environment.NewLine);
        }

        /// <summary>
        /// Write a collection of text to the <see cref="Output"/>, with each value delimited with custom text
        /// </summary>
        /// <param name="collection">The collection of text to write</param>
        /// <param name="delimiter">The delimiter to write between each value in the collection</param>
        public void WriteCollection(IEnumerable<string> collection, string delimiter)
        {
            WriteLine(string.Join(delimiter, collection));
        }

        /// <summary>
        /// Write a collection of objects to the <see cref="Output"/> using their default <see cref="object.ToString()"/>, with each value delimited by a newline
        /// </summary>
        /// <param name="collection">The collection of objects to write</param>
        public void WriteCollection<T>(IEnumerable<T> collection)
        {
            WriteCollection(collection.Select(o => o.ToString()));
        }

        /// <summary>
        /// Write a collection of objects to the <see cref="Output"/> using their default <see cref="object.ToString()"/>, with each value delimited with text
        /// </summary>
        /// <param name="collection">The collection of objects to write</param>
        /// <param name="delimiter">The delimiter to write between each value in the collection</param>
        public void WriteCollection<T>(IEnumerable<T> collection, string delimiter)
        {
            WriteCollection(collection.Select(o => o.ToString()), delimiter);
        }

        /// <summary>
        /// Get a line of text from the <see cref="Input"/>
        /// </summary>
        /// <returns></returns>
        public string GetString()
        {
            return Input.ReadLine();
        }

        /// <summary>
        /// Write a question to the <see cref="Output"/>, and then get a line of text from the <see cref="Input"/>
        /// </summary>
        /// <param name="question">The question to write</param>
        /// <returns>The line of text entered by the user</returns>
        public string GetString(string question)
        {
            Write(question);
            return GetString();
        }

        /// <summary>
        /// Write a question to the <see cref="Output"/>, and then get attempt to get an <see cref="int"/> from the <see cref="Input"/>. This process repeats if the received input is invalid. Any errors are written to <see cref="Debug"/>
        /// </summary>
        /// <param name="question">The question to write</param>
        /// <returns>The <see cref="int"/> entered by the user</returns>
        public int GetInt(string question = null)
        {
            return Get(int.Parse, question);
        }

        /// <summary>
        /// Write a question to the <see cref="Output"/>, and then get attempt to get an <see cref="int"/> from the <see cref="Input"/> of the index of the selected option. This process repeats if the received input is invalid or out of range of the options. Any errors are written to <see cref="Debug"/>
        /// </summary>
        /// <param name="options">The range of options to choose from</param>
        /// <param name="question">The question to write</param>
        /// <returns>The index of the selected option</returns>
        public int GetSelection(IEnumerable<string> options, string question = null)
        {
            // Format question
            var questionWithRange = new StringBuilder();

            if (question != null)
            {
                questionWithRange.AppendLine(question);
            }

            var optionCount = 0;

            foreach (var option in options)
            {
                ++optionCount;
                questionWithRange.AppendLine(optionCount + ": " + option);
            }

            if (optionCount == 0)
            {
                throw ErrorFactory.Default.CollectionEmpty(nameof(options));
            }

            var questionWithRangeAsString = questionWithRange.ToString();

            // Ask question
            int selectedOption;
            do
            {
                selectedOption = GetInt(questionWithRangeAsString);

                // Ensure question is within selection range
            } while (selectedOption <= 0 || selectedOption > optionCount);

            return selectedOption;
        }

        /// <summary>
        /// Write a question to the <see cref="Output"/>, and then get attempt to get a <see cref="long"/> from the <see cref="Input"/>. This process repeats if the received input is invalid. Any errors are written to <see cref="Debug"/>
        /// </summary>
        /// <param name="question">The question to write</param>
        /// <returns>The <see cref="long"/> entered by the user</returns>
        public long GetLong(string question = null)
        {
            return Get(long.Parse, question);
        }

        /// <summary>
        /// Write a question to the <see cref="Output"/>, and then get attempt to get a <see cref="decimal"/> from the <see cref="Input"/>. This process repeats if the received input is invalid. Any errors are written to <see cref="Debug"/>
        /// </summary>
        /// <param name="question">The question to write</param>
        /// <returns>The <see cref="decimal"/> entered by the user</returns>
        public decimal GetDecimal(string question = null)
        {
            return Get(decimal.Parse, question);
        }

        /// <summary>
        /// Write a question to the <see cref="Output"/>, and then get attempt to get a <see cref="DateTime"/> from the <see cref="Input"/> using <see cref="DateTime.Parse(string)"/>. This process repeats if the received input is invalid. Any errors are written to <see cref="Debug"/>
        /// </summary>
        /// <param name="question">The question to write</param>
        /// <returns>The <see cref="DateTime"/> entered by the user</returns>
        public DateTime GetDateTime(string question = null)
        {
            return Get(DateTime.Parse, question);
        }

        /// <summary>
        /// Write a question to the <see cref="Output"/>, and then get attempt to get a value from the <see cref="Input"/> using <paramref name="converter"/>. If the received input is invalid, this process repeats. Any errors are written to <see cref="Debug"/>
        /// </summary>
        /// <param name="converter">The method to use when converting from the text to the desired type</param>
        /// <param name="question">The question to write</param>
        /// <returns>The <typeparamref name="T"/> entered by the user</returns>
        public virtual T Get<T>(Func<string, T> converter, string question = null)
        {
            if (converter == null)
            {
                throw new ArgumentNullException(nameof(converter));
            }

            T returnValue = default(T);

            {
                bool conversionWorked;
                do
                {
                    if (question != null)
                    {
                        Write(question);
                    }

                    var valueEntered = GetString();

                    try
                    {
                        returnValue = converter(valueEntered);

                        conversionWorked = true;
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex);

                        conversionWorked = false;
                    }
                } while (!conversionWorked);
            }

            return returnValue;
        }
    }
}