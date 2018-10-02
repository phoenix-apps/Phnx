using System;
using Phnx.Reflection.Tests.TestClasses;
using NUnit.Framework;

namespace Phnx.Reflection.Tests.PropertyFieldInfo
{
    public class PropertyFieldInfoGetSetTests
    {
        [Test]
        public void GettingAProperty_WhichIsReadAndWrite_CanBeReadAndWritten()
        {
            var prop = new PropertyFieldInfo<PropertyFieldClass, string>(p => p.PublicProperty);

            Assert.IsTrue(prop.CanGet);
            Assert.IsTrue(prop.CanSet);
        }

        [Test]
        public void GettingAProperty_WhichIsReadOnly_CanBeReadAndNotWritten()
        {
            var prop = new PropertyFieldInfo<PropertyFieldClass, string>(p => p.PublicGetOnlyProperty);

            Assert.IsTrue(prop.CanGet);
            Assert.IsFalse(prop.CanSet);
        }
        [Test]
        public void GettingAField_WhichIsReadAndWrite_CanBeReadAndWritten()
        {
            var field = new PropertyFieldInfo<PropertyFieldClass, DateTime>(p => p.PublicField);

            Assert.IsTrue(field.CanGet);
            Assert.IsTrue(field.CanSet);
        }

        [Test]
        public void GettingAField_WhichIsReadOnly_CanBeReadAndNotWritten()
        {
            var field = new PropertyFieldInfo<PropertyFieldClass, DateTime>(p => p.PublicFieldReadOnly);

            Assert.IsTrue(field.CanGet);
            Assert.IsFalse(field.CanSet);
        }
    }
}
