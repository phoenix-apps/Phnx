using System;

namespace MarkSFrancis.IO.Threaded
{
    internal class ThreadReadResult<T>
    {
        public ThreadReadResult(T data)
        {
            Data = data;
        }

        public ThreadReadResult(Exception error)
        {
            Error = error;
        }

        public T Data { get; }

        public bool Faulted => Error != null;
        public Exception Error { get; }
    }
}
