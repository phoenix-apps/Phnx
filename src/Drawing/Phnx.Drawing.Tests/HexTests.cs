using NUnit.Framework;
using System;
using System.Drawing;

namespace Phnx.Drawing.Tests
{
    public class HexTests
    {
        [Test]
        public void New_WithNullChars_GetsNullHex()
        {
            char[] chars = null;
            var hex = new Hex(chars);

            Assert.IsNull(hex.HexCode);
        }

        [Test]
        public void New_WithEmptyChars_GetsHex()
        {
            char[] chars = new char[0];

            var hex = new Hex(chars);

            CollectionAssert.IsEmpty(hex.HexCode);
        }

        [Test]
        public void New_WithValidChars_GetsHex()
        {
            byte[] expected = new byte[] { 170, 160 };

            char[] chars = new[] { 'a', 'a', 'a' };
            var hex = new Hex(chars);

            CollectionAssert.AreEqual(expected, hex.HexCode);
        }

        [Test]
        public void New_WithInvalidChars_ThrowsFormatException()
        {
            byte[] expected = new byte[] { 170, 160 };

            char[] chars = new[] { 'g', 'a', 'a' };
            Assert.Throws<FormatException>(() => _ = new Hex(chars));
        }

        [Test]
        public void New_WithByte_GetsHex()
        {
            byte expected = 251;

            var hex = new Hex(expected);

            Assert.AreEqual(1, hex.HexCode.Length);
            Assert.AreEqual(expected, hex.HexCode[0]);
        }

        [Test]
        public void New_WithNullBytes_ThrowsArgumentNullException()
        {
            byte[] bytes = null;

            var hex = new Hex(bytes);

            Assert.IsNull(hex.HexCode);
        }

        [Test]
        public void New_WithEmptyBytes_SetsBytes()
        {
            byte[] bytes = new byte[0];

            var hex = new Hex(bytes);

            CollectionAssert.IsEmpty(hex.HexCode);
        }

        [Test]
        public void New_WithBytes_SetsBytes()
        {
            byte[] bytes = new byte[] { 1, 2, 4 };
            var copy = new byte[bytes.Length];
            bytes.CopyTo(copy, 0);

            var hex = new Hex(copy);

            CollectionAssert.AreEqual(bytes, hex.HexCode);
        }

        [Test]
        public void New_WithNullstrings_GetsNullHex()
        {
            string text = null;
            var hex = new Hex(text);

            Assert.IsNull(hex.HexCode);
        }

        [Test]
        public void New_WithEmptystrings_GetsHex()
        {
            string text = string.Empty;

            var hex = new Hex(text);

            CollectionAssert.IsEmpty(hex.HexCode);
        }

        [Test]
        public void New_WithValidstrings_GetsHex()
        {
            byte[] expected = new byte[] { 170, 160 };

            string text = "aaa";
            var hex = new Hex(text);

            CollectionAssert.AreEqual(expected, hex.HexCode);
        }

        [Test]
        public void New_WithInvalidstrings_ThrowsFormatException()
        {
            byte[] expected = new byte[] { 170, 160 };

            string text = "gaa";
            Assert.Throws<FormatException>(() => _ = new Hex(text));
        }

        [Test]
        public void New_WithColor_AndAlpha_GetsHex()
        {
            byte[] expected = new byte[] { 255, 0, 0, 0 };

            var hex = new Hex(Color.Black, true);

            Assert.AreEqual(expected, hex.HexCode);
        }

        [Test]
        public void New_WithColor_AndNoAlpha_GetsHex()
        {
            byte[] expected = new byte[] { 0, 128, 0 };

            var hex = new Hex(Color.Green, false);

            Assert.AreEqual(expected, hex.HexCode);
        }

        [Test]
        public void GetHexString_WhenCodeIsNull_GetsNullHex()
        {
            var hex = new Hex();

            Assert.IsNull(hex.HexString);
        }

        [Test]
        public void GetHexString_WhenCodeIsEmpty_GetsHex()
        {
            string expected = string.Empty;
            byte[] code = new byte[0];

            var hex = new Hex(code);

            Assert.AreEqual(expected, hex.HexString);
        }

        [Test]
        public void GetHexString_WhenCodeHasContents_GetsHex()
        {
            string expected = "aaa";
            byte[] code = new byte[] { 170, 160 };

            var hex = new Hex();
            hex.HexString = expected;

            Assert.AreEqual(expected, hex.HexString);
        }

        [Test]
        public void SetHexString_WithNullstrings_GetsNullHex()
        {
            string text = null;
            var hex = new Hex();
            hex.HexString = text;

            Assert.IsNull(hex.HexCode);
        }

        [Test]
        public void SetHexString_WithEmptystrings_GetsHex()
        {
            string text = string.Empty;

            var hex = new Hex();
            hex.HexString = text;

            CollectionAssert.IsEmpty(hex.HexCode);
        }

        [Test]
        public void SetHexString_WithValidstrings_GetsHex()
        {
            byte[] expected = new byte[] { 170, 160 };

            string text = "aaa";
            var hex = new Hex();
            hex.HexString = text;

            CollectionAssert.AreEqual(expected, hex.HexCode);
        }

        [Test]
        public void SetHexString_WithInvalidstrings_ThrowsFormatException()
        {
            byte[] expected = new byte[] { 170, 160 };

            string text = "gaa";
            var hex = new Hex();

            Assert.Throws<FormatException>(() => hex.HexString = text);
        }
    }
}
