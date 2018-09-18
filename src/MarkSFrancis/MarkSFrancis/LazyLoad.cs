using System;
using System.Threading;

namespace MarkSFrancis
{
    /// <summary>
    /// Provides casts to its root type and to <see cref="Lazy{T}"/> to make working with lazy loading easier
    /// </summary>
    /// <typeparam name="T">The type of data loaded</typeparam>
    public class LazyLoad<T> : Lazy<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LazyLoad{T}"/> class. When lazy initialization occurs, the default constructor of the target type is used.
        /// </summary>
        public LazyLoad() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LazyLoad{T}"/> class. When lazy initialization occurs, the default constructor of the target type and the specified initialization mode are used
        /// </summary>
        /// <param name="isThreadSafe"><see langword="true"/> to make this instance usable concurrently by multiple threads; <see langword="false"/> to make the instance usable by only one thread at a time</param>
        public LazyLoad(bool isThreadSafe) : base(isThreadSafe)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LazyLoad{T}"/> class. When lazy initialization occurs, the specified initialization function is used
        /// </summary>
        /// <param name="valueFactory">The delegate that is invoked to produce the lazily initialized value when it is needed</param>
        /// <exception cref="ArgumentNullException"><paramref name="valueFactory"/> is <see langword="null"/></exception>
        public LazyLoad(Func<T> valueFactory) : base(valueFactory)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LazyLoad{T}"/> class that uses the default constructor of <typeparamref name="T"/> and the specified thread-safety mode
        /// </summary>
        /// <param name="mode">The thread safety mode</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="mode"/> contains an invalid value</exception>
        public LazyLoad(LazyThreadSafetyMode mode) : base(mode)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LazyLoad{T}"/> class. When lazy initialization occurs, the specified initialization function and initialization mode are used
        /// </summary>
        /// <param name="valueFactory">The delegate that is invoked to produce the lazily initialized value when it is needed</param>
        /// <param name="isThreadSafe"><see langword="true"/> to make this instance usable concurrently by multiple threads; <see langword="false"/> to make the instance usable by only one thread at a time</param>
        /// <exception cref="ArgumentNullException"><paramref name="valueFactory"/> is <see langword="null"/></exception>
        public LazyLoad(Func<T> valueFactory, bool isThreadSafe)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LazyLoad{T}"/> class that uses the specified initialization function and thread-safety mode
        /// </summary>
        /// <param name="valueFactory">The delegate that is invoked to produce the lazily initialized value when it is needed</param>
        /// <param name="mode">The thread safety mode</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="mode"/> contains an invalid value</exception>
        /// <exception cref="ArgumentNullException"><paramref name="valueFactory"/> is <see langword="null"/></exception>
        public LazyLoad(Func<T> valueFactory, LazyThreadSafetyMode mode)
        {
        }

        /// <summary>
        /// Implicitly gets <paramref name="lazy"/>'s <see cref="Lazy{T}.Value"/>
        /// </summary>
        /// <param name="lazy">The <see cref="LazyLoad{T}"/> to get the value for</param>
        public static implicit operator T(LazyLoad<T> lazy)
        {
            return lazy.Value;
        }
    }
}
