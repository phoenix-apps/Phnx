using System;
using System.ComponentModel.DataAnnotations;

namespace Phnx.AspNetCore.ETags.Tests.Fakes
{
    public class BrokenFakeResource
    {
        [ConcurrencyCheck]
        public string Token => throw new NotImplementedException();
    }
}
