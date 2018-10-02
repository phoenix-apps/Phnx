using System;

namespace Phnx.Reflection.Tests.TestClasses
{
    public class PropertyFieldClass
    {
        public string PublicProperty { get; set; }

        // ReSharper disable once UnassignedGetOnlyAutoProperty
        public string PublicGetOnlyProperty { get; }

        public DateTime PublicField;

        // ReSharper disable once UnassignedReadonlyField
        public readonly DateTime PublicFieldReadOnly;

        private int PrivateProperty { get; set; }

#pragma warning disable 169
        private PropertyFieldClass _privateField;
#pragma warning restore 169
    }
}
