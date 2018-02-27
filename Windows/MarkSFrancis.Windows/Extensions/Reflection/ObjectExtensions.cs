using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using MarkSFrancis.Windows.Extensions;

namespace MarkSFrancis.Windows.Extensions.Reflection
{
    /// <summary>
    /// Extensions for <see cref="object"/>
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Deep clones an object. <typeparamref name="T"/> must be marked as <see cref="System.SerializableAttribute"/>
        /// </summary>
        /// <typeparam name="T">The type of object to serialize</typeparam>
        /// <param name="valueToCopy">The object to serialize</param>
        /// <returns></returns>
        public static T DeepCopy<T>(this T valueToCopy)
        {
            IFormatter formatter = new BinaryFormatter();

            using (var stream = new MemoryStream())
            {
                valueToCopy.SerializeBinary(stream);
                stream.Seek(0, SeekOrigin.Begin);
                return stream.DeserializeBinary<T>();
            }
        }

        /// <summary>
        /// Shallow clones an object, copying its memebers
        /// </summary>
        /// <typeparam name="T">The type of object to shallow clone</typeparam>
        /// <param name="valueToCopy">The object to shallow clone</param>
        /// <returns></returns>
        public static T ShallowCopy<T>(this T valueToCopy)
        {
            MethodInfo method = valueToCopy.GetType().GetMethod("MemberwiseClone",
                BindingFlags.NonPublic | BindingFlags.Instance);

            if (method == null)
            {
                ErrorFactory.Default.OperationInvalidException("The MemberwiseClone method is missing for the type " + valueToCopy.GetType().FullName);
            }

            // ReSharper disable once PossibleNullReferenceException, cannot be null
            return (T)method.Invoke(valueToCopy, null);
        }
    }
}