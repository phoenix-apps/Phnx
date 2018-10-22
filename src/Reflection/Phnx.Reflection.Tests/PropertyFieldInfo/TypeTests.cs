using NUnit.Framework;
using Phnx.Reflection.Tests.TestClasses;
using System;

namespace Phnx.Reflection.Tests.PropertyFieldInfo
{
    public class TypeTests
    {
        [Test]
        public void GetType_WhenProperty_GetsType()
        {
            var propField = new PropertyFieldInfo<PropertyFieldClass, string>(pf => pf.PublicGetOnlyProperty);

            Assert.AreEqual(typeof(string), propField.Type);
        }

        [Test]
        public void GetType_WhenField_GetsType()
        {
            var propField = new PropertyFieldInfo<PropertyFieldClass, DateTime>(pf => pf.PublicFieldReadOnly);

            Assert.AreEqual(typeof(DateTime), propField.Type);
        }
    }
}
