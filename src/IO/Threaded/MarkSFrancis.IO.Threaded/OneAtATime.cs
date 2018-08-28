using System;
using System.Threading;

namespace MarkSFrancis.IO.Threaded
{
    /// <summary>
    /// Manages multiple incoming requests, only running the most recently requested, and only executing one at a time
    /// </summary>
    public class OneAtATime
    {
        private readonly OneAtATime<object> _oneAtATime;

        /// <summary>
        /// Create a new instance of <see cref="OneAtATime"/> with an action that has no parameters to execute one at a time
        /// </summary>
        /// <param name="runner">The action to be executed one at a time</param>
        public OneAtATime(Action runner)
        {
            _oneAtATime = new OneAtATime<object>(o => runner());
        }

        /// <summary>
        /// Execute the action if it's not already running or queue a new execution if it's already running
        /// </summary>
        public void Execute()
        {
            _oneAtATime.Execute(null);
        }
    }

    /// <summary>
    /// Manages multiple incoming requests, only running the most recently requested, and only executing one at a time
    /// </summary>
    /// <typeparam name="TIn">The type of input to the action to run one at a time</typeparam>
    public class OneAtATime<TIn, TOut>
    {
        /// <summary>
        /// Create a new instance of <see cref="OneAtATime{TIn, TOut}"/> with a function that has 1 parameter to execute one at a time
        /// </summary>
        /// <param name="runner">The action to be executed one at a time</param>
        public OneAtATime(Func<TIn, TOut> runner)
        {
            _threadRunningLock = new object();
            _threadRunning = false;

            Runner = runner;
        }

        private readonly object _threadRunningLock;
        private bool _threadRunning;

        /// <summary>
        /// Whether an execution is currently queued to run as soon as the previous is finished
        /// </summary>
        private bool ThreadRunning
        {
            get
            {
                lock (_threadRunningLock)
                {
                    return _threadRunning;
                }
            }
            set
            {
                lock (_threadRunningLock)
                {
                    _threadRunning = value;
                }
            }
        }

        private readonly object _queuedLock;
        private bool _queued;
        private TIn _queuedData;

        private bool Queued
        {
            get
            {
                lock (_queuedLock)
                {
                    return _queued;
                }
            }
        }

        private object _lastestResultLock;
        private TOut _lastestResult;

        /// <summary>
        /// Get the latest result from the most recently execution
        /// </summary>
        public TOut LatestResult
        {
            get
            {
                lock (_lastestResultLock)
                {
                    return _lastestResult;
                }
            }
        }

        /// <summary>
        /// Get whether an item is currently executing
        /// </summary>
        public bool IsExecuting => ThreadRunning || Queued;

        /// <summary>
        /// The method to execute one at a time
        /// </summary>
        private Func<TIn, TOut> Runner { get; }

        /// <summary>
        /// Execute the action if it's not already running or queue a new execution if it's already running
        /// </summary>
        /// <param name="parameter">The data to pass to the </param>
        public void Execute(TIn parameter)
        {
            SetQueuedData(parameter);

            while (GetShouldExecute(out parameter))
            {
                ThreadPool.QueueUserWorkItem(cb =>
                {
                    Runner(parameter);
                    ThreadRunning = false;
                });
            }
        }

        private bool GetShouldExecute(out TIn parameter)
        {
            bool shouldExecute;
            lock (_queuedLock)
            {
                lock (_threadRunningLock)
                {
                    if (!_threadRunning)
                    {
                        shouldExecute = _threadRunning = true;
                    }
                    else
                    {
                        shouldExecute = false;
                    }
                }

                parameter = _queuedData;
            }

            return shouldExecute;
        }

        private void SetQueuedData(TIn parameter)
        {
            lock (_queuedLock)
            {
                _queued = true;
                _queuedData = parameter;
            }
        }
    }
}
