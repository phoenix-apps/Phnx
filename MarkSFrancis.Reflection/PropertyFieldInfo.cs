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
        public bool IsProperty => Property == null;

        /// <summary>
        /// Convert this to a <see cref="PropertyInfo"/>. If this is not a property, this returns null
        /// </summary>
        public PropertyInfo Property { get; }

        /// <summary>
        /// Convert this to a <see cref="FieldInfo"/>. If this is not a field, this returns null
        /// </summary>
        public FieldInfo Field { get; }

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
        /// Create a new <see cref="PropertyFieldInfo{T,U}"/> from a given function expression
        /// </summary>
        /// <param name="expression">The expression that points to the given property/ field</param>
        public PropertyFieldInfo(Expression<Func<T, U>> expression)
        {
            MemberInfo member;
            if (expression.Body.NodeType == ExpressionType.Convert)
            {
                //expression.Body = expression.Body as Convert;
                if (expression.Body is UnaryExpression unEx)
                {
                    if (unEx.Operand is MemberExpression memberExpression)
                    {
                        member = memberExpression.Member;
                    }
                    else
                    {
                        throw ErrorFactory.Default.InvalidExpression(nameof(expression));
                    }
                }
                else
                {
                    throw ErrorFactory.Default.InvalidExpression(nameof(expression));
                }
            }
            else
            {
                if (expression.Body is MemberExpression memberExpression)
                {
                    member = memberExpression.Member;
                }
                else
                {
                    throw ErrorFactory.Default.InvalidExpression(nameof(expression));
                }
            }

            var propField = LoadMember(member);
            Field = propField.Field;
            Property = propField.Property;
        }

        /// <summary>
        /// Create a new <see cref="PropertyFieldInfo{T,U}"/> from a given <see cref="MemberInfo"/>
        /// </summary>
        /// <param name="member">The meber (must be either a <see cref="PropertyInfo"/> or a <see cref="FieldInfo"/>)</param>
        public PropertyFieldInfo(MemberInfo member)
        {
            var propField = LoadMember(member);
            Field = propField.Field;
            Property = propField.Property;
        }

        /// <summary>
        /// Create a new <see cref="PropertyFieldInfo{T,U}"/> from a given <see cref="FieldInfo"/>
        /// </summary>
        /// <param name="field">The field</param>
        public PropertyFieldInfo(FieldInfo field)
        {
            Field = field;
        }

        /// <summary>
        /// Create a new <see cref="PropertyFieldInfo{T,U}"/> from a given <see cref="PropertyInfo"/>
        /// </summary>
        /// <param name="property">The property</param>
        public PropertyFieldInfo(PropertyInfo property)
        {
            Property = property;
        }

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

            throw ErrorFactory.Default.InvalidMember(nameof(member));
        }

        /// <summary>
        /// Set this member's value on a given object
        /// </summary>
        /// <param name="baseObject">The base object to set the member of</param>
        /// <param name="valueToAssign">The value to assign to the member</param>
        public virtual void SetValue(T baseObject, U valueToAssign)
        {
            Property?.SetValue(baseObject, valueToAssign);

            Field?.SetValue(baseObject, valueToAssign);
        }

        /// <summary>
        /// Get this member's value from a given object
        /// </summary>
        /// <param name="baseObject">The base object to get the member of</param>
        /// <returns>The value of the member</returns>
        public U GetValue(T baseObject)
        {
            return (U)(Property?.GetValue(baseObject) ?? Field?.GetValue(baseObject));
        }
    }

    /// <summary>
    /// Reflection options to group <see cref="PropertyInfo"/> and <see cref="FieldInfo"/> under a common wrapper when the type of the parent and child are not known at compile time
    /// </summary>
    public class PropertyFieldInfo : PropertyFieldInfo<object, object>
    {
        /// <summary>
        /// Create a new <see cref="PropertyFieldInfo"/> from a given function expression
        /// </summary>
        /// <param name="expression">The expression that points to the given property/ field</param>
        public PropertyFieldInfo(Expression<Func<object, object>> expression) : base(expression)
        {

        }

        /// <summary>
        /// Create a new <see cref="PropertyFieldInfo"/> from a given <see cref="MemberInfo"/>
        /// </summary>
        /// <param name="member">The meber (must be either a <see cref="PropertyInfo"/> or a <see cref="FieldInfo"/>)</param>
        public PropertyFieldInfo(MemberInfo member) : base(member)
        {

        }

        /// <summary>
        /// Create a new <see cref="PropertyFieldInfo"/> from a given <see cref="FieldInfo"/>
        /// </summary>
        /// <param name="field">The field</param>
        public PropertyFieldInfo(FieldInfo field) : base(field)
        {

        }
        /// <summary>
        /// Create a new <see cref="PropertyFieldInfo"/> from a given <see cref="PropertyInfo"/>
        /// </summary>
        /// <param name="property">The property</param>

        public PropertyFieldInfo(PropertyInfo property) : base(property)
        {

        }
    }
}