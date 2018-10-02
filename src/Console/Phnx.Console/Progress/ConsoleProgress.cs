using System;
using System.Text;
using System.Threading;

namespace Phnx.Console.Progress
{
    /// <summary>
    /// Writes the progress of an operation to the console. If <see cref="System.Console.IsOutputRedirected"/> is <see langword="true"/>, the progress bar manager will print nothing
    /// </summary>
    public class ConsoleProgress : IDisposable
    {
        private readonly ConsoleHelper _console;
        private readonly ProgressBarRenderer _bar;
        private readonly Func<decimal, string> _writeProgressMessage;
        private readonly bool _writeMessageToLeftOfBar;
        private readonly Thread _thread;
        private bool _safeExit;

        /// <summary>
        /// Create a new <see cref="ConsoleProgress"/>
        /// </summary>
        /// <param name="console">The console to output to</param>
        /// <param name="bar">The bar to use when rendering information</param>
        /// <param name="writeProgressMessage">The message to write with the progress indications</param>
        /// <param name="writeMessageToLeftOfBar">Whether to write that message to the left or the right of the progress bar</param>
        internal ConsoleProgress(ConsoleHelper console, ProgressBarRenderer bar, Func<decimal, string> writeProgressMessage, bool writeMessageToLeftOfBar)
        {
            _console = console;
            _bar = bar;
            _writeProgressMessage = writeProgressMessage;
            _writeMessageToLeftOfBar = writeMessageToLeftOfBar;
            _safeExit = false;
            _thread = new Thread(WriterThread);
            _thread.Start();
        }

        /// <summary>
        /// Create and use a new progress bar
        /// </summary>
        /// <param name="maxValue">The highest value that the progress bar can represent</param>
        /// <param name="writeProgressMessage">The text to write with the progress bar. This method must be thread-safe</param>
        /// <param name="writeMessageToLeftOfBar">Whether to write the extra text to the left or right of the progress bar</param>
        /// <returns>A console progress bar which can have its value changed as progress continues</returns>
        public ConsoleProgress(int maxValue, Func<decimal, string> writeProgressMessage, bool writeMessageToLeftOfBar) : this(
            new ConsoleHelper(),
            new ProgressBarRenderer(maxValue),
            writeProgressMessage,
            writeMessageToLeftOfBar)
        {
        }

        /// <summary>
        /// Get or set the current progress
        /// </summary>
        public decimal Progress
        {
            get
            {
                lock (_bar)
                {
                    return _bar.Progress;
                }
            }
            set
            {
                lock (_bar)
                {
                    _bar.Progress = value;
                }
            }
        }

        private bool IsComplete
        {
            get
            {
                lock (_bar)
                {
                    return _bar.IsComplete;
                }
            }
        }

        /// <summary>
        /// Whether an error has occured
        /// </summary>
        public bool IsFaulted
        {
            get
            {
                lock (_bar)
                {
                    return _bar.IsFaulted;
                }
            }
            set
            {
                lock (_bar)
                {
                    _bar.IsFaulted = value;
                }
            }
        }

        private void WriterThread()
        {
            if (System.Console.IsOutputRedirected)
            {
                return;
            }

            string lastWrite = string.Empty;
            string newWrite;

            _console.FontColor = ConsoleColor.Yellow;
            int rendersSinceLastUpdate = 0;

            while (!_safeExit && !IsComplete && !IsFaulted)
            {
                lock (_bar)
                {
                    newWrite = _bar.ToStringWithSpinner(rendersSinceLastUpdate % 20 == 0);
                }

                lastWrite = OverwriteLastWrite(lastWrite, newWrite);

                Thread.Sleep(25);
                ++rendersSinceLastUpdate;
            }

            // Write finished line
            lock (_bar)
            {
                newWrite = _bar.ToString();
            }

            if (IsComplete)
            {
                _console.FontColor = ConsoleColor.Green;
            }
            else if (IsFaulted)
            {
                _console.FontColor = ConsoleColor.Red;
            }

            StringBuilder output = new StringBuilder();
            output.Append('\b', lastWrite.Length);
            output.Append(newWrite);

            if (lastWrite.Length > newWrite.Length)
            {
                output.Append(' ', lastWrite.Length - newWrite.Length);
            }

            _console.WriteLine(output);
            _console.ResetColor();
        }

        private string OverwriteLastWrite(string lastWrite, string newWrite)
        {
            StringBuilder outputBuilder = new StringBuilder();

            if (_writeProgressMessage != null && _writeMessageToLeftOfBar)
            {
                outputBuilder.Append(_writeProgressMessage(Progress));
                outputBuilder.Append(' ');
            }

            outputBuilder.Append(newWrite);

            if (_writeProgressMessage != null && !_writeMessageToLeftOfBar)
            {
                outputBuilder.Append(' ');
                outputBuilder.Append(_writeProgressMessage(Progress));
            }

            int commonStartLength = 0;

            for (; commonStartLength < lastWrite.Length && commonStartLength < outputBuilder.Length; ++commonStartLength)
            {
                if (lastWrite[commonStartLength] != outputBuilder[commonStartLength])
                {
                    break;
                }
            }

            StringBuilder cleanup = new StringBuilder();

            // Remove differing characters
            cleanup.Append('\b', lastWrite.Length - commonStartLength);

            if (outputBuilder.Length < lastWrite.Length)
            {
                outputBuilder.Append(' ', lastWrite.Length - outputBuilder.Length);
            }

            string output = outputBuilder.ToString();
            _console.Write(cleanup.Append(output.Substring(commonStartLength)));

            return output;
        }

        /// <summary>
        /// Sets the progress to complete, and ensures that the completed state is written to the console
        /// </summary>
        public void WriteCompleted()
        {
            lock (_bar)
            {
                _bar.Progress = _bar.MaxValue;
            }

            while (_thread.IsAlive)
            {
                Thread.Sleep(50);
            }
        }

        /// <summary>
        /// Sets the progress to faulted, and ensures that the error state is written to the console
        /// </summary>
        public void WriteError()
        {
            lock (_bar)
            {
                _bar.IsFaulted = true;
            }

            while (_thread.IsAlive)
            {
                Thread.Sleep(50);
            }
        }

        /// <summary>
        /// Exits the progress bar writer, without ensuring that the "complete" progress bar is written
        /// </summary>
        public void Dispose()
        {
            if (!IsComplete)
            {
                WriteError();
            }

            lock (_bar)
            {
                _safeExit = true;
            }

            while (_thread.IsAlive)
            {
                Thread.Sleep(50);
            }
        }
    }
}
