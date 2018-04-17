using Newtonsoft.Json;

namespace MarkSFrancis.Web.Models.Request
{
    public class ApiJsonRequestMessage<T> : ApiRequestMessage
    {
        public ApiJsonRequestMessage()
        {
            base.ContentType = Models.ContentType.Json;
        }

        public new T Content
        {
            get => JsonConvert.DeserializeObject<T>(base.Content);
            set => base.Content = JsonConvert.SerializeObject(value);
        }

        public new string ContentType => base.ContentType;
    }
}
