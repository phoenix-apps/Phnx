using System;
using System.Collections.Concurrent;
using System.Threading;

namespace MarkSFrancis.IO.Threaded
{
    public class ThreadedReader<T> : IDisposable
    {
        private ConcurrentQueue<T> ResultQueue { get; }
        private Func<T> ReadFunc { get; }
        public int CacheCount => ResultQueue.Count;

        private bool ReadNext => _manualReadNext > 0 || (!SuppressLookAhead && LookAheadCount > ResultQueue.Count);
        private int _manualReadNext;
        public int LookAheadCount { get; set; }
        private bool SuppressLookAhead { get; set; }
        private Exception ReadException { get; set; }

        private bool SafeExit { get; set; }
        public int SleepTime { get; set; }

        private Thread TaskRunner { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="readFunc"></param>
        /// <param name="sleepTime">How long in ms to sleep if the look ahead is full, and no tasks are waiting</param>
        /// <param name="lookAheadCount">Number of values to pre-load</param>
        public ThreadedReader(Func<T> readFunc, int sleepTime = 20, int lookAheadCount = 100)
        {
            ResultQueue = new ConcurrentQueue<T>();
            ReadFunc = readFunc;
            LookAheadCount = lookAheadCount;
            SleepTime = sleepTime;

            TaskRunner = new Thread(TaskRunnerMethod);
            TaskRunner.Start();
        }

        public T Read()
        {
            T returnT;
            if (ResultQueue.Count > 0)
            {
                ResultQueue.TryDequeue(out returnT);
            }
            else
            {
                Interlocked.Increment(ref _manualReadNext);
                while (ResultQueue.Count == 0 && ReadException == null)
                {
                    Thread.Sleep(SleepTime);
                }

                if (ReadException != null)
                {
                    throw ReadException;
                }

                ResultQueue.TryDequeue(out returnT);
                return returnT;
            }
            return returnT;
        }

        private void TaskRunnerMethod()
        {
            while (!SafeExit)
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
                        ResultQueue.Enqueue(ReadFunc());
                    }
                    catch (Exception ex)
                    {
                        if (manualRead)
                        {
                            ReadException = ex;
                        }
                        else
                        {
                            // ignore exception
                            SuppressLookAhead = true;
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
            SafeExit = true;

            while (TaskRunner.IsAlive)
            {
                Thread.Sleep(SleepTime);
            }
        }
    }
}