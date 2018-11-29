using Microsoft.Extensions.DependencyInjection;
using Phnx.AspNetCore.Rest.Setup;

namespace Phnx.AspNetCore.Rest
{
    /// <summary>
    /// Extentions for <see cref="IServiceCollection"/> related to <see cref="Rest"/>
    /// </summary>
    public static class RestServiceBuilderExtensions
    {
        /// <summary>
        /// Setup all necessary components for <see cref="Rest"/>
        /// </summary>
        /// <param name="services">The service injector</param>
        public static void AddRest(this IServiceCollection services)
        {
            var builder = new RestServiceBuilder(services);

            builder.RegisterETags();
            builder.RegisterContextHelpers();
            builder.RegisterRestHelpers();
        }
    }
}
