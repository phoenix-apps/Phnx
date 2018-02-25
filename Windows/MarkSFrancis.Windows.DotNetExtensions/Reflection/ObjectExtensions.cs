using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace MarkSFrancis.Windows.Extensions.Reflection
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// Deep clones an object. <typeparamref name="T"/> must be marked as serializable
        /// </summary>
        /// <typeparam name="T">The type of object to serialize</typeparam>
        /// <param name="t">The object to serialize</param>
        /// <returns></returns>
        public static T DeepCopy<T>(this T t)
        {
            IFormatter formatter = new BinaryFormatter();

            using (var stream = new MemoryStream())
            {
                formatter.Serialize(stream, t);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }
        }

        /// <summary>
        /// Shallow clones an object
        /// </summary>
        /// <typeparam name="T">The type of object to shallow clone</typeparam>
        /// <param name="t">The object to shallow clone</param>
        /// <returns></returns>
        public static T ShallowCopy<T>(this T t)
        {
            MethodInfo method = t.GetType().GetMethod("MemberwiseClone",
                BindingFlags.NonPublic | BindingFlags.Instance);

            // ReSharper disable once PossibleNullReferenceException, cannot be null
            return (T)method.Invoke(t, null);
        }
    }
}