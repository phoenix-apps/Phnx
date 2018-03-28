using System;
using System.Linq.Expressions;
using System.Reflection;

namespace MarkSFrancis.Reflection
{
    /// <summary>
    /// Reflection options to group <see cref="PropertyInfo"/> and <see cref="FieldInfo"/> under a common wrapper, as <see cref="GetValue"/>, <see cref="SetValue"/> and other options are shared
    /// </summary>
    /// <typeparam name="T">The type that this <see cref="PropertyInfo"/> or <see cref="FieldInfo"/> belongs to</typeparam>
    /// <typeparam name="U">The type of this <see cref="PropertyInfo"/> or <see cref="FieldInfo"/></typeparam>
    public class PropertyFieldInfo<T, U>
    {
        /// <summary>
        /// Whether this is a <see cref="PropertyInfo"/>
        /// </summary>
        public bool IsProperty => Property != null;
        private PropertyInfo Property { get; }

        private FieldInfo Field { get; }

        /// <summary>
        /// Get the <see cref="Member"/> for this
        /// </summary>
        public MemberInfo Member => (MemberInfo)Property ?? Field;

        /// <summary>
        /// Gets the name of this member
        /// </summary>
        public string Name => Property?.Name ?? Field?.Name;

        /// <summary>
        /// Gets the type of this member
        /// </summary>
        public Type Type => Property?.PropertyType ?? Field.FieldType;

        /// <summary>
        /// Whether you can get the member's value
        /// </summary>
        public bool CanGet => !IsProperty || Property.CanRead;

        /// <summary>
        /// Whether you can set the member's value
        /// </summary>
        public bool CanSet => IsProperty ? Property.CanWrite : !Field.IsInitOnly;

        /// <summary>
        /// Create a new <see cref="PropertyFieldInfo{T,U}"/> from a function expression
        /// </summary>
        /// <param name="expression">The expression that points to a property/ field</param>
        /// <exception cref="ArgumentException">The expression is not a property or field access</exception>
        public PropertyFieldInfo(Expression<Func<T, U>> expression)
        {
            MemberInfo member;
            if (expression.Body is MemberExpression memberExpression)
            {
                member = memberExpression.Member;
            }
            else
            {
                throw ErrorFactory.Default.ExpressionIsNotPropertyOrFieldAccess(nameof(expression));
            }

            var propField = LoadMember(member);
            Field = propField.Field;
            Property = propField.Property;
        }

        /// <summary>
        /// Create a new <see cref="PropertyFieldInfo{T,U}"/> from a <see cref="MemberInfo"/>
        /// </summary>
        /// <param name="member">The meber (must be either a <see cref="PropertyInfo"/> or a <see cref="FieldInfo"/>)</param>
        /// <exception cref="ArgumentException">The member is not a property or field</exception>
        public PropertyFieldInfo(MemberInfo member)
        {
            var propField = LoadMember(member);
            Field = propField.Field;
            Property = propField.Property;
        }

        /// <summary>
        /// Create a new <see cref="PropertyFieldInfo{T,U}"/> from a <see cref="FieldInfo"/>
        /// </summary>
        /// <param name="field">The field</param>
        public PropertyFieldInfo(FieldInfo field)
        {
            Field = field;
        }

        /// <summary>
        /// Create a new <see cref="PropertyFieldInfo{T,U}"/> from a <see cref="PropertyInfo"/>
        /// </summary>
        /// <param name="property">The property</param>
        public PropertyFieldInfo(PropertyInfo property)
        {
            Property = property;
        }

        /// <summary>
        /// Converts a <see cref="MemberInfo"/> to a <see cref="PropertyInfo"/> or <see cref="FieldInfo"/> depending on whichever it represents
        /// </summary>
        /// <param name="member">The member to convert</param>
        /// <returns>The property or field of the member. Whichever the member is, gets set, and whichever it is not is set to <see langword="null"/></returns>
        /// <exception cref="ArgumentException">The member is not a property or field</exception>
        private static (PropertyInfo Property, FieldInfo Field) LoadMember(MemberInfo member)
        {
            if (member is PropertyInfo property)
            {
                return (property, null);
            }

            if (member is FieldInfo field)
            {
                return (null, field);
            }

            throw ErrorFactory.Default.MemberIsNotPropertyOrField(nameof(member));
        }

        /// <summary>
        /// Set this member's value on an object
        /// </summary>
        /// <param name="baseObject">The base object to set the member of</param>
        /// <param name="valueToAssign">The value to assign to the member</param>
        /// <exception cref="ArgumentException">The property's <see langword="set" /> accessor is not found</exception>
        /// <exception cref="TargetException"> The member is an instance member but <paramref name="baseObject" /> is <see langword="null" /></exception>
        /// <exception cref="MethodAccessException">There was an illegal attempt to access a private or protected method inside a class</exception>
        /// <exception cref="TargetInvocationException">An error occurred while setting the property value. The <see cref="P:Exception.InnerException" /> property indicates the reason for the error</exception>
        public virtual void SetValue(T baseObject, U valueToAssign)
        {
            Property?.SetValue(baseObject, valueToAssign);

            Field?.SetValue(baseObject, valueToAssign);
        }

        /// <summary>
        /// Get this member's value from an object
        /// </summary>
        /// <param name="baseObject">The base object to get the member of</param>
        /// <returns>The value of the member</returns>
        /// <exception cref="ArgumentException">The property's <see langword="get" /> accessor is not found</exception>
        /// <exception cref="TargetException">In the .NET for Windows Store apps or the Portable Class Library, catch <see cref="Exception" /> instead. The member is an instance member but <paramref name="baseObject" /> is <see langword="null" /></exception>
        /// <exception cref="MethodAccessException">There was an illegal attempt to access a private or protected method inside a class</exception>
        /// <exception cref="TargetInvocationException">An error occurred while retrieving the property value. The <see cref="P:Exception.InnerException" /> property indicates the reason for the error</exception>
        public U GetValue(T baseObject)
        {
            return (U)(Property?.GetValue(baseObject) ?? Field?.GetValue(baseObject));
        }

        /// <summary>
        /// Converts this <see cref="PropertyFieldInfo{T,U}"/> to a <see cref="PropertyInfo"/>
        /// </summary>
        /// <param name="propFieldInfo">The <see cref="PropertyFieldInfo{T,U}"/> to convert</param>
        /// <exception cref="InvalidCastException">The member was not a property</exception>
        public static explicit operator PropertyInfo(PropertyFieldInfo<T, U> propFieldInfo)
        {
            var prop = propFieldInfo.Property;

            if (prop == null)
            {
                ErrorFactory.Default.InvalidCast(propFieldInfo.Name, typeof(PropertyFieldInfo<T, U>),
                    typeof(PropertyInfo));
            }
            return prop;
        }

        /// <summary>
        /// Converts this <see cref="PropertyFieldInfo{T,U}"/> to a <see cref="FieldInfo"/>
        /// </summary>
        /// <param name="propFieldInfo">The <see cref="PropertyFieldInfo{T,U}"/> to convert</param>
        /// <exception cref="InvalidCastException">The member was not a field</exception>
        public static explicit operator FieldInfo(PropertyFieldInfo<T, U> propFieldInfo)
        {
            var field = propFieldInfo.Field;

            if (field == null)
            {
                ErrorFactory.Default.InvalidCast(propFieldInfo.Name, typeof(PropertyFieldInfo<T, U>),
                    typeof(FieldInfo));
            }
            return field;
        }

        /// <summary>
        /// Converts this <see cref="PropertyFieldInfo{T,U}"/> to a <see cref="FieldInfo"/>
        /// </summary>
        /// <param name="propFieldInfo">The <see cref="PropertyFieldInfo{T,U}"/> to convert</param>
        public static explicit operator MemberInfo(PropertyFieldInfo<T, U> propFieldInfo)
        {
            return propFieldInfo.Member;
        }
    }

    /// <summary>
    /// Reflection options to group <see cref="PropertyInfo"/> and <see cref="FieldInfo"/> under a common wrapper when the type of the parent and child are not known at compile time
    /// </summary>
    public class PropertyFieldInfo : PropertyFieldInfo<object, object>
    {
        /// <summary>
        /// Create a new <see cref="PropertyFieldInfo"/> from a function expression
        /// </summary>
        /// <param name="expression">The expression that points to the property/ field</param>
        public PropertyFieldInfo(Expression<Func<object, object>> expression) : base(expression)
        {

        }

        /// <summary>
        /// Create a new <see cref="PropertyFieldInfo"/> from a <see cref="MemberInfo"/>
        /// </summary>
        /// <param name="member">The meber (must be either a <see cref="PropertyInfo"/> or a <see cref="FieldInfo"/>)</param>
        public PropertyFieldInfo(MemberInfo member) : base(member)
        {

        }

        /// <summary>
        /// Create a new <see cref="PropertyFieldInfo"/> from a <see cref="FieldInfo"/>
        /// </summary>
        /// <param name="field">The field</param>
        public PropertyFieldInfo(FieldInfo field) : base(field)
        {

        }
        /// <summary>
        /// Create a new <see cref="PropertyFieldInfo"/> from a <see cref="PropertyInfo"/>
        /// </summary>
        /// <param name="property">The property</param>

        public PropertyFieldInfo(PropertyInfo property) : base(property)
        {

        }
    }
}