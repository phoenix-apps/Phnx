using System;

namespace MarkSFrancis.Extensions
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
    /// <exception cref="T:System.ArgumentNullException"><paramref name="type">type</paramref> is null.</exception>
    /// <exception cref="T:System.ArgumentException"><paramref name="type">type</paramref> is not a RuntimeType.   -or-  <paramref name="type">type</paramref> is an open generic type (that is, the <see cref="P:System.Type.ContainsGenericParameters"></see> property returns true).</exception>
    /// <exception cref="T:System.NotSupportedException"><paramref name="type">type</paramref> cannot be a <see cref="T:System.Reflection.Emit.TypeBuilder"></see>.   -or-   Creation of <see cref="T:System.TypedReference"></see>, <see cref="T:System.ArgIterator"></see>, <see cref="T:System.Void"></see>, and <see cref="T:System.RuntimeArgumentHandle"></see> types, or arrays of those types, is not supported.   -or-   The assembly that contains <paramref name="type">type</paramref> is a dynamic assembly that was created with <see cref="F:System.Reflection.Emit.AssemblyBuilderAccess.Save"></see>.   -or-   The constructor that best matches <paramref name="args">args</paramref> has varargs arguments.</exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">The constructor being called throws an exception.</exception>
    /// <exception cref="T:System.MethodAccessException">
    /// The caller does not have permission to call this constructor.</exception>
    /// <exception cref="T:System.MemberAccessException">Cannot create an instance of an abstract class, or this member was invoked with a late-binding mechanism.</exception>
    /// <exception cref="T:System.Runtime.InteropServices.InvalidComObjectException">The COM type was not obtained through <see cref="o:System.Type.GetTypeFromProgID"></see> or <see cref="o:System.Type.GetTypeFromCLSID"></see>.</exception>
    /// <exception cref="T:System.MissingMethodException">
    /// In the [.NET for Windows Store apps](http://go.microsoft.com/fwlink/?LinkID=247912) or the [Portable Class Library](~/docs/standard/cross-platform/cross-platform-development-with-the-portable-class-library.md), catch the base class exception, <see cref="T:System.MissingMemberException"></see>, instead.
    /// 
    /// No matching public constructor was found.</exception>
    /// <exception cref="T:System.Runtime.InteropServices.COMException"><paramref name="type">type</paramref> is a COM object but the class identifier used to obtain the type is invalid, or the identified class is not registered.</exception>
    /// <exception cref="T:System.TypeLoadException"><paramref name="type">type</paramref> is not a valid type.</exception>
        public static object CreateInstance(this Type type, params object[] args)
        {
            return Activator.CreateInstance(type, args);
        }
    }
}