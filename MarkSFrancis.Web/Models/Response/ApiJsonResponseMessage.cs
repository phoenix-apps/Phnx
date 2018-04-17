using Newtonsoft.Json;
using System.Net.Http;

namespace MarkSFrancis.Web.Models.Response
{
    public class ApiJsonResponseMessage<T> : ApiResponseMessage
    {
        public ApiJsonResponseMessage(HttpResponseMessage msg) : base(msg)
        {
        }

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