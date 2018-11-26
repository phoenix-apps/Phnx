using NUnit.Framework;
using System;

namespace Phnx.Tests
{
    public class OrdinalsTests
    {
        [Test]
        public void ConvertingToOrdinal_WithZero_GetsOrdinal()
        {
            var result = Ordinals.ToOrdinal(0);

            Assert.AreEqual("0", result);
        }

        [Test]
        public void ConvertingToOrdinal_With353_GetsOrdinal()
        {
            var result = Ordinals.ToOrdinal(353);

            Assert.AreEqual("353rd", result);
        }

        [Test]
        public void ConvertingToOrdinal_With1Or2Or3Or4_GetsOrdinal()
        {
            var result1 = Ordinals.ToOrdinal(1);
            var result2 = Ordinals.ToOrdinal(2);
            var result3 = Ordinals.ToOrdinal(3);
            var result4 = Ordinals.ToOrdinal(4);

            Assert.AreEqual("1st", result1);
            Assert.AreEqual("2nd", result2);
            Assert.AreEqual("3rd", result3);
            Assert.AreEqual("4th", result4);
        }

        [Test]
        public void ConvertingToOrdinal_With13_GetsOrdinal()
        {
            var result = Ordinals.ToOrdinal(13);

            Assert.AreEqual("13th", result);
        }

        [Test]
        public void ConvertingFromOrdinal_WithZero_GetsCardinal()
        {
            var result = Ordinals.FromOrdinal("0th");

            Assert.AreEqual(0, result);
        }

        [Test]
        public void ConvertingFromOrdinal_With353_GetsCardinal()
        {
            var result = Ordinals.FromOrdinal("353rd");

            Assert.AreEqual(353, result);
        }

        [Test]
        public void ConvertingFromOrdinal_With1Or2Or3Or4_GetsCardinal()
        {
            var result1 = Ordinals.FromOrdinal("1st");
            var result2 = Ordinals.FromOrdinal("2nd");
            var result3 = Ordinals.FromOrdinal("3rd");
            var result4 = Ordinals.FromOrdinal("4th");

            Assert.AreEqual(1, result1);
            Assert.AreEqual(2, result2);
            Assert.AreEqual(3, result3);
            Assert.AreEqual(4, result4);
        }

        [Test]
        public void ConvertingFromOrdinal_With13_GetsCardinal()
        {
            var result = Ordinals.FromOrdinal("13th");

            Assert.AreEqual(13, result);
        }

        [Test]
        public void ConvertingFromOrdinal_WithNull_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => Ordinals.FromOrdinal(null));
        }

        [Test]
        public void ConvertingFromOrdinal_WithNegative_ReturnsOriginalNumber()
        {
            var expected = -12;
            var result = Ordinals.FromOrdinal("-12");

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ConvertingFromOrdinal_WhenZero_ReturnsZero()
        {
            var expected = 0;
            var result = Ordinals.FromOrdinal("0");

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ConvertingFromOrdinal_ThatsTooShort_ThrowsFormatException()
        {
            Assert.Throws<FormatException>(() => Ordinals.FromOrdinal("1"));
        }
    }
}
