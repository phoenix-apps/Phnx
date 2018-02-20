using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace MarkSFrancis.Windows.Extensions.Reflection
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// Deep clones a given object. The given type MUST be serializable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
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

        public static T ShallowCopy<T>(this T t)
        {
            MethodInfo method = t.GetType().GetMethod("MemberwiseClone",
                BindingFlags.NonPublic | BindingFlags.Instance);

            // ReSharper disable once PossibleNullReferenceException, cannot be null
            return (T)method.Invoke(t, null);
        }
    }
}