using NUnit.Framework;
using System;
using System.Drawing;

namespace Phnx.Drawing.Tests
{
    public class HexTests
    {
        [Test]
        public void New_WithNullChars_CreatesNullHex()
        {
            char[] chars = null;
            var hex = new Hex(chars);

            Assert.IsNull(hex.HexCode);
        }

        [Test]
        public void New_WithEmptyChars_CreatesEmptyHex()
        {
            char[] chars = new char[0];

            var hex = new Hex(chars);

            CollectionAssert.IsEmpty(hex.HexCode);
        }

        [Test]
        public void New_WithValidChars_CreatesHex()
        {
            byte[] expected = new byte[] { 170 };

            char[] chars = new[] { 'a', 'a' };
            var hex = new Hex(chars);

            CollectionAssert.AreEqual(expected, hex.HexCode);
        }

        [Test]
        public void New_WithOddChars_DoublesHex()
        {
            byte[] expected = new byte[] { 170, 170, 170 };

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
        public void New_WithByte_CreatesHex()
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
        public void New_WithEmptyBytes_CreatesEmptyHex()
        {
            byte[] bytes = new byte[0];

            var hex = new Hex(bytes);

            CollectionAssert.IsEmpty(hex.HexCode);
        }

        [Test]
        public void New_WithBytes_CreatesHex()
        {
            byte[] bytes = new byte[] { 1, 2, 4 };
            var copy = new byte[bytes.Length];
            bytes.CopyTo(copy, 0);

            var hex = new Hex(copy);

            CollectionAssert.AreEqual(bytes, hex.HexCode);
        }

        [Test]
        public void New_WithNullString_CreatesNullHex()
        {
            string text = null;
            var hex = new Hex(text);

            Assert.IsNull(hex.HexCode);
        }

        [Test]
        public void New_WithEmptyString_CreatesEmptyHex()
        {
            string text = string.Empty;

            var hex = new Hex(text);

            CollectionAssert.IsEmpty(hex.HexCode);
        }

        [Test]
        public void New_WithValidString_CreatesHex()
        {
            byte[] expected = new byte[] { 170, 170 };

            string text = "aaaa";
            var hex = new Hex(text);

            CollectionAssert.AreEqual(expected, hex.HexCode);
        }

        [Test]
        public void New_WithOddLengthString_DoublesHex()
        {
            byte[] expected = new byte[] { 170, 170, 170 };

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
        public void New_WithColor_AndAlpha_CreatesHex()
        {
            byte[] expected = new byte[] { 255, 0, 0, 0 };

            var hex = new Hex(Color.Black, true);

            Assert.AreEqual(expected, hex.HexCode);
        }

        [Test]
        public void New_WithColor_AndNoAlpha_CreatesHex()
        {
            byte[] expected = new byte[] { 0, 128, 0 };

            var hex = new Hex(Color.Green, false);

            Assert.AreEqual(expected, hex.HexCode);
        }

        [Test]
        public void GetHexString_WhenCodeIsNotSet_GetsNullHex()
        {
            var hex = new Hex();

            Assert.IsNull(hex.HexString);
        }

        [Test]
        public void GetHexString_WhenCodeIsEmpty_GetsEmptyHex()
        {
            string expected = string.Empty;
            byte[] code = new byte[0];

            var hex = new Hex(code);

            Assert.AreEqual(expected, hex.HexString);
        }

        [Test]
        public void GetHexString_WhenCodeHasContents_GetsHex()
        {
            string expected = "AAAA";
            var code = new byte[] { 170, 170 };

            var hex = new Hex(code);

            Assert.AreEqual(expected, hex.HexString);
        }

        [Test]
        public void SetHexString_WithNullString_SetsHexToNull()
        {
            string text = null;
            var hex = new Hex
            {
                HexString = text
            };

            Assert.IsNull(hex.HexCode);
        }

        [Test]
        public void SetHexString_WithEmptyString_SetsHexToEmpty()
        {
            string text = string.Empty;
            var hex = new Hex
            {
                HexString = text
            };

            CollectionAssert.IsEmpty(hex.HexCode);
        }

        [Test]
        public void SetHexString_WithValidString_SetsHex()
        {
            byte[] expected = new byte[] { 170, 170 };
            string text = "aaaa";
            var hex = new Hex
            {
                HexString = text
            };

            CollectionAssert.AreEqual(expected, hex.HexCode);
        }

        [Test]
        public void SetHexString_WithOddLengthString_DoublesHex()
        {
            byte[] expected = new byte[] { 170, 170, 170 };
            string text = "aaa";
            var hex = new Hex
            {
                HexString = text
            };

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

        [Test]
        public void ToBytes_WhenBytesAreNull_ReturnsNull()
        {
            byte[] expected = null;

            var hex = new Hex(expected);

            var bytes = hex.ToBytes();

            Assert.AreEqual(expected, bytes);
        }

        [Test]
        public void ToBytes_WhenBytesAreNotNull_ReturnsCopyOfBytes()
        {
            byte[] expected = new byte[] { 1, 52, 253 };

            var hex = new Hex(expected);

            var bytes = hex.ToBytes();

            CollectionAssert.AreEqual(expected, bytes);

            // Edit first array to ensure that the result is a copy of the original, but not the original
            expected[1]--;

            CollectionAssert.AreNotEqual(expected, bytes);
        }
    }
}
