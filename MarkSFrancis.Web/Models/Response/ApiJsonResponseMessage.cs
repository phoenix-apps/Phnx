using Newtonsoft.Json;
using System.Net.Http;

namespace MarkSFrancis.Web.Models.Response
{
    /// <summary>
    /// Represents an <see cref="ApiResponseMessage"/> with a JSON format body content
    /// </summary>
    /// <typeparam name="T">The type of data in the JSON body</typeparam>
    public class ApiJsonResponseMessage<T> : ApiResponseMessage
    {
        /// <summary>
        /// Create a <see cref="ApiJsonResponseMessage{T}"/> from a <see cref="HttpResponseMessage"/>
        /// </summary>
        /// <param name="msg"></param>
        public ApiJsonResponseMessage(HttpResponseMessage msg) : base(msg)
        {
        }

        /// <summary>
        /// Load and deserialize the body to <typeparamref name="T"/>
        /// </summary>
        public T BodyDeserialized
        {
            get
            {
                if (string.IsNullOrEmpty(Body))
                {
                    return default(T);
                }

                return JsonConvert.DeserializeObject<T>(Body);
            }
        }
    }
}