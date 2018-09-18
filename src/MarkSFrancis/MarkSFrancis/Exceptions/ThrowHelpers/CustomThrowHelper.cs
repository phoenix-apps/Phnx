using System;

namespace MarkSFrancis.ThrowHelpers
{
    /// <summary>
    /// An implementation of <see cref="IThrowHelper"/> for function driven factories for handling each throw scenario
    /// </summary>
    /// <typeparam name="T">The type of exception this factory produces</typeparam>
    public class CustomThrowHelper<T> : IThrowHelper where T : Exception
    {
        private readonly Func<T> defaultFactory;
        private readonly Func<Exception, T> innerExceptionFactory;

        /// <summary>
        /// Create a new <see cref="CustomThrowHelper{T}"/> with custom constructors for each throw scenario
        /// </summary>
        /// <param name="defaultFactory">The factory to use for <see cref="Create()"/></param>
        /// <param name="innerExceptionFactory">The factory to use for <see cref="Create(Exception)"/></param>
        public CustomThrowHelper(Func<T> defaultFactory, Func<Exception, T> innerExceptionFactory)
        {
            this.defaultFactory = defaultFactory;
            this.innerExceptionFactory = innerExceptionFactory;
        }

        /// <summary>
        /// Throw the exception
        /// </summary>
        /// <exception cref="T"></exception>
        public Exception Create()
        {
            return defaultFactory();
        }

        /// <summary>
        /// Throw the exception with an inner exception
        /// </summary>
        /// <exception cref="T"></exception>
        public Exception Create(Exception innerException)
        {
            return innerExceptionFactory(innerException);
        }
    }
}
