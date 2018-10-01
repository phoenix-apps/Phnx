using NUnit.Framework;
using System.Drawing;

namespace MarkSFrancis.Drawing.Tests
{
    public class ColorTests
    {
        [Test]
        public void GettingTheHexColorCode_ForBlack_GetsBlackHexCode()
        {
            // Arrange
            var black = Color.Black;

            // Act
            var hex = new Hex(black);
            var hexCode = hex.ToString();

            // Assert
            Assert.AreEqual("000000", hexCode);
        }
    }
}
