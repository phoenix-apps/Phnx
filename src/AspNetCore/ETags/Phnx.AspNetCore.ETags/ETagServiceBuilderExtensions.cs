using Microsoft.Extensions.DependencyInjection;
using Phnx.AspNetCore.ETags.Setup;

namespace Phnx.AspNetCore.ETags
{
    /// <summary>
    /// Extentions for <see cref="IServiceCollection"/> related to <see cref="ETags"/>
    /// </summary>
    public static class ETagServiceBuilderExtensions
    {
        /// <summary>
        /// Setup all necessary components for <see cref="ETags"/>
        /// </summary>
        /// <param name="services">The service injector</param>
        public static IServiceCollection AddETags(this IServiceCollection services)
        {
            var builder = new ETagServiceBuilder(services);

            builder.RegisterETags();
            builder.RegisterContextHelpers();
            builder.RegisterETagHelpers();

            return services;
        }
    }
}
