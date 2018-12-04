using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;

namespace Phnx.AspNetCore.ETags.Tests.Fakes
{
    public class FakeHeaderDictionary : Dictionary<string, StringValues>, IHeaderDictionary
    {
        public long? ContentLength
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }
    }
}
