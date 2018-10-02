using System;

namespace Phnx.IO.Threaded
{
    internal class ThreadReadResult<T>
    {
        public ThreadReadResult(T data)
        {
            Data = data;
        }

        public ThreadReadResult(Exception error)
        {
            Error = error ?? throw new ArgumentNullException(nameof(error));
        }

        public T Data { get; }

        public Exception Error { get; }

        public bool ErrorOccured => Error != null;
    }
}
