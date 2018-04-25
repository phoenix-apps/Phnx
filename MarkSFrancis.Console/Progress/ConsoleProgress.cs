using System;
using System.Text;
using System.Threading;

namespace MarkSFrancis.Console.Progress
{
    /// <summary>
    /// Writes the progress of an operation to the console. If <see cref="System.Console.IsOutputRedirected"/> is <see langword="true"/>, the progress bar manager will print nothing
    /// </summary>
    public class ConsoleProgress : IDisposable
    {
        private readonly ConsoleIo _console;
        private readonly ConsoleProgressBar _bar;
        private readonly Func<decimal, string> _writeProgressMessage;
        private readonly bool _writeMessageToLeftOfBar;
        private readonly Thread _thread;
        private bool _safeExit;

        /// <summary>
        /// Create a new <see cref="ConsoleProgress"/>
        /// </summary>
        /// <param name="console">The console to output to</param>
        /// <param name="bar"></param>
        /// <param name="writeProgressMessage"></param>
        /// <param name="writeMessageToLeftOfBar"></param>
        internal ConsoleProgress(ConsoleIo console, ConsoleProgressBar bar, Func<decimal, string> writeProgressMessage, bool writeMessageToLeftOfBar)
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

        private void WriterThread()
        {
            if (System.Console.IsOutputRedirected)
            {
                return;
            }

            string lastWrite = string.Empty;
            string newWrite;

            while (!_safeExit && !IsComplete)
            {
                lock (_bar)
                {
                    newWrite = _bar.ToString();
                }

                lastWrite = OverwriteLastWrite(lastWrite, newWrite);

                Thread.Sleep(250);
            }

            if (_safeExit)
            {
                return;
            }

            // Write finished line
            lock (_bar)
            {
                newWrite = _bar.ToString(false);
            }

            OverwriteLastWrite(lastWrite, newWrite);
            _console.WriteLine();
        }

        private string OverwriteLastWrite(string lastWrite, string newWrite)
        {
            StringBuilder outputBuilder = new StringBuilder();

            if (_writeProgressMessage != null && _writeMessageToLeftOfBar)
            {
                outputBuilder.Append(_writeProgressMessage(Progress));
            }

            outputBuilder.Append(newWrite);

            if (_writeProgressMessage != null && !_writeMessageToLeftOfBar)
            {
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
        /// Exits the progress bar writer, without ensuring that the "complete" progress bar is written
        /// </summary>
        public void Dispose()
        {
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
