using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Phnx.Core.Configuration.Tests
{
    public static class FakeConfigurationFactory
    {
        public static IConfiguration Create(Dictionary<string, string> configuration)
        {
            return new ConfigurationBuilder()
                .AddInMemoryCollection(configuration)
                .Build();
        }
    }
}
