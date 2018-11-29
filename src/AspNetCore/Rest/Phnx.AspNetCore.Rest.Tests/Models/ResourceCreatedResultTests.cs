using NUnit.Framework;
using Phnx.AspNet.Core.Rest.Models;

namespace Phnx.AspNetCore.Rest.Tests.Models
{
    public class ResourceCreatedResultTests
    {
        [Test]
        public void New_WithUrl_SetsByUrlToTrue()
        {
            var createdResult = new ResourceCreatedResult("test");

            Assert.IsTrue(createdResult.ByUrl);
        }

        [Test]
        public void New_WithUrl_SetsUrl()
        {
            string url = "test";
            var createdResult = new ResourceCreatedResult(url);

            Assert.AreEqual(url, createdResult.Url);
        }

        [Test]
        public void New_WithActionControllerRoute_SetsByUrlToFalse()
        {
            var createdResult = new ResourceCreatedResult("test", "test", new object());

            Assert.IsFalse(createdResult.ByUrl);
        }

        [Test]
        public void New_WithNullActionControllerRoute_SetsByUrlToFalse()
        {
            var createdResult = new ResourceCreatedResult(null, null, null);

            Assert.IsFalse(createdResult.ByUrl);
        }

        [Test]
        public void New_WithActionControllerRoute_SetsValues()
        {
            string action = "act";
            string controller = "cont";
            object values = new
            {
                a = "b"
            };

            var createdResult = new ResourceCreatedResult(action, controller, values);

            Assert.AreEqual(action, createdResult.ActionName);
            Assert.AreEqual(controller, createdResult.ControllerName);
            Assert.AreEqual(values, createdResult.RouteValues);
        }

        [Test]
        public void New_WithNullActionControllerRoute_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => new ResourceCreatedResult(null, null, null));
        }
    }
}
