using NUnit.Framework;
using System.Drawing;

namespace Phnx.Drawing.Tests
{
    public class HexColorConverterTests
    {
        [Test]
        public void ColorToHex_WithAlpha_ForTranslucentBlack_GetsBlackHexCode()
        {
            var black = Color.FromArgb(50, Color.Black);

            var hex = HexColorConverter.GetHexCode(black, true);

            Assert.AreEqual("32000000", hex);
        }

        [Test]
        public void ColorToHex_WithoutAlpha_ForBlack_GetsBlackHexCode()
        {
            var black = Color.Black;

            var hex = HexColorConverter.GetHexCode(black, false);

            Assert.AreEqual("000000", hex);
        }

        [Test]
        public void HexToColor_WithAlpha_ForTranslucentBlack_GetsBlackHexCode()
        {
            var hex = "32000000";

            var color = HexColorConverter.GetColor(hex);

            var black = Color.FromArgb(50, Color.Black);
            Assert.AreEqual(black.ToArgb(), color.ToArgb());
        }

        [Test]
        public void HexToColor_WithoutAlpha_ForBlack_GetsBlackHexCode()
        {
            var hex = "000000";

            var color = HexColorConverter.GetColor(hex);

            Assert.AreEqual(Color.Black.ToArgb(), color.ToArgb());
        }
    }
}
