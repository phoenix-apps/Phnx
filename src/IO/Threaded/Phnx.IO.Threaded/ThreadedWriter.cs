using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading;

namespace Phnx.IO.Threaded
{
    /// <summary>
    /// A threaded writer which writes behind on a seperate thread, caching records ready for writing
    /// </summary>
    /// <typeparam name="T">The type of data to write</typeparam>
    public class ThreadedWriter<T> : IDisposable
    {
        private readonly FuncSyncEvent _sync;
        private readonly Action<T> _writeFunc;
        private readonly ConcurrentQueue<T> _writeQueue;

        /// <summary>
        /// Whether the parent thread has asked to have the write thread exit
        /// </summary>
        private volatile bool _safeExit;

        private ManualResetEventSlim _workerExited;

        /// <summary>
        /// The number of <see cref="Write(T)"/> requests currently queued
        /// </summary>
        public int CurrentQueueCount => _writeQueue.Count;

        /// <summary>
        /// The maximum number of items to cache
        /// </summary>
        public int MaximumQueueCount { get; }

        /// <summary>
        /// The most recent writing error. This will be thrown either when the object is disposed, or when the next write is called
        /// </summary>
        private volatile Exception _error;

        /// <summary>
        /// Create a new <see cref="ThreadedWriter{T}"/>
        /// </summary>
        /// <param name="writeFunc">The function to use when writing data. This is ran from a different thread to the one that called <see cref="Write(T)"/> requests</param>
        public ThreadedWriter(Action<T> writeFunc) : this(writeFunc, 0)
        {
        }

        /// <summary>
        /// Create a new <see cref="ThreadedWriter{T}"/> with a maximum queue size
        /// </summary>
        /// <param name="writeFunc">The function to use when writing data. This is ran from a different thread to the one that called <see cref="Write(T)"/> requests</param>
        /// <param name="maximumQueueCount">The maximum size of the write queue. If this is exceeded, write will be paused whilst waiting for space in the queue to add the new entry. Set this to 0 to have no limit</param>
        /// <exception cref="ArgumentNullException"><paramref name="writeFunc"/> is <see langword="null"/></exception>
        /// <exception cref="ArgumentLessThanZeroException"><paramref name="maximumQueueCount"/> is less than zero</exception>
        public ThreadedWriter(Action<T> writeFunc, int maximumQueueCount)
        {
            _sync = new FuncSyncEvent();

            _workerExited = new ManualResetEventSlim();
            _writeFunc = writeFunc ?? throw new ArgumentNullException(nameof(writeFunc));
            _writeQueue = new ConcurrentQueue<T>();

            if (maximumQueueCount < 0)
            {
                throw new ArgumentLessThanZeroException(nameof(maximumQueueCount));
            }
            MaximumQueueCount = maximumQueueCount;

            ThreadPool.QueueUserWorkItem(t => WriteThreadMethod());
        }

        /// <summary>
        /// Write the object
        /// </summary>
        /// <param name="valueToWrite">The value to write</param>
        /// <exception cref="ObjectDisposedException">This object was disposed before the write could be queued</exception>
        /// <exception cref="IOException">Writer error</exception>
        public void Write(T valueToWrite)
        {
            // Queue is full - wait for space before adding more entries
            _sync.WaitUntil(() => MaximumQueueCount == 0 || CurrentQueueCount < MaximumQueueCount || _safeExit || _error != null, 1);

            if (_safeExit)
            {
                throw new ObjectDisposedException("Writer", "Writer object disposed before write could be completed");
            }
            else if (_error != null)
            {
                // Exited because of error
                throw new IOException("An exception occured when writing", _error);
            }

            _writeQueue.Enqueue(valueToWrite);
        }

        private void WriteThreadMethod()
        {
            try
            {
                while (true)
                {
                    T objectToWrite = default(T);
                    bool itemFound = false;

                    _sync.WaitUntil(() => (itemFound = _writeQueue.TryDequeue(out objectToWrite)) || _safeExit, 1);

                    if (!itemFound && _safeExit)
                    {
                        return;
                    }

                    try
                    {
                        _writeFunc(objectToWrite);
                    }
                    catch (Exception ex)
                    {
                        _error = ex;
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
        /// Diposes this object, ensuring that the writer thread has safely exited. Optionally finishes writing to the output stream any pending output
        /// </summary>
        /// <param name="finishWriting">Whether to finish writing to all background threads</param>
        /// <exception cref="IOException">A writing error occured with one of the queued items</exception>
        public void Dispose(bool finishWriting)
        {
            if (finishWriting)
            {
                _sync.WaitUntil(() => _writeQueue.Count == 0 || _error != null, 1);
            }

            _safeExit = true;

            _workerExited.Wait();

            if (_error != null)
            {
                throw new IOException("An exception occured when writing", _error);
            }
        }

        /// <summary>
        /// Diposes this object, ensuring that the writer thread has safely exited. Also finishes writing to the output stream any pending output
        /// </summary>
        /// <exception cref="Exception">A writing error occured with one of the queued items</exception>
        public void Dispose()
        {
            Dispose(true);
        }
    }
}
