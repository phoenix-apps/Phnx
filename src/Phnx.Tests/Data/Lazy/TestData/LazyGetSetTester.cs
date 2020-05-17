using Phnx.Data.Lazy;
using System;

namespace Phnx.Data.Tests.Lazy.TestData
{
    public class LazyGetSetTester<T>
    {
        public int GetCount { get; private set; }

        public int SetCount { get; private set; }

        public LazyGetSet<T> LazyGetSet { get; set; }

        public LazyGetSetTester(Func<T> get = null)
        {
            LazyGetSet = new LazyGetSet<T>(() =>
            {
                ++GetCount;
                if (get != null)
                {
                    return get();
                }
                else
                {
                    return default(T);
                }
            });
        }

        public LazyGetSetTester(Func<T> get, Action<T> set)
        {
            LazyGetSet = new LazyGetSet<T>(() =>
            {
                ++GetCount;
                if (get != null)
                {
                    return get();
                }
                else
                {
                    return default(T);
                }
            }, o =>
            {
                ++SetCount;
                set?.Invoke(o);
            });
        }
    }
}
