using System;
using System.Collections.Concurrent;
using System.Threading;

namespace MarkSFrancis.IO.Threaded
{
    public class ThreadedWriter<T> : IDisposable
    {
        private readonly ConcurrentQueue<T> _writeQueue;

        private readonly Action<T> _writeFunc;

        private bool _safeExit;

        public int SleepTime { get; }

        private readonly Thread _taskRunner;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writeFunc"></param>
        /// <param name="sleepTime">How long in ms to sleep if there are no queued tasks</param>
        public ThreadedWriter(Action<T> writeFunc, int sleepTime = 20)
        {
            _writeQueue = new ConcurrentQueue<T>();
            _writeFunc = writeFunc;
            SleepTime = sleepTime;

            _taskRunner = new Thread(_taskRunnerMethod);
            _taskRunner.Start();
        }

        public void Write(T valueToWrite)
        {
            _writeQueue.Enqueue(valueToWrite);
        }

        private void _taskRunnerMethod()
        {
            while (!_safeExit)
            {
                if (_writeQueue.Count > 0)
                {
                    // Write
                    if (!_writeQueue.TryDequeue(out var objectToWrite))
                    {
                        continue;
                    }

                    _writeFunc(objectToWrite);
                }
                else
                {
                    Thread.Sleep(SleepTime);
                }
            }
        }

        /// <summary>
        /// Optionally finishes writing to the output stream any pending output, and then safely exits all
        /// background threads
        /// </summary>
        /// <param name="finishWriting"></param>
        public void Dispose(bool finishWriting)
        {
            if (finishWriting)
            {
                while (_writeQueue.Count > 0)
                {
                    Thread.Sleep(SleepTime);
                }
            }

            _safeExit = true;

            while (_taskRunner.IsAlive)
            {
                Thread.Sleep(SleepTime);
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