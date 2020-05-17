using Phnx.Tests.Fakes.TypeFakes;
using NUnit.Framework;
using System;
using System.Reflection;

namespace Phnx.Tests.Extensions
{
    public class TypeExtensionsTests
    {
        [Test]
        public void CreatingInstance_WithNoArgs_CreatesType()
        {
            var instance = typeof(ParentClass).CreateInstance();

            Assert.IsInstanceOf<ParentClass>(instance);
        }

        [Test]
        public void CreatingInstance_WithArgsButNoOverload_ThrowsMissingMethodException()
        {
            Assert.Throws<MissingMethodException>(() => typeof(ParentClass).CreateInstance(1));
        }

        [Test]
        public void CreatingInstance_OfTypeWithBrokenConstructor_ThrowsTargetInvocationException()
        {
            Assert.Throws<TargetInvocationException>(() => typeof(TypeWithBrokenConstructor).CreateInstance());
        }

        [Test]
        public void CreatingInstance_OfNullType_ThrowsArgumentNullException()
        {
            Type _null = null;

            Assert.Throws<ArgumentNullException>(() => _null.CreateInstance());
        }
    }
}
