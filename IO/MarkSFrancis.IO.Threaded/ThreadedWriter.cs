using System;
using System.Collections.Concurrent;
using System.Threading;

namespace MarkSFrancis.IO.Threaded
{
    /// <summary>
    /// A threaded writer which writes behind on a seperate thread, caching records ready for writing
    /// </summary>
    /// <typeparam name="T">The type of data to write</typeparam>
    public class ThreadedWriter<T> : IDisposable
    {
        private readonly Action<T> _writeFunc;
        private readonly ConcurrentQueue<T> _writeQueue;

        /// <summary>
        /// Whether the parent thread has asked to have the write thread exit
        /// </summary>
        private bool _safeExit;

        /// <summary>
        /// The maximum number of items to cache
        /// </summary>
        public int WriteQueueCount { get; }

        /// <summary>
        /// The most recent writing error. This will be thrown either when the object is disposed, or when the next write is called
        /// </summary>
        private Exception _error;

        /// <summary>
        /// The number of milliseconds to sleep if the cache is empty
        /// </summary>
        public int SleepTime { get; }

        private readonly Thread _taskRunner;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writeFunc"></param>
        /// <param name="writeQueueCount">The maximum size of the write queue. If this is exceeded, write will be paused whilst waiting for space in the queue to add the new entry</param>
        /// <param name="sleepTime">How long in ms to sleep if there are no queued tasks</param>
        public ThreadedWriter(Action<T> writeFunc, int writeQueueCount = 100, int sleepTime = 20)
        {
            _writeQueue = new ConcurrentQueue<T>();
            _writeFunc = writeFunc;
            WriteQueueCount = writeQueueCount;
            SleepTime = sleepTime;

            _taskRunner = new Thread(WriteThreadMethod);
            _taskRunner.Start();
        }

        /// <summary>
        /// Write the object
        /// </summary>
        /// <param name="valueToWrite">The value to write</param>
        public void Write(T valueToWrite)
        {
            lock (_error)
            {
                if (_error != null)
                {
                    throw _error;
                }
            }

            while (_writeQueue.Count >= WriteQueueCount)
            {
                Thread.Sleep(SleepTime);

                lock (_error)
                {
                    if (_error != null)
                    {
                        throw _error;
                    }
                }
            }

            lock (_error)
            {
                if (_error != null)
                {
                    throw _error;
                }
            }

            _writeQueue.Enqueue(valueToWrite);
        }

        private void WriteThreadMethod()
        {
            while (!_safeExit)
            {
                bool hasError;
                lock (_error)
                {
                    hasError = _error != null;
                }

                if (!hasError && _writeQueue.Count > 0)
                {
                    // Write
                    _writeQueue.TryDequeue(out var objectToWrite);

                    try
                    {
                        _writeFunc(objectToWrite);
                    }
                    catch (Exception ex)
                    {
                        lock (_error)
                        {
                            _error = ex;
                        }
                    }
                }
                else
                {
                    Thread.Sleep(SleepTime);
                }
            }
        }

        /// <summary>
        /// Optionally finishes writing to the output stream any pending output, and then safely exits all background threads
        /// </summary>
        /// <param name="finishWriting">Whether to finish writing to all background threads</param>
        public void Dispose(bool finishWriting)
        {
            lock (_error)
            {
                if (_error != null)
                {
                    throw _error;
                }
            }

            if (finishWriting)
            {
                while (_writeQueue.Count > 0)
                {
                    lock (_error)
                    {
                        if (_error != null)
                        {
                            throw _error;
                        }
                    }

                    Thread.Sleep(SleepTime);
                }
            }

            _safeExit = true;

            while (_taskRunner.IsAlive)
            {
                Thread.Sleep(SleepTime);

                lock (_error)
                {
                    if (_error != null)
                    {
                        throw _error;
                    }
                }
            }

            lock (_error)
            {
                if (_error != null)
                {
                    throw _error;
                }
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
