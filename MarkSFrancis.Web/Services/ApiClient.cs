using MarkSFrancis.Web.Client;
using MarkSFrancis.Web.Fluent;
using MarkSFrancis.Web.Services.Interfaces;

namespace MarkSFrancis.Web.Services
{
    public class ApiClient : IApiClient
    {
        private readonly ApiClientCore _apiClientCore;

        public ApiClient()
        {
            _apiClientCore = new ApiClientCore();
        }

        public FluentRequest CreateRequest(string url, params string[] urlSegments)
        {
            return new FluentRequest(_apiClientCore)
                .UseUrl(url, urlSegments);
        }
    }
}
