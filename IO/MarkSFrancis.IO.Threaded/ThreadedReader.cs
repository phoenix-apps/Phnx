using System;
using System.Collections.Concurrent;
using System.Threading;

namespace MarkSFrancis.IO.Threaded
{
    public class ThreadedReader<T> : IDisposable
    {
        private readonly ConcurrentQueue<T> _resultQueue;
        private readonly Func<T> _readFunc;
        public int CacheCount => _resultQueue.Count;

        private bool ReadNext => _manualReadNext > 0 || (!_suppressLookAhead && LookAheadCount > _resultQueue.Count);
        private int _manualReadNext;
        public int LookAheadCount { get; }
        private bool _suppressLookAhead;
        private Exception _readException;

        private bool _safeExit;
        public int SleepTime { get; }

        private readonly Thread _taskRunner;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="readFunc"></param>
        /// <param name="sleepTime">How long in ms to sleep if the look ahead is full, and no tasks are waiting</param>
        /// <param name="lookAheadCount">Number of values to pre-load</param>
        public ThreadedReader(Func<T> readFunc, int sleepTime = 20, int lookAheadCount = 100)
        {
            _resultQueue = new ConcurrentQueue<T>();
            _readFunc = readFunc;
            LookAheadCount = lookAheadCount;
            SleepTime = sleepTime;

            _taskRunner = new Thread(TaskRunnerMethod);
            _taskRunner.Start();
        }

        public T Read()
        {
            T returnT;
            if (_resultQueue.Count > 0)
            {
                _resultQueue.TryDequeue(out returnT);
            }
            else
            {
                Interlocked.Increment(ref _manualReadNext);
                while (_resultQueue.Count == 0 && _readException == null)
                {
                    Thread.Sleep(SleepTime);
                }

                if (_readException != null)
                {
                    throw _readException;
                }

                _resultQueue.TryDequeue(out returnT);
                return returnT;
            }
            return returnT;
        }

        private void TaskRunnerMethod()
        {
            while (!_safeExit)
            {
                if (ReadNext)
                {
                    bool manualRead = false;
                    if (_manualReadNext > 0)
                    {
                        manualRead = true;
                        Interlocked.Decrement(ref _manualReadNext);
                    }

                    try
                    {
                        _resultQueue.Enqueue(_readFunc());
                    }
                    catch (Exception ex)
                    {
                        if (manualRead)
                        {
                            _readException = ex;
                        }
                        else
                        {
                            // ignore exception
                            _suppressLookAhead = true;
                        }
                    }
                }
                else
                {
                    Thread.Sleep(SleepTime);
                }
            }
        }

        public void Dispose()
        {
            _safeExit = true;

            while (_taskRunner.IsAlive)
            {
                Thread.Sleep(SleepTime);
            }
        }
    }
}