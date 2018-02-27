using System.Reflection;
using MarkSFrancis.IO.Factory;
using MarkSFrancis.Windows.Serialization;

namespace MarkSFrancis.Windows.Extensions.Reflection
{
    /// <summary>
    /// Extensions for <see cref="object"/>
    /// </summary>
    public static class ObjectExtensions
    {
        private static readonly MemoryStreamFactory _streamFactory = new MemoryStreamFactory();
        private const string MemberwiseCloneMethodName = "MemberwiseClone";

        /// <summary>
        /// Deep clones an object. <typeparamref name="T"/> must be marked as <see cref="System.SerializableAttribute"/>
        /// </summary>
        /// <typeparam name="T">The type of object to serialize</typeparam>
        /// <param name="valueToCopy">The object to serialize</param>
        /// <returns>A deep copy of <paramref name="valueToCopy"/></returns>
        public static T DeepCopy<T>(this T valueToCopy)
        {
            var serializer = new BinarySerializer();

            using (var stream = _streamFactory.Create())
            {
                serializer.Serialize(valueToCopy, stream);

                stream.Position = 0;

                return serializer.Deserialize<T>(stream);
            }
        }

        /// <summary>
        /// Shallow clones an object, copying its members
        /// </summary>
        /// <typeparam name="T">The type of object to shallow clone</typeparam>
        /// <param name="valueToCopy">The object to shallow clone</param>
        /// <returns>A shallow copy of <paramref name="valueToCopy"/></returns>
        public static T ShallowCopy<T>(this T valueToCopy)
        {
            MethodInfo method = valueToCopy.GetType().GetMethod(MemberwiseCloneMethodName,
                BindingFlags.NonPublic | BindingFlags.Instance);

            if (method == null)
            {
                throw ErrorFactory.Default.OperationInvalidException($"The {MemberwiseCloneMethodName} method is missing for the type {valueToCopy.GetType().FullName}, and therefore it cannot be shallow cloned");
            }
            
            return (T)method.Invoke(valueToCopy, null);
        }
    }
}