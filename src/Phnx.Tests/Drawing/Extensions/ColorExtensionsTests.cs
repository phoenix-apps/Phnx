using NUnit.Framework;
using System.Drawing;

namespace Phnx.Drawing.Tests.Extensions
{
    public class ColorExtensionsTests
    {
        [Test]
        public void ToHex_ForBlack_WithoutAlpha_GetsHex()
        {
            string expected = "000000";

            var black = Color.Black;

            var hex = black.ToHex(false);

            Assert.AreEqual(expected, hex.HexString);
        }

        [Test]
        public void ToHex_ForBlack_WithAlpha_GetsHex()
        {
            string expected = "FF000000";

            var black = Color.Black;

            var hex = black.ToHex(true);

            Assert.AreEqual(expected, hex.HexString);
        }
    }
}
