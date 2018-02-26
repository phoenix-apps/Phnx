using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace MarkSFrancis.IO.Json
{
    public static class JObjectConverter
    {
        public static JObject FromObject(object data)
        {
            return JObject.FromObject(data);
        }

        public static JObject FromJson(string json)
        {
            return JObject.Parse(json);
        }

        public static JObject FromPropertyDictionary(Dictionary<string, string> propertyDictionary)
        {
            return JsonWrapper.Wrap(propertyDictionary);
        }

        public static T ToObject<T>(JObject jObject)
        {
            return jObject.ToObject<T>();
        }

        public static string ToJson(JObject jObject)
        {
            return jObject.ToString();
        }

        public static Dictionary<string, string> ToPropertyDictionary(JObject jObj)
        {
            return JsonWrapper.Unwrap(jObj);
        }
    }
}
