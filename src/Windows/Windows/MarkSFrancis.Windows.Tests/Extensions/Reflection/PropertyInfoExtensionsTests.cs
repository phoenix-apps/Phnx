using MarkSFrancis.Windows.Extensions.Reflection;
using NUnit.Framework;

namespace MarkSFrancis.Windows.Tests.Extensions.Reflection
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
