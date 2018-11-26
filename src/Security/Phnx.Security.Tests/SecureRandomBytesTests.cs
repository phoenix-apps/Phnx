using NUnit.Framework;

namespace Phnx.Security.Tests
{
    public class SecureRandomBytesTests
    {
        [Test]
        public void SecureRandomBytes_5Bytes_GetsArrayOf5Bytes()
        {
            var results = SecureRandomBytes.Generate(5);

            Assert.AreEqual(5, results.Length);
        }

        [Test]
        public void SecureRandomBytes_0Bytes_GetsEmptyArray()
        {
            var results = SecureRandomBytes.Generate(0);

            Assert.AreEqual(0, results.Length);
        }

        [Test]
        public void SecureRandomBytes_Minus1Bytes_ThrowsArgumentLessThanZero()
        {
            Assert.Throws<ArgumentLessThanZeroException>(() => SecureRandomBytes.Generate(-1));
        }
    }
}
