using System;
using System.Collections.Concurrent;
using System.Threading;

namespace MarkSFrancis.IO.Threaded
{
    public class ThreadedWriter<T> : IDisposable
    {
        private ConcurrentQueue<T> WriteQueue { get; }

        private Action<T> WriteFunc { get; }

        private bool SafeExit { get; set; }

        public int SleepTime { get; set; }

        private Thread TaskRunner { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writeFunc"></param>
        /// <param name="sleepTime">How long in ms to sleep if there are no queued tasks</param>
        public ThreadedWriter(Action<T> writeFunc, int sleepTime = 20)
        {
            WriteQueue = new ConcurrentQueue<T>();
            WriteFunc = writeFunc;
            SleepTime = sleepTime;

            TaskRunner = new Thread(TaskRunnerMethod);
            TaskRunner.Start();
        }

        public void Write(T itemToWrite)
        {
            WriteQueue.Enqueue(itemToWrite);
        }

        private void TaskRunnerMethod()
        {
            while (!SafeExit)
            {
                if (WriteQueue.Count > 0)
                {
                    // Write
                    if (!WriteQueue.TryDequeue(out var objectToWrite))
                    {
                        continue;
                    }

                    WriteFunc(objectToWrite);
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
                while (WriteQueue.Count > 0)
                {
                    Thread.Sleep(SleepTime);
                }
            }

            SafeExit = true;

            while (TaskRunner.IsAlive)
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