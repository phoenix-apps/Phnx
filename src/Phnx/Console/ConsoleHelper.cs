using Phnx.Console.Progress;
using Phnx.Try;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security;
using System.Text;
using _Console = System.Console;

namespace Phnx.Console
{
    /// <summary>
    /// Provides helper methods for <see cref="System.Console"/> to make reading and writing easier to use, such as getting numbers, printing collections and more
    /// </summary>
    public class ConsoleHelper : TextIoHelper
    {
        /// <summary>
        /// Create a new instance of ConsoleIo
        /// </summary>
        public ConsoleHelper() : base(_Console.In, _Console.Out)
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
            get => _Console.ForegroundColor;
            set => _Console.ForegroundColor = value;
        }

        /// <summary>
        /// Get or set the console background color
        /// </summary>
        /// <exception cref="ArgumentException">The color specified in a set operation is not a valid member of <see cref="ConsoleColor"/></exception>
        /// <exception cref="SecurityException">The user does not have permission to perform this action</exception>
        /// <exception cref="IOException">An I/O error occurred</exception>
        public ConsoleColor BackgroundColor
        {
            get => _Console.BackgroundColor;
            set => _Console.BackgroundColor = value;
        }

        /// <summary>
        /// Get or set the title to display in the console's title bar
        /// </summary>
        /// <exception cref="InvalidOperationException">In a get operation, the retrieved title is longer than 24500 characters</exception>
        /// <exception cref="ArgumentOutOfRangeException">In a set operation, the specified title is longer than 24500 characters</exception>
        /// <exception cref="ArgumentNullException">In a set operation, the specified title is <see langword="null" /></exception>
        /// <exception cref="IOException">An I/O error occurred</exception>
        public string WindowTitle
        {
            get => _Console.Title;
            set => _Console.Title = value;
        }

        /// <summary>
        /// Gets a value that indicates whether output has been redirected from the standard output stream
        /// </summary>
        public bool IsOutputRedirected => _Console.IsOutputRedirected;

        /// <summary>
        /// Resets the console text and background color to their default values
        /// </summary>
        /// <exception cref="SecurityException">The user does not have permission to perform this action</exception>
        /// <exception cref="IOException">An I/O error occurred</exception>
        public void ResetColor()
        {
            _Console.ResetColor();
        }

        /// <summary>
        /// Clear the console
        /// </summary>
        /// <exception cref="IOException">An I/O error occurred</exception>
        public void Clear() => _Console.Clear();

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
        /// Writes an informational message in cyan
        /// </summary>
        /// <param name="info">The information to write</param>
        /// <exception cref="SecurityException">The user does not have permission to perform this action</exception>
        /// <exception cref="IOException">An I/O error occurred</exception>
        public void WriteInfo(string info)
        {
            WriteLine(info, ConsoleColor.Cyan);
        }

        /// <summary>
        /// Writes a warning message in yellow
        /// </summary>
        /// <param name="warning">The warning to write</param>
        /// <exception cref="SecurityException">The user does not have permission to perform this action</exception>
        /// <exception cref="IOException">An I/O error occurred</exception>
        public void WriteWarning(string warning)
        {
            WriteLine(warning, ConsoleColor.Yellow);
        }

        /// <summary>
        /// Writes an error in red
        /// </summary>
        /// <param name="error">The error to write</param>
        /// <exception cref="SecurityException">The user does not have permission to perform this action</exception>
        /// <exception cref="IOException">An I/O error occurred</exception>
        public void WriteError(string error)
        {
            WriteLine(error, ConsoleColor.Red);
        }

        /// <summary>
        /// Writes an error in red
        /// </summary>
        /// <param name="error">The error to write</param>
        /// <exception cref="SecurityException">The user does not have permission to perform this action</exception>
        /// <exception cref="IOException">An I/O error occurred</exception>
        public void WriteError(Exception error)
        {
            WriteError(error.ToString());
        }

        /// <summary>
        /// Writes text in a specified font color
        /// </summary>
        /// <param name="text">The text to write</param>
        /// <param name="fontColor">The font color to use</param>
        /// <exception cref="SecurityException">The user does not have permission to perform this action</exception>
        /// <exception cref="IOException">An I/O error occurred</exception>
        public void Write(string text, ConsoleColor fontColor)
        {
            ConsoleColor startFontColor = FontColor;
            FontColor = fontColor;

            Write(text);

            FontColor = startFontColor;
        }

        /// <summary>
        /// Writes a line of text in a specified font color
        /// </summary>
        /// <param name="text">The text to write</param>
        /// <param name="fontColor">The font color to use</param>
        /// <exception cref="SecurityException">The user does not have permission to perform this action</exception>
        /// <exception cref="IOException">An I/O error occurred</exception>
        public void WriteLine(string text, ConsoleColor fontColor)
        {
            ConsoleColor startFontColor = FontColor;
            FontColor = fontColor;

            WriteLine(text);

            FontColor = startFontColor;
        }

        /// <summary>
        /// Writes the <paramref name="question"/> to the console, and then waits until the user enters <see cref="ConsoleKey.Y"/> or <see cref="ConsoleKey.N"/>
        /// </summary>
        /// <param name="question">The yes or no question to ask the user. " (y/n): " is appended to the question automatically</param>
        /// <returns><see langword="true"/> if the user pressed <see cref="ConsoleKey.Y"/> or <see langword="false"/> if the user pressed <see cref="ConsoleKey.N"/></returns>
        /// <exception cref="InvalidOperationException">The <see cref="System.Console.In"/> property is redirected from some stream other than the console</exception>
        /// <exception cref="IOException">An I/O error occured</exception>
        public bool YesNo(string question)
        {
            Write(question.Trim() + " (y/n): ");

            ConsoleKeyInfo keyInfo;

            do
            {
                keyInfo = ReadKey(false);

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
        /// <exception cref="IOException">An I/O error occured</exception>
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
            return _Console.ReadKey(!showPressedKeyInConsole);
        }

        /// <summary>
        /// Clears previously written lines from the console
        /// </summary>
        /// <param name="lineWritten">The lines that were written to the console, which now need to be cleared</param>
        /// <exception cref="SecurityException">The user does not have permission to perform this action</exception>
        /// <exception cref="IOException">An I/O error occurred</exception>
        public void UndoWriteLine(string lineWritten)
        {
            if (IsOutputRedirected)
            {
                return;
            }

            int linesToEraseCount = GetLinesCoveredByText(lineWritten) + 1;

            _Console.SetCursorPosition(0, _Console.CursorTop - linesToEraseCount);
            _Console.WriteLine(new string(' ', lineWritten.Length));

            _Console.SetCursorPosition(0, _Console.CursorTop - linesToEraseCount);
        }

        /// <summary>
        /// Clears the current line from the console
        /// </summary>
        /// <param name="textWritten">The text that was written to the console</param>
        /// <exception cref="SecurityException">The user does not have permission to perform this action</exception>
        /// <exception cref="IOException">An I/O error occurred</exception>
        public void UndoWrite(string textWritten)
        {
            if (IsOutputRedirected)
            {
                return;
            }

            int linesToEraseCount = GetLinesCoveredByText(textWritten);

            _Console.SetCursorPosition(0, _Console.CursorTop - linesToEraseCount);
            _Console.Write(new string(' ', textWritten.Length));

            _Console.SetCursorPosition(0, _Console.CursorTop - linesToEraseCount);
        }

        private int GetLinesCoveredByText(string text)
        {
            Debug.Assert(!IsOutputRedirected);

            var width = _Console.WindowWidth;
            var eraseCount = text.Length;

            return (int)Math.Truncate((decimal)eraseCount / width);
        }
    }
}
