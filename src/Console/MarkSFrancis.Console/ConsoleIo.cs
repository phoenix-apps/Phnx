using MarkSFrancis.Console.Progress;
using System;
using System.Diagnostics;
using System.IO;
using System.Security;

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
        /// <exception cref="ArgumentException">The color specified in a set operation is not a valid member of <see cref="ConsoleColor"/></exception>
        /// <exception cref="SecurityException">The user does not have permission to perform this action</exception>
        /// <exception cref="IOException">An I/O error occurred</exception>
        public ConsoleColor FontColor
        {
            get => System.Console.ForegroundColor;
            set => System.Console.ForegroundColor = value;
        }

        /// <summary>
        /// Get or set the console background color
        /// </summary>
        /// <exception cref="ArgumentException">The color specified in a set operation is not a valid member of <see cref="ConsoleColor"/></exception>
        /// <exception cref="SecurityException">The user does not have permission to perform this action</exception>
        /// <exception cref="IOException">An I/O error occurred</exception>
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
        /// <exception cref="IOException">An I/O error occurred</exception>
        public string Title
        {
            get => System.Console.Title;
            set => System.Console.Title = value;
        }

        /// <summary>
        /// Resets the console text and background color to their default values
        /// </summary>
        /// <exception cref="SecurityException">The user does not have permission to perform this action</exception>
        /// <exception cref="IOException">An I/O error occurred</exception>
        public void ResetColor()
        {
            System.Console.ResetColor();
        }

        /// <summary>
        /// Clear the console
        /// </summary>
        /// <exception cref="IOException">An I/O error occurred</exception>
        public void Clear() => System.Console.Clear();

        /// <summary>
        /// Create and use a new progress bar
        /// </summary>
        /// <param name="maxValue">The highest value that the progress bar can represent</param>
        /// <param name="writeProgressMessage">The text to write with the progress bar. This method must be thread-safe</param>
        /// <param name="writeMessageToLeftOfBar">Whether to write the extra text to the left or right of the progress bar</param>
        /// <returns>A console progress bar which can have its value changed as progress continues</returns>
        public ConsoleProgress ProgressBar(int maxValue, Func<decimal, string> writeProgressMessage = null, bool writeMessageToLeftOfBar = false)
        {
            var bar = new ProgressBarRenderer(maxValue);

            return new ConsoleProgress(this, bar, writeProgressMessage, writeMessageToLeftOfBar);
        }

        /// <summary>
        /// Writes the <paramref name="question"/> to the console, and then read a line from the console, and convert it using the <paramref name="converter"/>. If the received input is invalid, <see cref="Clear"/> is called, and the process repeats
        /// </summary>
        /// <param name="converter">The method to use when converting from the text to the desired type</param>
        /// <param name="question">The question to write. If this is <see langword="null"/>, the console is not cleared</param>
        /// <returns>The <typeparamref name="T"/> entered by the user</returns>
        public override T Get<T>(Func<string, T> converter, string question = null)
        {
            if (converter == null)
            {
                throw ErrorFactory.Default.ArgumentNull(nameof(converter));
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
        /// Writes the <paramref name="question"/> to the console, and then waits until the user enters <see cref="ConsoleKey.Y"/> or <see cref="ConsoleKey.N"/>
        /// </summary>
        /// <param name="question">The yes or no question to ask the user. " (y/n): " is appended to the question automatically</param>
        /// <returns><see langword="true"/> if the user pressed <see cref="ConsoleKey.Y"/> or <see langword="false"/> if the user pressed <see cref="ConsoleKey.N"/></returns>
        /// <exception cref="InvalidOperationException">The <see cref="System.Console.In"/> property is redirected from some stream other than the console</exception>
        /// <exception cref="IOException">An I/O error occurs</exception>
        public bool YesNo(string question)
        {
            Write(question.Trim() + " (y/n): ");

            ConsoleKeyInfo keyInfo;

            do
            {
                keyInfo = ReadKey(true);

            } while (keyInfo.Key != ConsoleKey.Y && keyInfo.Key != ConsoleKey.N);

            WriteLine(keyInfo.KeyChar);

            return keyInfo.Key == ConsoleKey.Y;
        }

        /// <summary>
        /// Writes the <paramref name="question"/> to the console, and then waits until the user presses <see cref="ConsoleKey.Y"/>, <see cref="ConsoleKey.N"/> or <see cref="ConsoleKey.Escape"/>
        /// </summary>
        /// <param name="question">The yes or no question to ask the user. " (y/n/escape): " is appended to the question automatically</param>
        /// <returns><see langword="true"/> if the user pressed <see cref="ConsoleKey.Y"/>, <see langword="false"/> if the user pressed <see cref="ConsoleKey.N"/>, and <see langword="null"/> if the user pressed <see cref="ConsoleKey.Escape"/></returns>
        /// <exception cref="InvalidOperationException">The <see cref="System.Console.In"/> property is redirected from some stream other than the console</exception>
        /// <exception cref="IOException">An I/O error occurs</exception>
        public bool? YesNoCancel(string question)
        {
            Write(question.Trim() + " (y/n/escape): ");

            ConsoleKeyInfo keyInfo;

            do
            {
                keyInfo = ReadKey(false);

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
        /// <param name="showPressedKeyInConsole">Determines whether to display the pressed key in the console window</param>
        /// <returns>An object that describes the <see cref="ConsoleKey"/> constant and Unicode character, if any, that correspond to the pressed console key. The <see cref="ConsoleKeyInfo"/> object also describes, in a bitwise combination of <see cref="ConsoleModifiers"/> values, whether one or more <see cref="ConsoleModifiers.Shift"/>, <see cref="ConsoleModifiers.Alt"/>, or <see cref="ConsoleModifiers.Control"/> modifier keys was pressed simultaneously with the console key</returns>
        /// <exception cref="InvalidOperationException">The <see cref="System.Console.In"/> property is redirected from some stream other than the console</exception>
        public ConsoleKeyInfo ReadKey(bool showPressedKeyInConsole = true)
        {
            return System.Console.ReadKey(!showPressedKeyInConsole);
        }
    }
}
