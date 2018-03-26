using System;
using System.Collections.Concurrent;
using System.Threading;

namespace MarkSFrancis.IO.Threaded
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
        /// Whether to read the next value
        /// </summary>
        private bool ReadNext => _manualReadNext > 0 ||
                                 (!_errorOccured && LookAheadCount > _resultQueue.Count);

        /// <summary>
        /// The number of times that a manual read has been requested. This only happens if the user has emptied the cache. Lock <see cref="_manualReadNextSyncContext"/> before accessing this member
        /// </summary>
        private int _manualReadNext;

        /// <summary>
        /// The sync context for <see cref="_manualReadNext"/>
        /// </summary>
        private readonly object _manualReadNextSyncContext;

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

        /// <summary>
        /// The number of milliseconds to sleep if the cache is full, or if the main thread is waiting for an entry in the cache
        /// </summary>
        public int SleepTime { get; }

        private readonly Thread _readThread;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="readFunc"></param>
        /// <param name="lookAheadCount">The maximum number of values to cache</param>
        /// <param name="sleepTime">How long in ms to sleep if the look ahead is full, and no tasks are waiting</param>
        public ThreadedReader(Func<T> readFunc, int lookAheadCount = 100, int sleepTime = 20)
        {
            _manualReadNextSyncContext = null;

            _readFunc = readFunc;
            _resultQueue = new ConcurrentQueue<ThreadReadResult<T>>();
            LookAheadCount = lookAheadCount;
            SleepTime = sleepTime;

            _readThread = new Thread(ReadThreadMethod);
            _readThread.Start();
        }

        /// <summary>
        /// Read the next object
        /// </summary>
        /// <returns>The object read from the data source</returns>
        public T Read()
        {
            ThreadReadResult<T> returnT;
            if (_resultQueue.Count > 0)
            {
                _resultQueue.TryDequeue(out returnT);
            }
            else
            {
                lock (_manualReadNextSyncContext)
                {
                    ++_manualReadNext;
                }

                // Wait for read
                while (_resultQueue.Count == 0)
                {
                    Thread.Sleep(SleepTime);
                }

                _resultQueue.TryDequeue(out returnT);
            }

            if (returnT.Faulted)
            {
                throw returnT.Error;
            }

            return returnT.Data;
        }

        private void ReadThreadMethod()
        {
            while (!_safeExit)
            {
                if (ReadNext)
                {
                    lock (_manualReadNextSyncContext)
                    {
                        if (_manualReadNext > 0)
                        {
                            --_manualReadNext;
                        }
                    }

                    try
                    {
                        lock (_readFunc)
                        {
                            T data = _readFunc();
                            _resultQueue.Enqueue(new ThreadReadResult<T>(data));
                        }
                    }
                    catch (Exception ex)
                    {
                        _errorOccured = true;
                        _resultQueue.Enqueue(new ThreadReadResult<T>(ex));
                    }
                }
                else
                {
                    // Sleep thread briefly as no reads are needed right now
                    Thread.Sleep(SleepTime);
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
                Thread.Sleep(SleepTime);
            }
        }
    }
}