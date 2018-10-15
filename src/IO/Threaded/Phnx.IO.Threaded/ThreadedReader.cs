using System;
using System.Collections.Concurrent;
using System.Threading;

namespace Phnx.IO.Threaded
{
    /// <summary>
    /// A threaded reader which reads ahead on a seperate thread, caching records ready for consumption
    /// </summary>
    /// <typeparam name="T">The type of data to read</typeparam>
    public class ThreadedReader<T> : IDisposable
    {
        private readonly Func<T> _readFunc;
        private readonly ConcurrentQueue<ThreadReadResult<T>> _resultQueue;

        /// <summary>
        /// The number of items currently cached
        /// </summary>
        public int CachedCount => _resultQueue.Count;

        /// <summary>
        /// Set after an item is read
        /// </summary>
        private readonly ManualResetEventSlim _itemReadEvent;

        /// <summary>
        /// Set when an item should be read
        /// </summary>
        private readonly ManualResetEventSlim _readRequestEvent;

        /// <summary>
        /// The maximum number of items to cache
        /// </summary>
        public int LookAheadCount { get; }

        /// <summary>
        /// Whether an error has occured with the reading thread
        /// </summary>
        private bool _errorOccured;

        /// <summary>
        /// Whether the parent thread has asked to have the read thread exit
        /// </summary>
        private bool _safeExit;

        private readonly Thread _readThread;

        /// <summary>
        ///
        /// </summary>
        /// <param name="readFunc"></param>
        /// <param name="lookAheadCount">The maximum number of values to cache</param>
        public ThreadedReader(Func<T> readFunc, int lookAheadCount = 100)
        {
            _itemReadEvent = new ManualResetEventSlim(false);
            _readRequestEvent = new ManualResetEventSlim(true);

            _readFunc = readFunc ?? throw new ArgumentNullException(nameof(readFunc));
            _resultQueue = new ConcurrentQueue<ThreadReadResult<T>>();
            LookAheadCount = lookAheadCount;

            _readThread = new Thread(ReadThreadMethod);
            _readThread.Start();
        }

        /// <summary>
        /// Read the next object
        /// </summary>
        /// <returns>The object read from the data source</returns>
        public T Read()
        {
            _readRequestEvent.Set();

            ThreadReadResult<T> returnT;
            if (_resultQueue.Count > 0)
            {
                _resultQueue.TryDequeue(out returnT);
            }
            else
            {
                // Wait for read
                while (!_resultQueue.TryDequeue(out returnT))
                {
                    // Wait for signal
                    _itemReadEvent.Wait();
                    _itemReadEvent.Reset();

                    if (_safeExit)
                    {
                        throw new ObjectDisposedException("Read object disposed before read could be completed");
                    }
                }
            }

            if (returnT.ErrorOccured)
            {
                throw returnT.Error;
            }

            return returnT.Data;
        }

        private void ReadThreadMethod()
        {
            while (true)
            {
                _readRequestEvent.Wait();
                if (_safeExit)
                {
                    return;
                }

                if (CachedCount == LookAheadCount)
                {
                    _readRequestEvent.Reset();
                }

                try
                {
                    T data = _readFunc();
                    _resultQueue.Enqueue(new ThreadReadResult<T>(data));
                }
                catch (Exception ex)
                {
                    _errorOccured = true;
                    _resultQueue.Enqueue(new ThreadReadResult<T>(ex));
                }

                _itemReadEvent.Set();
            }
        }

        /// <summary>
        /// Cleans up the reading resource thread, finishing any currently active reads, then safely exits the reading thread
        /// </summary>
        public void Dispose()
        {
            _safeExit = true;

            _itemReadEvent.Set();
            _readRequestEvent.Set();

            while (_readThread.IsAlive)
            {
                Thread.Sleep(10);
            }
        }
    }
}