using System;

namespace Phnx
{
    /// <summary>
    /// Extensions for <see cref="Type"/>
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>Creates an instance of the specified type using the constructor that best matches the specified parameters.</summary>
        /// <param name="type">The type of object to create.</param>
        /// <param name="args">An array of arguments that match in number, order, and type the parameters of the constructor to invoke. If args is an empty array or null, the constructor that takes no parameters (the default constructor) is invoked.</param>
        /// <returns>A reference to the newly created object.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="type"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="type"/> is not a RuntimeType or <paramref name="type"/> is an open generic type (that is, the <see cref="Type.ContainsGenericParameters"/> property returns <see langword="true"/>).</exception>
        /// <exception cref="NotSupportedException"><paramref name="type"/> cannot be a <see cref="T:System.Reflection.Emit.TypeBuilder"/> or creation of <see cref="TypedReference"/>, <see cref="T:System.ArgIterator"/>, <see cref="Void"/>, and <see cref="RuntimeArgumentHandle"/> types, or arrays of those types, is not supported, or the assembly that contains <paramref name="type"/> is a dynamic assembly that was created with <see cref="F:System.Reflection.Emit.AssemblyBuilderAccess.Save"/>, or the constructor that best matches <paramref name="args"/> has varargs arguments.</exception>
        /// <exception cref="System.Reflection.TargetInvocationException">The constructor being called throws an exception.</exception>
        /// <exception cref="MethodAccessException">
        /// The caller does not have permission to call this constructor.</exception>
        /// <exception cref="MemberAccessException">Cannot create an instance of an abstract class, or this member was invoked with a late-binding mechanism.</exception>
        /// <exception cref="System.Runtime.InteropServices.InvalidComObjectException">The COM type was not obtained through <see cref="Type.GetTypeFromProgID(string)"/> or <see cref="Type.GetTypeFromCLSID(Guid)"/>.</exception>
        /// <exception cref="MissingMethodException">
        /// In the [.NET for Windows Store apps](http://go.microsoft.com/fwlink/?LinkID=247912) or the [Portable Class Library](~/docs/standard/cross-platform/cross-platform-development-with-the-portable-class-library.md), catch the base class exception, <see cref="MissingMemberException"/>, instead.
        ///
        /// No matching public constructor was found.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException"><paramref name="type"/> is a COM object but the class identifier used to obtain the type is invalid, or the identified class is not registered.</exception>
        /// <exception cref="TypeLoadException"><paramref name="type"/> is not a valid type.</exception>
        public static object CreateInstance(this Type type, params object[] args)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return Activator.CreateInstance(type, args);
        }
    }
}