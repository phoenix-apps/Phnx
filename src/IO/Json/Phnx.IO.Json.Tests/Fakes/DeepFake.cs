using System;
using System.Collections.Generic;

namespace Phnx.IO.Json.Tests.Fakes
{
    public class DeepFake
    {
        public ShallowFake Single { get; set; }

        public List<ShallowFake> Collection { get; set; }
    }
}
