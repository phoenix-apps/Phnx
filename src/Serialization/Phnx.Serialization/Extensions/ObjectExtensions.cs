using Newtonsoft.Json;
using System.Reflection;

namespace Phnx.Serialization
{
    /// <summary>
    /// Extensions for <see cref="object"/> related to serialization
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Deep clones an object
        /// </summary>
        /// <typeparam name="T">The type of object to serialize</typeparam>
        /// <param name="valueToCopy">The object to serialize</param>
        /// <returns>A deep copy of <paramref name="valueToCopy"/></returns>
        /// <remarks>This works by copying the entire object to JSON, and then copying it back from JSON to a new object</remarks>
        public static T DeepCopy<T>(this T valueToCopy)
        {
            string jsonObject = JsonConvert.SerializeObject(valueToCopy);

            return JsonConvert.DeserializeObject<T>(jsonObject);
        }

        /// <summary>
        /// Shallow clones an object, copying its members
        /// </summary>
        /// <typeparam name="T">The type of object to shallow clone</typeparam>
        /// <param name="valueToCopy">The object to shallow clone</param>
        /// <returns>A shallow copy of <paramref name="valueToCopy"/></returns>
        public static T ShallowCopy<T>(this T valueToCopy)
        {
            var cloneMethod = typeof(T).GetMethod("MemberwiseClone", BindingFlags.Instance | BindingFlags.NonPublic);

            var clone = (T)cloneMethod.Invoke(valueToCopy, new object[0]);

            return clone;
        }
    }
}
