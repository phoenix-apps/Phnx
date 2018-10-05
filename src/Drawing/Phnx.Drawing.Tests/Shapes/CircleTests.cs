using NUnit.Framework;
using Phnx.Drawing.Shapes;
using System.Linq;

namespace Phnx.Drawing.Tests.Shapes
{
    public class CircleTests
    {
        [Test]
        public void Circle_IsATypeOfShape()
        {
            var interfaces = typeof(Circle).GetInterfaces();
            var isIShape = interfaces.Contains(typeof(IShape));
            Assert.IsTrue(isIShape);
        }
    }
}
