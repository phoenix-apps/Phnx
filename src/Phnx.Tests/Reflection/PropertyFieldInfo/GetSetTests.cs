using NUnit.Framework;
using Phnx.Reflection.Tests.TestClasses;
using System;

namespace Phnx.Reflection.Tests.PropertyFieldInfo
{
    public class GetSetTests
    {
        [Test]
        public void CanGetSet_Prop_WhichIsReadAndWrite_CanBeReadAndWritten()
        {
            var prop = new PropertyFieldInfo<PropertyFieldClass, string>(p => p.PublicProperty);

            Assert.IsTrue(prop.CanGet);
            Assert.IsTrue(prop.CanSet);
        }

        [Test]
        public void CanGetSet_Prop_WhichIsReadOnly_CanBeReadAndNotWritten()
        {
            var prop = new PropertyFieldInfo<PropertyFieldClass, string>(p => p.PublicGetOnlyProperty);

            Assert.IsTrue(prop.CanGet);
            Assert.IsFalse(prop.CanSet);
        }
        [Test]
        public void CanGetSet_Field_WhichIsReadAndWrite_CanBeReadAndWritten()
        {
            var field = new PropertyFieldInfo<PropertyFieldClass, DateTime>(p => p.PublicField);

            Assert.IsTrue(field.CanGet);
            Assert.IsTrue(field.CanSet);
        }

        [Test]
        public void CanGetSet_Field_WhichIsReadOnly_CanBeReadAndNotWritten()
        {
            var field = new PropertyFieldInfo<PropertyFieldClass, DateTime>(p => p.PublicFieldReadOnly);

            Assert.IsTrue(field.CanGet);
            Assert.IsFalse(field.CanSet);
        }

        [Test]
        public void GetValue_Prop_WhichIsReadonly_Gets()
        {
            var expected = "test";
            var instance = new PropertyFieldClass(expected);

            var prop = new PropertyFieldInfo<PropertyFieldClass, string>(p => p.PublicGetOnlyProperty);

            var result = prop.GetValue(instance);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void SetValue_Prop_WhichIsWritable_Sets()
        {
            var expected = "test";
            var instance = new PropertyFieldClass();

            var prop = new PropertyFieldInfo<PropertyFieldClass, string>(p => p.PublicProperty);

            prop.SetValue(instance, expected);

            Assert.AreEqual(expected, instance.PublicProperty);
        }

        [Test]
        public void GetValue_Field_WhichIsReadonly_Gets()
        {
            var expected = new DateTime(2000, 1, 1);
            var instance = new PropertyFieldClass(expected);

            var prop = new PropertyFieldInfo<PropertyFieldClass, DateTime>(p => p.PublicFieldReadOnly);

            DateTime result = prop.GetValue(instance);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void SetValue_Field_WhichIsWritable_Sets()
        {
            var expected = new DateTime(2000, 1, 1);
            var instance = new PropertyFieldClass();

            var field = new PropertyFieldInfo<PropertyFieldClass, DateTime>(p => p.PublicField);

            field.SetValue(instance, expected);

            Assert.AreEqual(expected, instance.PublicField);
        }

        [Test]
        public void GetSetValue_ForValueWithImplicitCast_GetsAndSets()
        {
            object value = new DateTime(2000, 1, 1);
            var instance = new PropertyFieldClass();

            var field = new PropertyFieldInfo<PropertyFieldClass, object>(p => p.PublicField);

            field.SetValue(instance, value);

            Assert.AreEqual(value, instance.PublicField);
            Assert.AreEqual(value, field.GetValue(instance));
        }
    }
}
