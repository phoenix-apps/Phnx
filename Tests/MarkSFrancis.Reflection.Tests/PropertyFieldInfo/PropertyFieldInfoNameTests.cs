using System;
using System.Reflection;
using MarkSFrancis.Reflection.Tests.TestClasses;
using NUnit.Framework;

namespace MarkSFrancis.Reflection.Tests.PropertyFieldInfo
{
    public class PropertyFieldInfoNameTests 
    {
        [Test]
        public void GettingAProperty_WhichIsPublic_GetsTheCorrectName()
        {
            var prop = new PropertyFieldInfo<PropertyFieldClass, string>(p => p.PublicProperty);

            Assert.AreEqual(nameof(PropertyFieldClass.PublicProperty), prop.Name);
        }

        [Test]
        public void GettingAProperty_WhichIsPrivate_GetsTheCorrectName()
        {
            string propName = "PrivateProperty";

            var propInfo =
                typeof(PropertyFieldClass).GetProperty(propName,
                    BindingFlags.Instance | BindingFlags.NonPublic);

            var prop = new PropertyFieldInfo<PropertyFieldClass, int>(propInfo);

            Assert.AreEqual(propName, prop.Name);
        }

        [Test]
        public void GettingAField_WhichIsPublic_GetsTheCorrectName()
        {
            var field = new PropertyFieldInfo<PropertyFieldClass, DateTime>(p => p.PublicField);

            Assert.AreEqual(nameof(PropertyFieldClass.PublicField), field.Name);
        }

        [Test]
        public void GettingAField_WhichIsPrivate_GetsTheCorrectName()
        {
            string fieldName = "_privateField";

            var fieldInfo =
                typeof(PropertyFieldClass).GetField(fieldName,
                    BindingFlags.Instance | BindingFlags.NonPublic);

            var field = new PropertyFieldInfo<PropertyFieldClass, int>(fieldInfo);

            Assert.AreEqual(fieldName, field.Name);
        }
    }
}
