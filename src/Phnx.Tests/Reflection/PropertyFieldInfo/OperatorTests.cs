using NUnit.Framework;
using Phnx.Reflection.Tests.TestClasses;
using System;
using System.Reflection;

namespace Phnx.Reflection.Tests.PropertyFieldInfo
{
    public class OperatorTests
    {
        [Test]
        public void ToPropertyInfo_WhenField_ThrowsInvalidCastException()
        {
            var propFieldInfo = new PropertyFieldInfo<PropertyFieldClass, DateTime>(pf => pf.PublicField);

            Assert.Throws<InvalidCastException>(() => _ = (PropertyInfo)propFieldInfo);
        }

        [Test]
        public void ToPropertyInfo_WhenProperty_Converts()
        {
            var propFieldInfo = new PropertyFieldInfo<PropertyFieldClass, string>(pf => pf.PublicProperty);

            var prop = (PropertyInfo)propFieldInfo;

            Assert.AreEqual(propFieldInfo.Member, prop);
        }

        [Test]
        public void ToFieldInfo_WhenField_Converts()
        {
            var propFieldInfo = new PropertyFieldInfo<PropertyFieldClass, DateTime>(pf => pf.PublicField);

            var prop = (FieldInfo)propFieldInfo;

            Assert.AreEqual(propFieldInfo.Member, prop);
        }

        [Test]
        public void ToFieldInfo_WhenProperty_ThrowsInvalidCastException()
        {
            var propFieldInfo = new PropertyFieldInfo<PropertyFieldClass, string>(pf => pf.PublicProperty);

            Assert.Throws<InvalidCastException>(() => _ = (FieldInfo)propFieldInfo);
        }

        [Test]
        public void ToMember_WhenProperty_Converts()
        {
            var propFieldInfo = new PropertyFieldInfo<PropertyFieldClass, string>(pf => pf.PublicProperty);

            var member = (MemberInfo)propFieldInfo;

            Assert.AreEqual(propFieldInfo.Member, member);
        }

        [Test]
        public void ToMember_WhenField_Converts()
        {
            var propFieldInfo = new PropertyFieldInfo<PropertyFieldClass, DateTime>(pf => pf.PublicField);

            var member = (MemberInfo)propFieldInfo;

            Assert.AreEqual(propFieldInfo.Member, member);
        }
    }
}
