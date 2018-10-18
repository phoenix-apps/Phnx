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

        private ManualResetEventSlim _workerExited;

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
        /// <exception cref="ArgumentNullException"><paramref name="readFunc"/> is <see langword="null"/></exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="lookAheadCount"/> is less than or equal to zero</exception>
        public ThreadedReader(Func<T> readFunc, int lookAheadCount = 100)
        {
            _sync = new FuncSyncEvent();

            _workerExited = new ManualResetEventSlim();
            _readFunc = readFunc ?? throw new ArgumentNullException(nameof(readFunc));
            _resultQueue = new ConcurrentQueue<ThreadReadResult<T>>();

            if (lookAheadCount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(lookAheadCount));
            }
            LookAheadCount = lookAheadCount;

            ThreadPool.QueueUserWorkItem(t => ReadThreadMethod());
        }

        /// <summary>
        /// Read the next object
        /// </summary>
        /// <returns>The object read from the data source</returns>
        /// <exception cref="ObjectDisposedException">This object was disposed before the read could be completed</exception>
        /// <exception cref="Exception">Reader error</exception>
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
            try
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
            finally
            {
                _workerExited.Set();
            }
        }

        /// <summary>
        /// Cleans up the reading resource thread, finishing any currently active reads, then waits until the reading thread has been exited
        /// </summary>
        public void Dispose()
        {
            _safeExit = true;

            _workerExited.Wait();
        }
    }
}
