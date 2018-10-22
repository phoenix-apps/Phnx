using NUnit.Framework;
using Phnx.Reflection.Tests.TestClasses;
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Phnx.Reflection.Tests.PropertyFieldInfo
{
    public class NewTests
    {
        [Test]
        public void New_WithNullExpression_ThrowsArgumentNullException()
        {
            Expression<Func<PropertyFieldClass, string>> expression = null;

            Assert.Throws<ArgumentNullException>(() => _ = new PropertyFieldInfo<PropertyFieldClass, string>(expression));
        }

        [Test]
        public void New_WithNonMemberExpression_ThrowsArgumentException()
        {
            Expression<Func<PropertyFieldClass, string>> expression = item => null;

            Assert.Throws<ArgumentException>(() => _ = new PropertyFieldInfo<PropertyFieldClass, string>(expression));
        }

        [Test]
        public void New_WithMethodAccessExpression_ThrowsArgumentException()
        {
            Expression<Func<PropertyFieldClass, string>> expression = item => item.ToString();

            Assert.Throws<ArgumentException>(() =>
            _ = new PropertyFieldInfo<PropertyFieldClass, string>(expression));
        }

        [Test]
        public void New_WithMethodMemberInfo_ThrowsArgumentException()
        {
            MemberInfo member = typeof(PropertyFieldClass).GetMember(nameof(PropertyFieldClass.ToString))[0];

            Assert.Throws<ArgumentException>(() =>
            _ = new PropertyFieldInfo<PropertyFieldClass, string>(member));
        }

        [Test]
        public void New_WithPropertyMemberInfo_ThrowsArgumentException()
        {
            MemberInfo member = typeof(PropertyFieldClass).GetProperty(nameof(PropertyFieldClass.PublicProperty));

            Assert.DoesNotThrow(() =>
            _ = new PropertyFieldInfo<PropertyFieldClass, string>(member));
        }
    }
}
