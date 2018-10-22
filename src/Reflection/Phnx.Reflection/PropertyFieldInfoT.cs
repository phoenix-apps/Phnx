using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Phnx.Reflection
{
    /// <summary>
    /// Reflection options to group <see cref="PropertyInfo"/> and <see cref="FieldInfo"/> under a common wrapper, as <see cref="GetValue"/>, <see cref="SetValue"/> and other options are shared
    /// </summary>
    /// <typeparam name="T">The type that this <see cref="PropertyInfo"/> or <see cref="FieldInfo"/> belongs to</typeparam>
    /// <typeparam name="U">The type of this <see cref="PropertyInfo"/> or <see cref="FieldInfo"/></typeparam>
    public class PropertyFieldInfo<T, U> : PropertyFieldInfo
    {
        /// <summary>
        /// Create a new <see cref="PropertyFieldInfo"/> from a function expression
        /// </summary>
        /// <param name="expression">The expression that points to a property/ field</param>
        /// <exception cref="ArgumentException">The expression is not a property or field access</exception>
        public PropertyFieldInfo(Expression<Func<T, U>> expression)
        {
            if (expression is null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            LoadExpression(expression);
        }

        /// <summary>
        /// Create a new <see cref="PropertyFieldInfo"/> from a <see cref="MemberInfo"/>
        /// </summary>
        /// <param name="member">The meber (must be either a <see cref="PropertyInfo"/> or a <see cref="FieldInfo"/>)</param>
        /// <exception cref="ArgumentException">The member is not a property or field</exception>
        public PropertyFieldInfo(MemberInfo member) : base(member)
        {
        }

        /// <summary>
        /// Create a new <see cref="PropertyFieldInfo"/> from a <see cref="PropertyInfo"/>
        /// </summary>
        /// <param name="property">The property</param>
        public PropertyFieldInfo(PropertyInfo property) : base(property)
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
        /// Loads an <see cref="Expression{T}"/> as a member access onto this
        /// </summary>
        /// <param name="expression">The expression that points to a property/ field</param>
        /// <typeparam name="TObject">The object which member belongs to</typeparam>
        /// <typeparam name="TMember">The type of the member to load onto this</typeparam>
        /// <exception cref="ArgumentException">The expression is not a property or field access</exception>
        protected void LoadExpression<TObject, TMember>(Expression<Func<TObject, TMember>> expression)
        {
            MemberInfo member;
            if (expression.Body is MemberExpression memberExpression)
            {
                member = memberExpression.Member;
            }
            else
            {
                string msg = ErrorMessage.Factory.ExpressionIsNotPropertyOrFieldAccess();
                throw new ArgumentException(msg, nameof(expression));
            }

            LoadMember(member);
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
        public void SetValue(T baseObject, U valueToAssign)
        {
            base.SetValue(baseObject, valueToAssign);
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
        public U GetValue(T baseObject)
        {
            return (U)base.GetValue(baseObject);
        }
    }
}
