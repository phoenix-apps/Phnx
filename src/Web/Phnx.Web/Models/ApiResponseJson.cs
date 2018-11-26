using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace Phnx.Web.Models
{
    /// <summary>
    /// Represents an <see cref="ApiResponse"/> with a JSON format body content
    /// </summary>
    /// <typeparam name="T">The type of data in the JSON body</typeparam>
    public class ApiResponseJson<T> : ApiResponse
    {
        /// <summary>
        /// Create a <see cref="ApiResponseJson{T}"/> from a <see cref="HttpResponseMessage"/>
        /// </summary>
        /// <param name="message">The message to create the response from</param>
        public ApiResponseJson(HttpResponseMessage message) : base(message)
        {
        }

        /// <summary>
        /// Load and deserialize the body to <typeparamref name="T"/>
        /// </summary>
        public async Task<T> GetBodyAsync()
        {
            var bodyString = await GetBodyAsStringAsync();

            return JsonConvert.DeserializeObject<T>(bodyString);
        }
    }
}