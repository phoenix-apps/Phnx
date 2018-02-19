using System;
using System.Linq.Expressions;
using System.Reflection;

namespace MarkSFrancis.Reflection
{
    public class PropertyFieldInfo<T, U>
    {
        public bool IsProperty => Property == null;
        public PropertyInfo Property { get; }
        public FieldInfo Field { get; }
        public MemberInfo Member => (MemberInfo)Property ?? Field;

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
                        throw ErrorFactory.InvalidExpression(nameof(expression));
                    }
                }
                else
                {
                    throw ErrorFactory.InvalidExpression(nameof(expression));
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
                    throw ErrorFactory.InvalidExpression(nameof(expression));
                }
            }

            var propField = LoadMember(member);
            Field = propField.Field;
            Property = propField.Property;
        }

        public PropertyFieldInfo(MemberInfo member)
        {
            var propField = LoadMember(member);
            Field = propField.Field;
            Property = propField.Property;
        }

        public PropertyFieldInfo(FieldInfo field)
        {
            Field = field;
        }

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

            throw ErrorFactory.InvalidMember(nameof(member));
        }

        public string Name => Property?.Name ?? Field?.Name;

        public Type MyType => Property?.PropertyType ?? Field.FieldType;

        public virtual void SetValue(T baseObject, U valueToAssign)
        {
            Property?.SetValue(baseObject, valueToAssign);

            Field?.SetValue(baseObject, valueToAssign);
        }

        public U GetValue(T baseObject)
        {
            return (U)(Property?.GetValue(baseObject) ?? Field?.GetValue(baseObject));
        }
    }

    public class PropertyFieldInfo : PropertyFieldInfo<object, object>
    {
        public PropertyFieldInfo(Expression<Func<object, object>> expression) : base(expression)
        {

        }

        public PropertyFieldInfo(MemberInfo member) : base(member)
        {

        }

        public PropertyFieldInfo(FieldInfo field) : base(field)
        {

        }

        public PropertyFieldInfo(PropertyInfo property) : base(property)
        {

        }
    }
}