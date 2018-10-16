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
        private readonly FuncSyncEvent _sync;

        /// <summary>
        /// Whether the parent thread has asked to have the read thread exit
        /// </summary>
        private volatile bool _safeExit;

        private readonly Thread _readThread;

        /// <summary>
        /// The number of items currently cached
        /// </summary>
        public int CachedCount => _resultQueue.Count;

        /// <summary>
        /// The maximum number of items to cache
        /// </summary>
        public int LookAheadCount { get; }

        /// <summary>
        /// Create a new <see cref="ThreadedReader{T}"/>
        /// </summary>
        /// <param name="readFunc">The function to use when loading resources from an external source</param>
        /// <param name="lookAheadCount">The maximum number of values to cache</param>
        public ThreadedReader(Func<T> readFunc, int lookAheadCount = 100)
        {
            if (lookAheadCount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(lookAheadCount));
            }

            _sync = new FuncSyncEvent();

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
            ThreadReadResult<T> returnT = null;
            // Wait for read
            _sync.WaitUntil(() => _resultQueue.TryDequeue(out returnT) || _safeExit, 1);

            if (_safeExit)
            {
                throw new ObjectDisposedException("Reader", "Read object disposed before read could be completed");
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
                _sync.WaitUntil(() => CachedCount < LookAheadCount || _safeExit, 1);

                if (_safeExit)
                {
                    return;
                }

                try
                {
                    T data = _readFunc();
                    _resultQueue.Enqueue(new ThreadReadResult<T>(data));
                }
                catch (Exception ex)
                {
                    _resultQueue.Enqueue(new ThreadReadResult<T>(ex));
                    return;
                }
            }
        }

        /// <summary>
        /// Cleans up the reading resource thread, finishing any currently active reads, then safely exits the reading thread
        /// </summary>
        public void Dispose()
        {
            _safeExit = true;

            while (_readThread.IsAlive)
            {
                Thread.Sleep(1);
            }
        }
    }
}