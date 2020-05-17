using Phnx.Try;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using _Console = System.Console;

namespace Phnx.Console
{
    /// <summary>
    /// Provides extensions for getting values from the console
    /// </summary>
    public static class GetExtensions
    {
        /// <summary>
        /// Gets an <see cref="int"/> from the user
        /// </summary>
        /// <param name="helper">The console to use</param>
        /// <returns>The <see cref="int"/> entered by the user</returns>
        /// <exception cref="ArgumentNullException"><paramref name="helper"/> is <see langword="null"/></exception>
        /// <exception cref="IOException">An I/O error occured</exception>
        /// <exception cref="ObjectDisposedException"><see cref="TextIoHelper.Output"/> or <see cref="TextIoHelper.Input"/> is closed</exception>
        /// <exception cref="OutOfMemoryException">There is insufficient memory to allocate a buffer for the line read from <see cref="TextIoHelper.Input"/></exception>
        /// <exception cref="ArgumentOutOfRangeException">The number of characters in the next line is greater than <see cref="int.MaxValue"/></exception>
        /// <exception cref="EndOfStreamException">Reached the end of <see cref="TextIoHelper.Input"/></exception>
        public static int GetInt(this ConsoleHelper helper)
        {
            TryParseDelegateExtensions.TryParse<string, int> act = int.TryParse;

            return Get(helper, act.ToTryResult("Please enter a valid integer"));
        }

        /// <summary>
        /// Gets a <see cref="long"/> from the user
        /// </summary>
        /// <param name="helper">The console to use</param>
        /// <returns>The <see cref="long"/> entered by the user</returns>
        /// <exception cref="ArgumentNullException"><paramref name="helper"/> is <see langword="null"/></exception>
        /// <exception cref="IOException">An I/O error occured</exception>
        /// <exception cref="ObjectDisposedException"><see cref="TextIoHelper.Output"/> or <see cref="TextIoHelper.Input"/> is closed</exception>
        /// <exception cref="OutOfMemoryException">There is insufficient memory to allocate a buffer for the line read from <see cref="TextIoHelper.Input"/></exception>
        /// <exception cref="ArgumentOutOfRangeException">The number of characters in the next line is greater than <see cref="int.MaxValue"/></exception>
        /// <exception cref="EndOfStreamException">Reached the end of <see cref="TextIoHelper.Input"/></exception>
        public static long GetLong(this ConsoleHelper helper)
        {
            TryParseDelegateExtensions.TryParse<string, long> act = long.TryParse;

            return Get(helper, act.ToTryResult("Please enter a valid integer"));
        }

        /// <summary>
        /// Get a <see cref="decimal"/> from the user
        /// </summary>
        /// <param name="helper">The console to use</param>
        /// <returns>The <see cref="decimal"/> entered by the user</returns>
        /// <exception cref="ArgumentNullException"><paramref name="helper"/> is <see langword="null"/></exception>
        /// <exception cref="IOException">An I/O error occured</exception>
        /// <exception cref="ObjectDisposedException"><see cref="TextIoHelper.Output"/> or <see cref="TextIoHelper.Input"/> is closed</exception>
        /// <exception cref="OutOfMemoryException">There is insufficient memory to allocate a buffer for the line read from <see cref="TextIoHelper.Input"/></exception>
        /// <exception cref="ArgumentOutOfRangeException">The number of characters in the next line is greater than <see cref="int.MaxValue"/></exception>
        /// <exception cref="EndOfStreamException">Reached the end of <see cref="TextIoHelper.Input"/></exception>
        public static decimal GetDecimal(this ConsoleHelper helper)
        {
            TryParseDelegateExtensions.TryParse<string, decimal> act = decimal.TryParse;

            return Get(helper, act.ToTryResult("Please enter a valid number"));
        }

        /// <summary>
        /// Get a <see cref="double"/> from the user
        /// </summary>
        /// <param name="helper">The console to use</param>
        /// <returns>The <see cref="double"/> entered by the user</returns>
        /// <exception cref="ArgumentNullException"><paramref name="helper"/> is <see langword="null"/></exception>
        /// <exception cref="IOException">An I/O error occured</exception>
        /// <exception cref="ObjectDisposedException"><see cref="TextIoHelper.Output"/> or <see cref="TextIoHelper.Input"/> is closed</exception>
        /// <exception cref="OutOfMemoryException">There is insufficient memory to allocate a buffer for the line read from <see cref="TextIoHelper.Input"/></exception>
        /// <exception cref="ArgumentOutOfRangeException">The number of characters in the next line is greater than <see cref="int.MaxValue"/></exception>
        /// <exception cref="EndOfStreamException">Reached the end of <see cref="TextIoHelper.Input"/></exception>
        public static double GetDouble(this ConsoleHelper helper)
        {
            TryParseDelegateExtensions.TryParse<string, double> act = double.TryParse;

            return Get(helper, act.ToTryResult("Please enter a valid number"));
        }

        /// <summary>
        /// Write a question and a range of options to the console, and asks the user to select one
        /// </summary>
        /// <param name="helper">The console to use</param>
        /// <param name="options">The range of options to choose from</param>
        /// <returns>The index of the selected option</returns>
        /// <exception cref="ArgumentNullException"><paramref name="helper"/> or <paramref name="options"/> is <see langword="null"/></exception>
        /// <exception cref="InvalidOperationException"><paramref name="options"/> is empty</exception>
        /// <exception cref="IOException">An I/O error occured</exception>
        /// <exception cref="ObjectDisposedException"><see cref="TextIoHelper.Output"/> or <see cref="TextIoHelper.Input"/> is closed</exception>
        /// <exception cref="OutOfMemoryException">There is insufficient memory to allocate a buffer for the line read from <see cref="TextIoHelper.Input"/></exception>
        /// <exception cref="ArgumentOutOfRangeException">The number of characters in the next line is greater than <see cref="int.MaxValue"/></exception>
        /// <exception cref="EndOfStreamException">Reached the end of <see cref="TextIoHelper.Input"/></exception>
        /// <exception cref="InvalidCastException">An error occurred converting the input to a <see cref="int"/></exception>
        public static int GetSelection(this ConsoleHelper helper, IEnumerable<string> options)
        {
            if (helper is null)
            {
                throw new ArgumentNullException(nameof(helper));
            }

            if (options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            var formattedOptions = new StringBuilder();
            var optionCount = 0;

            foreach (var option in options)
            {
                formattedOptions.AppendLine(++optionCount + ": " + option);
            }

            if (optionCount == 0)
            {
                throw new InvalidOperationException(nameof(options));
            }

            helper.WriteLine(formattedOptions.ToString());

            TryResult<int> converter(string enteredValue)
            {
                var error = $"Please enter an integer between 1 and {optionCount}";
                if (!int.TryParse(enteredValue, out var valueInt))
                {
                    return error;
                }

                if (valueInt <= 0 || valueInt > optionCount)
                {
                    return error;
                }

                return valueInt - 1;
            }

            return Get(helper, converter);
        }

        /// <summary>
        /// Reads a line of input from the console, and converts it. If the received input is invalid, the error is shown, and the process repeats until valid input is given
        /// </summary>
        /// <param name="helper">The console to use</param>
        /// <param name="converter">The method to use when converting from the text to the desired type</param>
        /// <returns>The <typeparamref name="T"/> entered by the user</returns>
        /// <exception cref="ArgumentNullException"><paramref name="helper"/> or <paramref name="converter"/> is <see langword="null"/></exception>
        /// <exception cref="IOException">An I/O exception occured</exception>
        /// <exception cref="OutOfMemoryException">There is insufficient memory to allocate a buffer for the line read from <see cref="TextIoHelper.Input"/></exception>
        /// <exception cref="ArgumentOutOfRangeException">The number of characters in the next line is larger than <see cref="int.MaxValue"/></exception>
        public static T Get<T>(this ConsoleHelper helper, Func<string, TryResult<T>> converter)
        {
            if (helper is null)
            {
                throw new ArgumentNullException(nameof(helper));
            }

            if (converter is null)
            {
                throw new ArgumentNullException(nameof(converter));
            }

            string errorWritten = null;
            string valueEntered;
            if (_Console.CursorLeft != 0)
            {
                helper.NewLine();
            }

            void Reset()
            {
                helper.UndoWriteLine(valueEntered);

                if (errorWritten != null)
                {
                    helper.UndoWriteLine(errorWritten);
                }
            }

            do
            {
                var originalColor = helper.FontColor;
                helper.FontColor = ConsoleColor.Cyan;
                valueEntered = helper.ReadLine();
                helper.FontColor = originalColor;

                var converted = converter(valueEntered);

                if (converted)
                {
                    if (errorWritten != null)
                    {
                        Reset();
                        helper.WriteLine(valueEntered, ConsoleColor.Cyan);
                    }

                    return converted.Result;
                }

                Reset();
                helper.WriteError(errorWritten = converted.ErrorMessage);
            } while (true);
        }
    }
}
