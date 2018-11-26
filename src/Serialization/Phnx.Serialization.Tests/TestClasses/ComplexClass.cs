using System;

namespace Phnx.Serialization.Tests.TestClasses
{
    [Serializable]
    public class ComplexClass : BaseClass
    {
        public ComplexPropertyClass ComplexProperty { get; set; }

        public string MyValue { get; set; }
    }
}
