using System;
using System.Threading;
using System.Threading.Tasks;

namespace MarkSFrancis.Extensions
{
    /// <summary>
    /// Extensions for <see cref="Func{TResult}"/> and <see cref="Action"/>, helping with asynchronous operations
    /// </summary>
    public static class AsyncExtensions
    {
        /// <summary>
        /// Invoke a method asynchronously
        /// </summary>
        /// <param name="action">The method to invoke</param>
        /// <param name="withContext">Whether to preserve the <see cref="SynchronizationContext.Current"/>. This should generally be set to <see langword="false"/> if you're writing a library</param>
        /// <returns></returns>
        public static Task InvokeAsync(this Action action, bool withContext = true)
        {
            var task = Task.Factory.StartNew(action);

            task.ConfigureAwait(withContext);

            return task;
        }

        /// <summary>
        /// Invoke a method asynchronously
        /// </summary>
        /// <param name="func">The method to invoke</param>
        /// <param name="withContext">Whether to preserve the <see cref="SynchronizationContext.Current"/>. This should generally be set to <see langword="true"/> if you're writing a library</param>
        /// <returns></returns>
        public static Task<T> InvokeAsync<T>(this Func<T> func, bool withContext = true)
        {
            var task = Task.Factory.StartNew(func);

            task.ConfigureAwait(withContext);

            return task;
        }
    }
}