using System.Linq;
using System.Reflection;
using MarkSFrancis.Collections.Extensions;
using MarkSFrancis.Reflection.Extensions;
using MarkSFrancis.Reflection.Tests.TestClasses;
using NUnit.Framework;

namespace MarkSFrancis.Reflection.Tests
{
    public class TypeExtensionTests
    {
        [Test]
        public void GettingAllPublicPropertyFields_WithoutGenerics_GetsAll()
        {
            var propFields = typeof(PropertyFieldClass).GetPropertyFieldInfos()
                .Select(m => m.Name)
                .OrderBy(m => m);

            var publicFields = typeof(PropertyFieldClass).GetFields()
                    .Select(f => (MemberInfo)f);

            var publicProperties = typeof(PropertyFieldClass).GetProperties()
                    .Select(p => (MemberInfo)p);

            var publicPropertiesAndFields = 
                publicFields.Append(publicProperties)
                    .Select(m => m.Name)
                    .OrderBy(m => m);

            Assert.AreEqual(publicPropertiesAndFields, propFields);
        }

        [Test]
        public void GettingAllPublicPropertyFields_WithGenerics_GetsAll()
        {
            var propFields = typeof(PropertyFieldClass).GetPropertyFieldInfos<PropertyFieldClass>()
                .Select(m => m.Name)
                .OrderBy(m => m);

            var publicFields = typeof(PropertyFieldClass).GetFields()
                    .Select(f => (MemberInfo)f);

            var publicProperties = typeof(PropertyFieldClass).GetProperties()
                    .Select(p => (MemberInfo)p);

            var publicPropertiesAndFields =
                publicFields.Append(publicProperties)
                    .Select(m => m.Name)
                    .OrderBy(m => m);

            Assert.AreEqual(publicPropertiesAndFields, propFields);
        }
    }
}
