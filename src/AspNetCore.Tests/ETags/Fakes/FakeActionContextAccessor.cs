using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Routing;

namespace Phnx.AspNetCore.ETags.Tests.Fakes
{
    public class FakeActionContextAccessor : IActionContextAccessor
    {
        public FakeActionContextAccessor()
        {
            ActionContext = new ActionContext(new DefaultHttpContext(), new RouteData(), new ActionDescriptor());
        }

        public ActionContext ActionContext { get; set; }
    }
}
