using NUnit.Framework;
using Phnx.Reflection.Tests.TestClasses;
using System.Diagnostics;

namespace Phnx.Reflection.Tests.Extensions
{
    public class GetAttributeTests
    {
        [Test]
        public void GettingADisplayName_WithDisplayNameAttribute_GetsDisplayName()
        {
            var prop = new PropertyFieldInfo<AttributesClass, string>(a => a.DisplayNameTest).Member;

            Assert.AreEqual(AttributesClass.DisplayNameTestName, prop.GetDisplayName());
        }

        [Test]
        public void GettingADisplayName_WithDisplayAttribute_GetsDisplayName()
        {
            var prop = new PropertyFieldInfo<AttributesClass, string>(a => a.DisplayNameTest2).Member;

            Assert.AreEqual(AttributesClass.DisplayNameTest2Name, prop.GetDisplayName());
        }

        [Test]
        public void GettingADisplayName_WithoutDisplayAttribute_GetsMemberName()
        {
            var prop = new PropertyFieldInfo<AttributesClass, string>(a => a.DisplayNameTest3).Member;

            Assert.AreEqual(AttributesClass.DisplayNameTest3Name, prop.GetDisplayName());
        }

        [Test]
        public void GettingAnAttribute_FromAMember_GetsTheAttribute()
        {
            var prop = new PropertyFieldInfo<AttributesClass, string>(a => a.RandomAttributeTest).Member;

            var attribute = prop.GetAttribute<DebuggerBrowsableAttribute>();

            Assert.AreEqual(AttributesClass.RandomAttributeTestBrowsableState, attribute.State);
        }
    }
}
