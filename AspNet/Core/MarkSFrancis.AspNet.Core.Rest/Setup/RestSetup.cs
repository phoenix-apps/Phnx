using MarkSFrancis.AspNet.Core.Rest.Services;
using MarkSFrancis.AspNet.Core.Rest.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace MarkSFrancis.AspNet.Core.Rest.Setup
{
    /// <summary>
    /// A service builder specifically for setting up <see cref="Rest"/> for injection
    /// </summary>
    internal class RestServiceBuilder
    {
        /// <summary>
        /// The service collection for dependancy injection
        /// </summary>
        private readonly IServiceCollection services;

        internal RestServiceBuilder(IServiceCollection services)
        {
            this.services = services;
        }

        public void RegisterETags()
        {
            services.AddScoped<IETagService, ETagService>();
        }

        public void RegisterContextHelpers()
        {
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
        }

        public void RegisterRestHelpers()
        {
            services.AddScoped(typeof(IRestRequestService<>), typeof(RestRequestService<>));

            services.AddScoped(typeof(IRestResponseService<,,>), typeof(RestResponseService<,,>));

            services.AddScoped(typeof(IRestControllerHelperService<,,,>), typeof(RestControllerHelperService<,,,>));
        }
    }
}
