using System;
using System.ComponentModel.DataAnnotations;

namespace Phnx.AspNetCore.Rest.Tests.Fakes
{
    public class BrokenFakeResource
    {
        [ConcurrencyCheck]
        public string Token => throw new NotImplementedException();
    }
}
