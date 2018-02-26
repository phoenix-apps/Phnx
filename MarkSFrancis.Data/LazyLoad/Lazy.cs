using System;

namespace MarkSFrancis.Data.LazyLoad
{
    /// <summary>
    /// Lazy loading for a property, with the option to set and get its value
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Lazy<T>
    {
        private readonly Func<T> _getFunc;
        private readonly Action<T> _setFunc;

        /// <summary>
        /// Occurs when the value for a <see cref="Lazy{T}"/> is set
        /// </summary>
        /// <param name="sender">The <see cref="Lazy{T}"/> that triggered the event</param>
        /// <param name="newValue">The new value that has been assigned to the sender</param>
        public delegate void ValueSetEvent(Lazy<T> sender, T newValue);

        /// <summary>
        /// Occurs when the value for a <see cref="Lazy{T}"/> is loaded from an external source into its cache
        /// </summary>
        /// <param name="sender">The <see cref="Lazy{T}"/> that triggered the event</param>
        /// <param name="cachedValue">The value that has been loaded into the cache</param>
        public delegate void ValueCachedEvent(Lazy<T> sender, T cachedValue);
        
        /// <summary>
        /// Occurs when the value for a <see cref="Lazy{T}"/> is requested
        /// </summary>
        /// <param name="sender">The <see cref="Lazy{T}"/> that triggered the event</param>
        /// <param name="value">The value of the sender that has retrieved</param>
        public delegate void ValueGetEvent(Lazy<T> sender, T value);

        /// <summary>
        /// Occurs when the value for this <see cref="Lazy{T}"/> is set
        /// </summary>
        public event ValueSetEvent ValueSet;

        /// <summary>
        /// Occurs when the value for this <see cref="Lazy{T}"/> is loaded from an external source into the cache
        /// </summary>
        public event ValueCachedEvent ValueCached;

        /// <summary>
        /// Occurs when the value for this <see cref="Lazy{T}"/> is loaded from an external source, or from the cache
        /// </summary>
        public event ValueGetEvent ValueGet;

        private T _cachedValue;
        private bool _valueIsCached;

        /// <summary>
        /// Whether the value for this <see cref="Lazy{T}"/> can be set
        /// </summary>
        public bool CanSet => _setFunc != null;

        private bool _valueChangedInLife;

        /// <summary>
        /// Whether the value of this has ever been set
        /// </summary>
        public bool ValueChangedInLife => _valueChangedInLife;

        /// <summary>
        /// Create a new read-only <see cref="Lazy{T}"/> using a function to load the data from an external source when requested
        /// </summary>
        /// <param name="getFunction">The function to load the data from an external source</param>
        public Lazy(Func<T> getFunction)
        {
            _getFunc = getFunction;
        }

        /// <summary>
        /// Create a new <see cref="Lazy{T}"/> using a function to load the data from an external source when requested, and a function to update the external source. The external source is updated immediately when the <see cref="Value"/> is set
        /// </summary>
        /// <param name="getFunction">The function to load the data from an external source</param>
        /// <param name="setFunction">The function to send the data to an external source</param>
        public Lazy(Func<T> getFunction, Action<T> setFunction)
        {
            _getFunc = getFunction;
            _setFunc = setFunction;
        }

        /// <summary>
        /// The lazy-loaded value
        /// </summary>
        public T Value
        {
            get
            {
                if (!_valueIsCached)
                {
                    _cachedValue = _getFunc();
                    _valueIsCached = true;

                    ValueCached?.Invoke(this, _cachedValue);
                }

                ValueGet?.Invoke(this, _cachedValue);
                return _cachedValue;
            }
            set
            {
                if (!CanSet)
                {
                    throw new InvalidOperationException($"Cannot set {typeof(Lazy<T>).Name} value");
                }

                _setFunc.Invoke(value);
                _cachedValue = value;
                _valueIsCached = true;
                _valueChangedInLife = true;

                ValueSet?.Invoke(this, Value);
            }
        }

        /// <summary>
        /// Reset and clear the cache. The next time <see cref="Value"/> is retrieved, it will reload from the external source
        /// </summary>
        public void ClearCache()
        {
            _valueIsCached = false;
        }
    }
}