using Phnx.Tests.Fakes.TypeFakes;
using NUnit.Framework;

namespace Phnx.Tests.Extensions.ObjectExtensionsTests
{
    public class ChainTests
    {
        [Test]
        public void ChainingAction_ExecutesAndReturnsNewVersion()
        {
            int startingId = 57;

            var p = new ParentClass
            {
                Id = startingId
            };

            p.Chain(ChainMe).Chain(ChainMe);

            Assert.AreEqual(startingId += 2, p.Id);
        }

        private void ChainMe(ParentClass input)
        {
            ++input.Id;
        }
    }
}
