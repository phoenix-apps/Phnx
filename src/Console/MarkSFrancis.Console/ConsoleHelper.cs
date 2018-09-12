using MarkSFrancis.Console.Progress;
using System;
using System.IO;
using System.Security;
using _Console = System.Console;

namespace MarkSFrancis.Console
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
            WriteLineInColor(info, ConsoleColor.Cyan);
        }

        /// <summary>
        /// Writes a warning message in yellow
        /// </summary>
        /// <param name="warning">The warning to write</param>
        /// <exception cref="SecurityException">The user does not have permission to perform this action</exception>
        /// <exception cref="IOException">An I/O error occurred</exception>
        public void WriteWarning(string warning)
        {
            WriteLineInColor(warning, ConsoleColor.Yellow);
        }

        /// <summary>
        /// Writes an error in red
        /// </summary>
        /// <param name="error">The error to write</param>
        /// <exception cref="SecurityException">The user does not have permission to perform this action</exception>
        /// <exception cref="IOException">An I/O error occurred</exception>
        public void WriteError(string error)
        {
            WriteLineInColor(error, ConsoleColor.Red);
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
        /// Writes a line of text in a specified font and/or background color
        /// </summary>
        /// <param name="text">The text to write</param>
        /// <param name="fontColor">The font color to use</param>
        /// <param name="backgroundColor">The background color to use</param>
        /// <exception cref="SecurityException">The user does not have permission to perform this action</exception>
        /// <exception cref="IOException">An I/O error occurred</exception>
        public void WriteLineInColor(string text, ConsoleColor? fontColor = null, ConsoleColor? backgroundColor = null)
        {
            ConsoleColor? startFontColor = null, startBackgroundColor = null;

            if (fontColor != null)
            {
                startFontColor = FontColor;
                FontColor = fontColor.Value;
            }

            if (backgroundColor != null)
            {
                startBackgroundColor = BackgroundColor;
                BackgroundColor = backgroundColor.Value;
            }

            WriteLine(text);

            if (fontColor != null)
            {
                FontColor = startFontColor.Value;
            }

            if (backgroundColor != null)
            {
                BackgroundColor = startBackgroundColor.Value;
            }
        }

        /// <summary>
        /// Writes the <paramref name="question"/> to the console, and then read a line from the console, and convert it using the <paramref name="converter"/>. If the received input is invalid, <see cref="Clear"/> is called, and the process repeats
        /// </summary>
        /// <param name="converter">The method to use when converting from the text to the desired type</param>
        /// <param name="question">The question to write. If this is <see langword="null"/>, the console is not cleared</param>
        /// <returns>The <typeparamref name="T"/> entered by the user</returns>
        /// <exception cref="IOException">An I/O exception occured</exception>
        /// <exception cref="OutOfMemoryException">There is insufficient memory to allocate a buffer for the line read from <see cref="TextIoHelper.Input"/></exception>
        /// <exception cref="ArgumentOutOfRangeException">The number of characters in the next line is larger than <see cref="int.MaxValue"/></exception>
        public override T Get<T>(Func<string, T> converter, string question = null)
        {
            if (converter == null)
            {
                throw ErrorFactory.Default.ArgumentNull(nameof(converter));
            }

            if (question != null)
            {
                question = question.Trim() + " ";
                Write(question);
            }

            string errorWritten = null;

            do
            {
                var valueEntered = GetString();

                try
                {
                    return converter(valueEntered);
                }
                catch (Exception ex)
                {
                    UndoWriteLine(question + valueEntered);
                    if (errorWritten != null)
                    {
                        UndoWriteLine(errorWritten);
                    }

                    WriteError(ex.Message);
                    errorWritten = ex.Message;

                    if (question != null)
                    {
                        Write(question);
                    }
                }
            } while (true);
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
        /// Clears previous lines from the console. This is faster than <see cref="UndoWriteLine(int)"/>, but requires you to know what text was written
        /// </summary>
        /// <param name="linesWritten">The lines that were written to the console to be cleared</param>
        /// <exception cref="SecurityException">The user does not have permission to perform this action</exception>
        /// <exception cref="IOException">An I/O error occurred</exception>
        public void UndoWriteLine(params string[] linesWritten)
        {
            for (int lineIndex = linesWritten.Length - 1; lineIndex >= 0; --lineIndex)
            {
                _Console.SetCursorPosition(0, _Console.CursorTop - 1);
                ClearCurrentLine(linesWritten[lineIndex]);
            }
        }

        /// <summary>
        /// Clears previous lines from the console
        /// </summary>
        /// <param name="linesToClear">The number of lines to clear</param>
        /// <exception cref="SecurityException">The user does not have permission to perform this action</exception>
        /// <exception cref="IOException">An I/O error occurred</exception>
        public void UndoWriteLine(int linesToClear)
        {
            for (int linesCleared = 0; linesCleared < linesToClear; ++linesCleared)
            {
                _Console.SetCursorPosition(0, _Console.CursorTop - 1);
                ClearCurrentLine();
            }
        }

        /// <summary>
        /// Clears the current line from the console. This is faster than <see cref="ClearCurrentLine()"/>, but requires you to know what text was written
        /// </summary>
        /// <param name="textWritten">The text that was written to the console</param>
        /// <exception cref="SecurityException">The user does not have permission to perform this action</exception>
        /// <exception cref="IOException">An I/O error occurred</exception>
        public void ClearCurrentLine(string textWritten)
        {
            var width = _Console.BufferWidth;

            int charactersToErase = textWritten.Length;

            int numberOfLinesToErase = (int)Math.Truncate((decimal)charactersToErase / width) + 1;

            _Console.SetCursorPosition(0, _Console.CursorTop - (numberOfLinesToErase - 1));
            for (int linesErased = 0; linesErased < numberOfLinesToErase - 1; ++linesErased)
            {
                Write(new string(' ', width));
            }

            if (charactersToErase % width == 0)
            {
                Write(new string(' ', width));
                _Console.SetCursorPosition(0, _Console.CursorTop - numberOfLinesToErase);
            }
            else
            {
                Write(new string(' ', charactersToErase % width));
                _Console.SetCursorPosition(0, _Console.CursorTop - (numberOfLinesToErase - 1));
            }
        }

        /// <summary>
        /// Clears the current line from the console
        /// </summary>
        /// <exception cref="SecurityException">The user does not have permission to perform this action</exception>
        /// <exception cref="IOException">An I/O error occurred</exception>
        public void ClearCurrentLine()
        {
            var width = _Console.BufferWidth;

            _Console.SetCursorPosition(0, _Console.CursorTop);

            _Console.Write(new string(' ', width));

            _Console.SetCursorPosition(0, _Console.CursorTop - 1);
        }
    }
}
