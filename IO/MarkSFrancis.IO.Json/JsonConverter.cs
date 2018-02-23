using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MarkSFrancis.IO.Json
{
    public static class JsonConverter
    {
        public static T ToObject<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
        
        internal static JObject ToJObject(object data)
        {
            return JObject.FromObject(data);
        }
        
        public static string ToJson(object data)
        {
            return JsonConvert.SerializeObject(data);
        }
    }
}
