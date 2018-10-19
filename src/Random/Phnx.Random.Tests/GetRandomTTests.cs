using NUnit.Framework;
using Phnx.Random.Generator;
using Phnx.Random.Tests.Fakes;
using System;

namespace Phnx.Random.Tests
{
    public class GetRandomTTests
    {
        [Test]
        public void GetT_Deep_PopulatesChildProperties()
        {
            var randomT = RandomTGenerator.Get<DeepFake>(false);

            Assert.IsNotNull(randomT);
            Assert.AreNotEqual(Guid.Empty, randomT.Id);
            Assert.IsNotNull(randomT.Collection);
            Assert.IsNotNull(randomT.Single);
            Assert.IsNotNull(randomT.Recursive);
        }

        [Test]
        public void GetT_Shallow_PopulatesOnlyCLRProperties()
        {
            var randomT = RandomTGenerator.Get<DeepFake>(true);

            Assert.IsNotNull(randomT);
            Assert.AreNotEqual(Guid.Empty, randomT.Id);
            Assert.IsNull(randomT.Collection);
            Assert.IsNull(randomT.Single);
            Assert.IsNull(randomT.Recursive);
        }
    }
}
