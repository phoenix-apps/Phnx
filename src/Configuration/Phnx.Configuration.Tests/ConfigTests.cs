using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Phnx.Configuration.Tests
{
    public class ConfigTests
    {
        [Test]
        public void GetString_GetsValue()
        {
            string key = "asdf", expected = "val";

            var fake = FakeConfigurationFactory.Create(new Dictionary<string, string>
            {
                { key, expected }
            });

            var config = new Config(fake);

            var result = config.Get<string>(key);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetInt_WhenValueIsInt_GetsValue()
        {
            var key = "asdfhnf:asdfh";
            var expected = 1202;

            var fake = FakeConfigurationFactory.Create(new Dictionary<string, string>
            {
                { key, expected.ToString() }
            });

            var config = new Config(fake);

            var result = config.Get<int>(key);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetComplexType_WithoutConverter_ThrowsInvalidCastException()
        {
            var key = "_";

            var fake = FakeConfigurationFactory.Create(new Dictionary<string, string>
            {
                { key, "a" }
            });

            var config = new Config(fake);

            Assert.Throws<InvalidCastException>(() => config.Get<Config>(key));
        }

        [Test]
        public void GetString_WhenValueIsNull_GetsNull()
        {
            string key = "_", expected = null;

            var fake = FakeConfigurationFactory.Create(new Dictionary<string, string>
            {
                { key, expected }
            });

            var config = new Config(fake);

            var result = config.Get<string>(key);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetInt_WhenValueIsNull_ThrowsInvalidCastException()
        {
            string key = "_", expected = null;

            var fake = FakeConfigurationFactory.Create(new Dictionary<string, string>
            {
                { key, expected }
            });

            var config = new Config(fake);

            Assert.Throws<InvalidCastException>(() => config.Get<int>(key));
        }

        [Test]
        public void GetInt_WhenValueIsText_ThrowsInvalidCastException()
        {
            string key = "_", expected = "asdf";

            var fake = FakeConfigurationFactory.Create(new Dictionary<string, string>
            {
                { key, expected }
            });

            var config = new Config(fake);

            Assert.Throws<InvalidCastException>(() => config.Get<int>(key));
        }
    }
}
