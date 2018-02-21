using System;

namespace MarkSFrancis.Data.LazyLoad
{
    public class Lazy<T>
    {
        private readonly Func<T> _getFunc;
        private readonly Action<T> _setFunc;

        public delegate void ValueSetEvent(Lazy<T> sender, T newValue);

        public delegate void ValueCachedEvent(Lazy<T> sender, T cachedValue);

        public delegate void ValueGetEvent(Lazy<T> sender, T value);

        public event ValueSetEvent ValueSet;
        public event ValueCachedEvent ValueCached;
        public event ValueGetEvent ValueGet;

        private T _cachedValue;
        private bool _valueIsCached;

        public bool CanSet => _setFunc != null;

        private bool _valueChangedInLife;
        public bool ValueChangedInLife => _valueChangedInLife;

        public Lazy(Func<T> getFunction)
        {
            _getFunc = getFunction;
        }

        public Lazy(Func<T> getFunction, Action<T> setFunction)
        {
            _getFunc = getFunction;
            _setFunc = setFunction;
        }

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

        public void ClearCache()
        {
            _valueIsCached = false;
        }
    }
}