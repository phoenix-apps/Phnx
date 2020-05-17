using NUnit.Framework;
using Phnx.Reflection.Tests.TestClasses;
using System;

namespace Phnx.Reflection.Tests.PropertyFieldInfo
{
    public class AutoPropertyTests
    {
        [Test]
        public void GettingWhetherAutoProperty_WhenItIs_ReturnsTrue()
        {
            // Arrange
            var propFieldInfo = new PropertyFieldInfo<PropertyFieldClass, string>(t => t.PublicProperty);

            // Assert
            Assert.IsTrue(propFieldInfo.IsAutoProperty);
        }

        [Test]
        public void GettingWhetherAutoProperty_WhenItIsNot_ReturnsFalse()
        {
            // Arrange
            var propFieldInfo = new PropertyFieldInfo<PropertyFieldClass, string>(t => t.PublicNonAutoProperty);

            // Assert
            Assert.IsFalse(propFieldInfo.IsAutoProperty);
        }

        [Test]
        public void GettingWhetherAutoProperty_WhenItIsAField_ReturnsFalse()
        {
            // Arrange
            var propFieldInfo = new PropertyFieldInfo<PropertyFieldClass, DateTime>(t => t.PublicField);

            // Assert
            Assert.IsFalse(propFieldInfo.IsAutoProperty);
        }
    }
}
