using NUnit.Framework;
using System;
using System.Drawing;

namespace Phnx.Drawing.Tests
{
    public class HexColorTests
    {
        [Test]
        public void IsValidColor_WhenHexCodeIsNull_ReturnsFalse()
        {
            var hex = new Hex();

            Assert.IsFalse(hex.IsColor);
        }

        [Test]
        public void IsValidColor_WhenHexCodeIsEmpty_ReturnsFalse()
        {
            var hex = new Hex(new byte[0]);

            Assert.IsFalse(hex.IsColor);
        }

        [Test]
        public void IsValidColor_WhenHexCodeIsTooShort_ReturnsFalse()
        {
            var hex = new Hex(new byte[] { 124, 12 });

            Assert.IsFalse(hex.IsColor);
        }

        [Test]
        public void IsValidColor_WhenHexCodeIsTooLong_ReturnsFalse()
        {
            var hex = new Hex(new byte[] { 124, 12, 124, 64, 25 });

            Assert.IsFalse(hex.IsColor);
        }

        [Test]
        public void IsValidColor_WhenHexIsOpaqueColor_ReturnsTrue()
        {
            var hex = new Hex(Color.Black, false);

            Assert.IsTrue(hex.IsColor);
        }

        [Test]
        public void IsValidColor_WhenHexIsTransparentColor_ReturnsTrue()
        {
            var hex = new Hex(Color.Black, true);

            Assert.IsTrue(hex.IsColor);
        }

        [Test]
        public void IsColorWithAlpha_WhenHexCodeIsNull_ReturnsFalse()
        {
            var hex = new Hex();

            Assert.IsFalse(hex.IsColorWithAlpha);
        }

        [Test]
        public void IsColorWithAlpha_WhenHexCodeIsEmpty_ReturnsFalse()
        {
            var hex = new Hex(new byte[0]);

            Assert.IsFalse(hex.IsColorWithAlpha);
        }

        [Test]
        public void IsColorWithAlpha_WhenHexCodeIsTooShort_ReturnsFalse()
        {
            var hex = new Hex(new byte[] { 124, 12 });

            Assert.IsFalse(hex.IsColorWithAlpha);
        }

        [Test]
        public void IsColorWithAlpha_WhenHexCodeIsTooLong_ReturnsFalse()
        {
            var hex = new Hex(new byte[] { 124, 12, 124, 64, 25 });

            Assert.IsFalse(hex.IsColorWithAlpha);
        }

        [Test]
        public void IsColorWithAlpha_WhenHexIsOpaqueColor_ReturnsFalse()
        {
            var hex = new Hex(Color.Black, false);

            Assert.IsFalse(hex.IsColorWithAlpha);
        }

        [Test]
        public void IsColorWithAlpha_WhenHexIsTransparentColor_ReturnsTrue()
        {
            var hex = new Hex(Color.Black, true);

            Assert.IsTrue(hex.IsColorWithAlpha);
        }

        [Test]
        public void ToColor_WhenHexCodeIsTooSmall_ThrowsInvalidCastException()
        {
            var hex = new Hex(255);

            Assert.Throws<InvalidCastException>(() => hex.ToColor());
        }

        [Test]
        public void ToColor_WhenHexCodeIsNull_ThrowsInvalidCastException()
        {
            var hex = new Hex((string)null);

            Assert.Throws<InvalidCastException>(() => hex.ToColor());
        }

        [Test]
        public void ToColor_WhenHexCodeIsOpaque_Converts()
        {
            var expected = Color.Black;
            var hex = new Hex(expected, false);

            var color = hex.ToColor();

            Assert.AreEqual(expected.ToArgb(), color.ToArgb());
        }

        [Test]
        public void ToColor_WhenHexCodeIsTransparent_Converts()
        {
            var expected = Color.FromArgb(123, 51, 15, 201);
            var hex = new Hex(expected, true);

            var color = hex.ToColor();

            Assert.AreEqual(expected.ToArgb(), color.ToArgb());
        }

        [Test]
        public void TryToColor_WhenHexCodeIsTooSmall_ThrowsInvalidCastException()
        {
            var hex = new Hex(255);

            var result = hex.TryToColor(out var color);

            Assert.IsFalse(result);
            Assert.AreEqual(default(Color), color);
        }

        [Test]
        public void TryToColor_WhenHexCodeIsNull_ThrowsInvalidCastException()
        {
            var hex = new Hex((string)null);

            var result = hex.TryToColor(out var color);

            Assert.IsFalse(result);
            Assert.AreEqual(default(Color), color);
        }

        [Test]
        public void TryToColor_WhenHexCodeIsOpaque_Converts()
        {
            var expected = Color.Black;
            var hex = new Hex(expected, false);

            var result = hex.TryToColor(out var color);

            Assert.IsTrue(result);
            Assert.AreEqual(expected.ToArgb(), color.ToArgb());
        }

        [Test]
        public void TryToColor_WhenHexCodeIsTransparent_Converts()
        {
            var expected = Color.FromArgb(123, 51, 15, 201);
            var hex = new Hex(expected, true);

            var result = hex.TryToColor(out var color);

            Assert.IsTrue(result);
            Assert.AreEqual(expected.ToArgb(), color.ToArgb());
        }
    }
}
