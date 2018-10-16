using System;
using System.Collections.Concurrent;
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

        /// <summary>
        /// The number of <see cref="Write(T)"/> requests currently queued
        /// </summary>
        public int CurrentQueueCount => _writeQueue.Count;

        /// <summary>
        /// The maximum number of items to cache
        /// </summary>
        public int MaximumOutputQueueCount { get; }

        /// <summary>
        /// The most recent writing error. This will be thrown either when the object is disposed, or when the next write is called
        /// </summary>
        private volatile Exception _error;

        private readonly Thread _writeThread;

        /// <summary>
        ///
        /// </summary>
        /// <param name="writeFunc"></param>
        /// <param name="writeQueueCount">The maximum size of the write queue. If this is exceeded, write will be paused whilst waiting for space in the queue to add the new entry</param>
        public ThreadedWriter(Action<T> writeFunc, int writeQueueCount = 100)
        {
            _sync = new FuncSyncEvent();

            _writeFunc = writeFunc ?? throw new ArgumentNullException(nameof(writeFunc));
            _writeQueue = new ConcurrentQueue<T>();
            MaximumOutputQueueCount = writeQueueCount;

            _writeThread = new Thread(WriteThreadMethod);
            _writeThread.Start();
        }

        /// <summary>
        /// Write the object
        /// </summary>
        /// <param name="valueToWrite">The value to write</param>
        public void Write(T valueToWrite)
        {
            // Queue is full - wait for space before adding more entries
            _sync.WaitUntil(() => MaximumOutputQueueCount == 0 || CurrentQueueCount < MaximumOutputQueueCount || _safeExit || _error != null, 1);

            if (_safeExit)
            {
                throw new ObjectDisposedException("Writer", "Writer object disposed before write could be completed");
            }
            else if (_error != null)
            {
                // Exited because of error
                throw _error;
            }

            _writeQueue.Enqueue(valueToWrite);
        }

        private void WriteThreadMethod()
        {
            while (true)
            {
                T objectToWrite = default(T);

                _sync.WaitUntil(() => _writeQueue.TryDequeue(out objectToWrite) || _safeExit, 1);

                if (_safeExit)
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

        /// <summary>
        /// Optionally finishes writing to the output stream any pending output, and then safely exits all background threads
        /// </summary>
        /// <param name="finishWriting">Whether to finish writing to all background threads</param>
        public void Dispose(bool finishWriting)
        {
            if (finishWriting)
            {
                _sync.WaitUntil(() => _writeQueue.Count == 0 || _error != null, 1);
            }

            _safeExit = true;

            _sync.WaitUntil(() => !_writeThread.IsAlive, 1);

            if (_error != null)
            {
                throw _error;
            }
        }

        /// <summary>
        /// Disposes the object, finishing writing to the output first
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }
    }
}
