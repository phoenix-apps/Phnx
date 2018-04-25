using MarkSFrancis.Console.Progress;
using System;
using System.Diagnostics;

namespace MarkSFrancis.Console
{
    /// <summary>
    /// Creates a wrapper around <see cref="System.Console"/> to make reading and writing easier to use, such as getting numbers, printing collections and more
    /// </summary>
    public class ConsoleIo : TextIo
    {
        /// <summary>
        /// Create a new instance of ConsoleIo
        /// </summary>
        public ConsoleIo() : base(System.Console.In, System.Console.Out)
        {

        }

        /// <summary>
        /// Get or set the output font color
        /// </summary>
        public ConsoleColor FontColor
        {
            get => System.Console.ForegroundColor;
            set => System.Console.ForegroundColor = value;
        }

        /// <summary>
        /// Get or set the console background color
        /// </summary>
        public ConsoleColor BackgroundColor
        {
            get => System.Console.BackgroundColor;
            set => System.Console.BackgroundColor = value;
        }

        /// <summary>
        /// Get or set the title to display in the console's title bar
        /// </summary>
        /// <exception cref="InvalidOperationException">In a get operation, the retrieved title is longer than 24500 characters</exception>
        /// <exception cref="ArgumentOutOfRangeException">In a set operation, the specified title is longer than 24500 characters</exception>
        /// <exception cref="ArgumentNullException">In a set operation, the specified title is <see langword="null" /></exception>
        /// <exception cref="System.IO.IOException">An I/O error occurred</exception>
        public string Title
        {
            get => System.Console.Title;
            set => System.Console.Title = value;
        }

        /// <summary>
        /// Resets the console text and background color to their default values
        /// </summary>
        public void ResetColor()
        {
            System.Console.ResetColor();
        }

        /// <summary>
        /// Clear the console
        /// </summary>
        public void Clear() => System.Console.Clear();

        /// <summary>
        /// Create and use a new progress bar
        /// </summary>
        /// <param name="maxValue">The highest value that the progress bar can represent</param>
        /// <param name="writeProgressMessage">The text to write with the progress bar. This method must be thread-safe</param>
        /// <param name="writeMessageToLeftOfBar">Whether to write the extra text to the left or right of the progress bar</param>
        /// <returns></returns>
        public ConsoleProgress ProgressBar(int maxValue, Func<decimal, string> writeProgressMessage = null, bool writeMessageToLeftOfBar = false)
        {
            var bar = new ConsoleProgressBar(maxValue);

            return new ConsoleProgress(this, bar, writeProgressMessage, writeMessageToLeftOfBar);
        }

        /// <summary>
        /// Write a question to the <see cref="System.Console"/>, and then get attempt to get a value from the <see cref="System.Console"/> using <paramref name="converter"/>. If the received input is invalid, <see cref="Clear"/> is called, and the process repeats. Any errors are written to <see cref="Debug"/>
        /// </summary>
        /// <param name="converter">The method to use when converting from the text to the desired type</param>
        /// <param name="question">The question to write</param>
        /// <returns></returns>
        public override T Get<T>(Func<string, T> converter, string question = null)
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
                        Clear();

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

        /// <summary>
        /// Ask a simple yes or no question from the user, blocking moving on until the user enters <see cref="ConsoleKey.Y"/> or <see cref="ConsoleKey.N"/>
        /// </summary>
        /// <param name="question">The yes or no question to ask the user. " (y/n): " is appended to the question automatically</param>
        /// <returns></returns>
        public bool YesNo(string question)
        {
            Write(question.Trim() + " (y/n): ");

            ConsoleKeyInfo keyInfo;

            do
            {
                keyInfo = ReadKey();

            } while (keyInfo.Key != ConsoleKey.Y && keyInfo.Key != ConsoleKey.N);

            WriteLine(keyInfo.KeyChar);

            return keyInfo.Key == ConsoleKey.Y;
        }

        /// <summary>
        /// Ask a simple yes or no question, which can also be cancelled by pressing <see cref="ConsoleKey.Escape"/>. This method will block until the user presses <see cref="ConsoleKey.Y"/>, <see cref="ConsoleKey.N"/> or <see cref="ConsoleKey.Escape"/>
        /// </summary>
        /// <param name="question">The yes or no question to ask the user. " (y/n/escape): " is appended to the question automatically</param>
        /// <returns></returns>
        public bool? YesNoCancel(string question)
        {
            Write(question.Trim() + " (y/n/escape): ");

            ConsoleKeyInfo keyInfo;

            do
            {
                keyInfo = ReadKey();

            } while (keyInfo.Key != ConsoleKey.Y && keyInfo.Key != ConsoleKey.N &&
                     keyInfo.Key != ConsoleKey.Escape);

            if (keyInfo.Key == ConsoleKey.Escape)
            {
                WriteLine("Cancelled");
                return null;
            }

            WriteLine(keyInfo.KeyChar);

            return keyInfo.Key == ConsoleKey.Y;
        }

        /// <summary>
        /// Waits for a key press from the console, and return the key that is pressed
        /// </summary>
        public ConsoleKeyInfo ReadKey(bool intercept = true)
        {
            return System.Console.ReadKey(intercept);
        }
    }
}