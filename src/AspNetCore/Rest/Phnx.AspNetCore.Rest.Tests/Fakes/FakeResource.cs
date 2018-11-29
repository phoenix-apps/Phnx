using Phnx.AspNetCore.Rest.Models;

namespace Phnx.AspNetCore.Rest.Tests.Fakes
{
    public class FakeResource : IResourceDataModel
    {
        public FakeResource(string concurrencyStamp)
        {
            ConcurrencyStamp = concurrencyStamp;
        }

        public string ConcurrencyStamp { get; }
    }
}
