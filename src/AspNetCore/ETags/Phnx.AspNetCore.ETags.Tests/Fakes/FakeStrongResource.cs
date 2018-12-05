using System;
using System.ComponentModel.DataAnnotations;

namespace Phnx.AspNetCore.ETags.Tests.Fakes
{
    public class FakeStrongResource
    {
        public FakeStrongResource(string concurrencyStamp)
        {
            ConcurrencyStamp = concurrencyStamp;
        }

        [ConcurrencyCheck]
        public string ConcurrencyStamp { get; }
    }
}
