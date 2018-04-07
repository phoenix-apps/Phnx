using System.Drawing;
using NUnit.Framework;

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
            var hex = new Hexadecimal(black);
            var hexCode = hex.ToString();

            // Assert
            Assert.AreEqual("000000", hexCode);
        }
    }
}
