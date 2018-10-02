using MarkSFrancis.Reflection;
using NUnit.Framework;
using Phnx.Windows.Extensions.Reflection;

namespace Phnx.Windows.Tests.Extensions.Reflection
{
    public class PropertyFieldInfoExtensionsTests
    {
        [Test]
        public void GettingWhetherAutoProperty_WhenItIs_ReturnsTrue()
        {
            // Arrange
            var propFieldInfo = new PropertyFieldInfo<TestClass, string>(t => t.AutoProperty);

            // Assert
            Assert.IsTrue(propFieldInfo.IsAutoProperty());
        }

        [Test]
        public void GettingWhetherAutoProperty_WhenItIsNot_ReturnsFalse()
        {
            // Arrange
            var propFieldInfo = new PropertyFieldInfo<TestClass, string>(t => t.NotAutoProperty);

            // Assert
            Assert.IsFalse(propFieldInfo.IsAutoProperty());
        }

        [Test]
        public void GettingWhetherAutoProperty_WhenItIsAField_ReturnsFalse()
        {
            // Arrange
            var propFieldInfo = new PropertyFieldInfo<TestClass, string>(t => t.Field);

            // Assert
            Assert.IsFalse(propFieldInfo.IsAutoProperty());
        }
    }
}
