using Microsoft.Extensions.DependencyInjection;
using Phnx.AspNetCore.Rest.Services;
using Phnx.AspNetCore.Rest.Setup;

namespace Phnx.AspNetCore.Rest
{
    /// <summary>
    /// Extentions for <see cref="IServiceCollection"/> related to <see cref="Rest"/>
    /// </summary>
    public static class RestServiceBuilderExtensions
    {
        /// <summary>
        /// Setup all necessary components for <see cref="Rest"/>. To use <see cref="RestControllerHelperService{TDataModel, TDtoModel, TPatchDtoModel}"/>, you'll need to inject your own implementation of <see cref="IResourceMapService{TDataModel, TDtoModel, TPatchDtoModel}"/> for the same types
        /// </summary>
        /// <param name="services">The service injector</param>
        public static IServiceCollection AddRest(this IServiceCollection services)
        {
            var builder = new RestServiceBuilder(services);

            builder.RegisterETags();
            builder.RegisterContextHelpers();
            builder.RegisterRestHelpers();

            return services;
        }
    }
}
