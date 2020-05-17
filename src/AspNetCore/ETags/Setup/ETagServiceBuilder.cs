using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Phnx.AspNetCore.ETags.Services;

namespace Phnx.AspNetCore.ETags.Setup
{
    /// <summary>
    /// A service builder specifically for setting up <see cref="ETags"/> for injection
    /// </summary>
    internal class ETagServiceBuilder
    {
        /// <summary>
        /// The service collection for dependancy injection
        /// </summary>
        private readonly IServiceCollection services;

        internal ETagServiceBuilder(IServiceCollection services)
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

        public void RegisterETagHelpers()
        {
            services.AddScoped<IETagRequestService, ETagRequestService>();

            services.AddScoped<IETagResponseService, ETagResponseService>();
        }
    }
}
