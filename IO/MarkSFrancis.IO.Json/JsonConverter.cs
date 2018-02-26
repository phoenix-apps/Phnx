using Newtonsoft.Json;

namespace MarkSFrancis.IO.Json
{
    public static class JsonConverter
    {
        public static T ToObject<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
        
        public static string ToJson(object data)
        {
            return JsonConvert.SerializeObject(data);
        }
    }
}
