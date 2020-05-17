using NUnit.Framework;
using Phnx.Reflection.Tests.TestClasses;
using System;

namespace Phnx.Reflection.Tests.PropertyFieldInfo
{
    public class OtherTests
    {
        [Test]
        public void GettingAMember_WhichIsAProperty_ReportsToBeAProperty()
        {
            var prop = new PropertyFieldInfo<PropertyFieldClass, string>(p => p.PublicProperty);

            Assert.IsTrue(prop.IsProperty);
        }

        [Test]
        public void GettingAMember_WhichIsAField_ReportsToBeAField()
        {
            var field = new PropertyFieldInfo<PropertyFieldClass, DateTime>(p => p.PublicField);

            Assert.IsFalse(field.IsProperty);
        }
    }
}