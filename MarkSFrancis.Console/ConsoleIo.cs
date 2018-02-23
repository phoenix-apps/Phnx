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
        /// Clear the console
        /// </summary>
        public void Clear() => System.Console.Clear();

        /// <summary>
        /// Write a message to the <see cref="System.Console"/>, and then get attempt to get a value from the <see cref="System.Console"/> using the given converter. If the received input is invalid, <see cref="Clear"/> is called, and the process repeats. Any errors are written to <see cref="Debug"/>
        /// </summary>
        /// <param name="converter">The method to use when converting from the text to the desired type</param>
        /// <param name="message">The message to write</param>
        /// <returns></returns>
        public override T Get<T>(Func<string, T> converter, string message = null)
        {
            if (converter == null)
            {
                throw new ArgumentNullException(nameof(converter));
            }

            message = FormatQuestion(message);

            T returnValue = default(T);

            {
                bool conversionWorked;
                do
                {
                    if (message != null)
                    {
                        Clear();

                        Write(message);
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
            Write(question + " (y/n): ");

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
            Write(question + " (y/n/escape): ");

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