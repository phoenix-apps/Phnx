using System;

namespace Phnx.Reflection.Tests.TestClasses
{
    public class PropertyFieldClass
    {
        public PropertyFieldClass()
        {
        }

        public PropertyFieldClass(string getOnlyPropVal)
        {
            PublicGetOnlyProperty = getOnlyPropVal;
        }

        public PropertyFieldClass(DateTime getOnlyFieldVal) => PublicFieldReadOnly = getOnlyFieldVal;

        public PropertyFieldClass(string getOnlyPropVal, DateTime getOnlyFieldVal)
        {
            PublicGetOnlyProperty = getOnlyPropVal;
            PublicFieldReadOnly = getOnlyFieldVal;
        }

        public string PublicProperty { get; set; }

        public string PublicGetOnlyProperty { get; }

        public DateTime PublicField;

        public readonly DateTime PublicFieldReadOnly;

        private int PrivateProperty { get; set; }

        private int _privateField;

        private int PrivateMethod()
        {
            _privateField = 0;
            return _privateField;
        }
    }
}
