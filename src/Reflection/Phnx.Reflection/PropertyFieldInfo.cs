using System;
using System.Reflection;

namespace Phnx.Reflection
{
    /// <summary>
    /// Reflection options to group <see cref="PropertyInfo"/> and <see cref="FieldInfo"/> under a common wrapper, as <see cref="GetValue"/>, <see cref="SetValue"/> and other options are shared
    /// </summary>
    public class PropertyFieldInfo
    {
        /// <summary>
        /// Whether this is a <see cref="PropertyInfo"/>
        /// </summary>
        public bool IsProperty => Property != null;

        /// <summary>
        /// The <see cref="PropertyInfo"/> that this represents. If this represents a <see cref="FieldInfo"/>, this is <see langword="null"/>
        /// </summary>
        protected PropertyInfo Property { get; set; }

        /// <summary>
        /// The <see cref="FieldInfo"/> that this represents. If this represents a <see cref="PropertyInfo"/>, this is <see langword="null"/>
        /// </summary>
        protected FieldInfo Field { get; set; }

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
        /// Create a new, unassigned <see cref="PropertyFieldInfo"/>
        /// </summary>
        protected PropertyFieldInfo()
        {
        }

        /// <summary>
        /// Create a new <see cref="PropertyFieldInfo"/> from a <see cref="MemberInfo"/>
        /// </summary>
        /// <param name="member">The meber (must be either a <see cref="PropertyInfo"/> or a <see cref="FieldInfo"/>)</param>
        /// <exception cref="ArgumentException">The member is not a property or field</exception>
        public PropertyFieldInfo(MemberInfo member)
        {
            LoadMember(member);
        }

        /// <summary>
        /// Create a new <see cref="PropertyFieldInfo"/> from a <see cref="FieldInfo"/>
        /// </summary>
        /// <param name="field">The field</param>
        public PropertyFieldInfo(FieldInfo field)
        {
            Field = field;
        }

        /// <summary>
        /// Create a new <see cref="PropertyFieldInfo"/> from a <see cref="PropertyInfo"/>
        /// </summary>
        /// <param name="property">The property</param>
        public PropertyFieldInfo(PropertyInfo property)
        {
            Property = property;
        }

        /// <summary>
        /// Loads a <see cref="MemberInfo"/> onto either <see cref="Property"/> or <see cref="Field"/> depending on whichever it represents
        /// </summary>
        /// <param name="member">The member to load</param>
        /// <exception cref="ArgumentException">The member is not a property or field</exception>
        protected void LoadMember(MemberInfo member)
        {
            switch (member)
            {
                case PropertyInfo property:
                    Property = property;
                    break;
                case FieldInfo field:
                    Field = field;
                    break;
                default:
                    string msg = ErrorMessage.Factory.MemberIsNotPropertyOrField();
                    throw new ArgumentException(msg, nameof(member));
            }
        }

        /// <summary>
        /// Set this member's value on an object
        /// </summary>
        /// <param name="baseObject">The base object to set the member of</param>
        /// <param name="valueToAssign">The value to assign to the member</param>
        /// <exception cref="ArgumentException">The property's <see langword="set" /> accessor is not found</exception>
        /// <exception cref="TargetException"> The member is an instance member but <paramref name="baseObject" /> is <see langword="null" /></exception>
        /// <exception cref="MethodAccessException">There was an illegal attempt to access a private or protected method inside a class</exception>
        /// <exception cref="TargetInvocationException">An error occurred while setting the property value. The <see cref="Exception.InnerException" /> property indicates the reason for the error</exception>
        public void SetValue(object baseObject, object valueToAssign)
        {
            if (IsProperty)
            {
                Property.SetValue(baseObject, valueToAssign);
            }
            else
            {
                Field.SetValue(baseObject, valueToAssign);
            }
        }

        /// <summary>
        /// Get this member's value from an object
        /// </summary>
        /// <param name="baseObject">The base object to get the member of</param>
        /// <returns>The value of the member</returns>
        /// <exception cref="ArgumentException">The property's <see langword="get" /> accessor is not found</exception>
        /// <exception cref="TargetException">In the .NET for Windows Store apps or the Portable Class Library, catch <see cref="Exception" /> instead. The member is an instance member but <paramref name="baseObject" /> is <see langword="null" /></exception>
        /// <exception cref="MethodAccessException">There was an illegal attempt to access a private or protected method inside a class</exception>
        /// <exception cref="TargetInvocationException">An error occurred while retrieving the property value. The <see cref="Exception.InnerException" /> property indicates the reason for the error</exception>
        public object GetValue(object baseObject)
        {
            if (IsProperty)
            {
                return Property.GetValue(baseObject);
            }
            else
            {
                return Field.GetValue(baseObject);
            }
        }

        /// <summary>
        /// Converts this <see cref="PropertyFieldInfo"/> to a <see cref="PropertyInfo"/>
        /// </summary>
        /// <param name="propFieldInfo">The <see cref="PropertyFieldInfo"/> to convert</param>
        /// <exception cref="InvalidCastException">The member was not a property</exception>
        public static explicit operator PropertyInfo(PropertyFieldInfo propFieldInfo)
        {
            var prop = propFieldInfo.Property;

            if (!propFieldInfo.IsProperty)
            {
                string err = ErrorMessage.Factory.InvalidCast(propFieldInfo.Name, typeof(PropertyFieldInfo),
                    typeof(PropertyInfo));

                throw new InvalidCastException(err);
            }

            return prop;
        }

        /// <summary>
        /// Converts this <see cref="PropertyFieldInfo"/> to a <see cref="FieldInfo"/>
        /// </summary>
        /// <param name="propFieldInfo">The <see cref="PropertyFieldInfo"/> to convert</param>
        /// <exception cref="InvalidCastException">The member was not a field</exception>
        public static explicit operator FieldInfo(PropertyFieldInfo propFieldInfo)
        {
            var field = propFieldInfo.Field;

            if (propFieldInfo.IsProperty)
            {
                string err = ErrorMessage.Factory.InvalidCast(propFieldInfo.Name, typeof(PropertyFieldInfo),
                    typeof(FieldInfo));

                throw new InvalidCastException(err);
            }

            return field;
        }

        /// <summary>
        /// Converts this <see cref="PropertyFieldInfo"/> to a <see cref="FieldInfo"/>
        /// </summary>
        /// <param name="propFieldInfo">The <see cref="PropertyFieldInfo"/> to convert</param>
        public static explicit operator MemberInfo(PropertyFieldInfo propFieldInfo)
        {
            return propFieldInfo.Member;
        }
    }
}
