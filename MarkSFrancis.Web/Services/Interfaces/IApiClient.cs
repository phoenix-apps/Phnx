using MarkSFrancis.Web.Fluent;

namespace MarkSFrancis.Web.Services.Interfaces
{
    public interface IApiClient
    {
        FluentRequest CreateRequest(string url, params string[] urlSegments);
    }
}