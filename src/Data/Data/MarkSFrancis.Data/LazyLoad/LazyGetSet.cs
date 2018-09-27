using System;

namespace MarkSFrancis.Data.LazyLoad
{
    /// <summary>
    /// Lazy loading for a property, with the option to set and get its value
    /// </summary>
    /// <typeparam name="T">The type of value cached</typeparam>
    public class LazyGetSet<T>
    {
        private readonly Func<T> _getFromExternal;
        private readonly Action<T> _setToExternal;

        /// <summary>
        /// Occurs when the value for a <see cref="LazyGetSet{T}"/> is set
        /// </summary>
        /// <param name="sender">The <see cref="LazyGetSet{T}"/> that triggered the event</param>
        /// <param name="newValue">The new value that has been assigned to the sender</param>
        public delegate void ValueSetEvent(LazyGetSet<T> sender, T newValue);

        /// <summary>
        /// Occurs when the value for a <see cref="LazyGetSet{T}"/> is loaded from an external source into its cache
        /// </summary>
        /// <param name="sender">The <see cref="LazyGetSet{T}"/> that triggered the event</param>
        /// <param name="cachedValue">The value that has been loaded into the cache</param>
        public delegate void ValueCachedEvent(LazyGetSet<T> sender, T cachedValue);

        /// <summary>
        /// Occurs when the value for a <see cref="LazyGetSet{T}"/> is requested
        /// </summary>
        /// <param name="sender">The <see cref="LazyGetSet{T}"/> that triggered the event</param>
        /// <param name="value">The value of the sender that has retrieved</param>
        public delegate void ValueGetEvent(LazyGetSet<T> sender, T value);

        /// <summary>
        /// Occurs when the value for this <see cref="LazyGetSet{T}"/> is set
        /// </summary>
        public event ValueSetEvent ValueSet;

        /// <summary>
        /// Occurs when the value for this <see cref="LazyGetSet{T}"/> is loaded from an external source into the cache
        /// </summary>
        public event ValueCachedEvent ValueCached;

        /// <summary>
        /// Occurs when the value for this <see cref="LazyGetSet{T}"/> is loaded from an external source, or from the cache
        /// </summary>
        public event ValueGetEvent ValueGet;

        private T _cachedValue;

        /// <summary>
        /// Whether the value for this <see cref="LazyGetSet{T}"/> can be set
        /// </summary>
        public bool CanSet => _setToExternal != null;

        /// <summary>
        /// Whether the value of this has ever been set
        /// </summary>
        public bool ValueChangedInLife { get; private set; }

        /// <summary>
        /// Whether the value is currently cached
        /// </summary>
        public bool IsCached { get; private set; }

        /// <summary>
        /// Create a new read-only <see cref="LazyGetSet{T}"/> using a function to load the data from an external source when requested
        /// </summary>
        /// <param name="get">The function to load the data from an external source</param>
        public LazyGetSet(Func<T> get)
        {
            _getFromExternal = get ?? throw new ArgumentNullException(nameof(get));
        }

        /// <summary>
        /// Create a new <see cref="LazyGetSet{T}"/> using a function to load the data from an external source when requested, and a function to update the external source. The external source is updated immediately when the <see cref="Value"/> is set
        /// </summary>
        /// <param name="get">The function to load the data from an external source</param>
        /// <param name="set">The function to send the data to an external source</param>
        public LazyGetSet(Func<T> get, Action<T> set)
        {
            _getFromExternal = get ?? throw new ArgumentNullException(nameof(get));
            _setToExternal = set ?? throw new ArgumentNullException(nameof(set));
        }

        /// <summary>
        /// The lazy-loaded value
        /// </summary>
        /// <exception cref="NotSupportedException">Could not set the value, as <see cref="CanSet"/> was <see langword="false"/></exception>
        public T Value
        {
            get
            {
                if (!IsCached)
                {
                    _cachedValue = _getFromExternal();
                    IsCached = true;
                    ValueChangedInLife = false;

                    ValueCached?.Invoke(this, _cachedValue);
                }

                ValueGet?.Invoke(this, _cachedValue);
                return _cachedValue;
            }
            set
            {
                if (!CanSet)
                {
                    throw new NotSupportedException(ErrorMessage.Factory.CannotSetValue());
                }

                _setToExternal(value);
                _cachedValue = value;
                IsCached = true;
                ValueChangedInLife = true;

                ValueSet?.Invoke(this, _cachedValue);
                ValueCached?.Invoke(this, _cachedValue);
            }
        }

        /// <summary>
        /// Reset and clear the cache. The next time <see cref="Value"/> is retrieved, it will reload from the external source
        /// </summary>
        public void ClearCache()
        {
            IsCached = false;
        }
    }
}