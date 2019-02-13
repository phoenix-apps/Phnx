using NUnit.Framework;
using Phnx.Reflection.Tests.TestClasses;
using System.Diagnostics;
using System.Reflection;

namespace Phnx.Reflection.Tests.Extensions
{
    public class GetAttributeTests
    {
        [Test]
        public void GettingADisplayName_WithDisplayNameAttribute_GetsDisplayName()
        {
            MemberInfo prop = new PropertyFieldInfo<AttributesClass, string>(a => a.DisplayNameTest).Member;

            Assert.AreEqual(AttributesClass.DisplayNameTestName, prop.GetDisplayName());
        }

        [Test]
        public void GettingADisplayName_WithDisplayAttribute_GetsDisplayName()
        {
            MemberInfo prop = new PropertyFieldInfo<AttributesClass, string>(a => a.DisplayNameTest2).Member;

            Assert.AreEqual(AttributesClass.DisplayNameTest2Name, prop.GetDisplayName());
        }

        [Test]
        public void GettingADisplayName_WithoutDisplayAttribute_GetsMemberName()
        {
            MemberInfo prop = new PropertyFieldInfo<AttributesClass, string>(a => a.DisplayNameTest3).Member;

            Assert.AreEqual(AttributesClass.DisplayNameTest3Name, prop.GetDisplayName());
        }

        [Test]
        public void GettingAnAttribute_FromAMember_GetsTheAttribute()
        {
            MemberInfo prop = new PropertyFieldInfo<AttributesClass, string>(a => a.RandomAttributeTest).Member;

            DebuggerBrowsableAttribute attribute = prop.GetAttribute<DebuggerBrowsableAttribute>();

            Assert.AreEqual(AttributesClass.RandomAttributeTestBrowsableState, attribute.State);
        }
    }
}
