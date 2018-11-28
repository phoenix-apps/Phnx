using NUnit.Framework;
using Phnx.Reflection;
using Phnx.Windows.Tests.Fakes;

namespace Phnx.Windows.Tests.Reflection.Extensions
{
    public class PropertyInfoExtensionsTests
    {
        [Test]
        public void GettingWhetherAutoProperty_WhenItIs_ReturnsTrue()
        {
            // Arrange
            var propFieldInfo = typeof(TestClass).GetProperty(nameof(TestClass.AutoProperty));

            // Assert
            Assert.IsTrue(propFieldInfo.IsAuto());
        }

        [Test]
        public void GettingWhetherAutoProperty_WhenItIsNot_ReturnsFalse()
        {
            // Arrange
            var propFieldInfo = typeof(TestClass).GetProperty(nameof(TestClass.NotAutoProperty));

            // Assert
            Assert.IsFalse(propFieldInfo.IsAuto());
        }
    }
}
