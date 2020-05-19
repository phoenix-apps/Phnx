using Phnx.Try;
using System.Threading.Tasks;

namespace Phnx.Tests.Try.Extensions.Async
{
    public static class Factory
    {
        public static Task<TryResult<T>> SucceedTask<T>(T item)
        {
            return Task.FromResult(TryResult<T>.Succeed(item));
        }

        public static Task<TryResult> SucceedTask()
        {
            return Task.FromResult(TryResult.Succeed());
        }

        public static Task<TryResult<T>> FailedTask<T>(string err)
        {
            return Task.FromResult(TryResult<T>.Fail(err));
        }

        public static Task<TryResult> FailedTask(string err)
        {
            return Task.FromResult(TryResult.Fail(err));
        }

        public static Task<TryResult<T>> NullTask<T>()
        {
            return Task.FromResult<TryResult<T>>(null);
        }

        public static Task<TryResult> NullTask()
        {
            return Task.FromResult<TryResult>(null);
        }
    }
}
