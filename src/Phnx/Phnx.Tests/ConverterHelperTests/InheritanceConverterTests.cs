using NUnit.Framework;
using System;

namespace Phnx.Tests.ConverterHelperTests
{
    using ChildType = Fakes.TypeFakes.ChildClass;
    using ParentType = Fakes.TypeFakes.ParentClass;

    public class InheritanceConverterTests
    {
        [Test]
        public void ConvertingFromChildTypeToParent_UsingGenerics_Converts()
        {
            var converter = ConverterHelpers.GetDefaultConverter<ChildType, ParentType>();

            var resultShouldBe = new ChildType();
            var result = converter(resultShouldBe);

            Assert.IsInstanceOf<ParentType>(result);
        }

        [Test]
        public void ConvertingFromParentTypeToChild_UsingGenerics_Converts()
        {
            var converter = ConverterHelpers.GetDefaultConverter<ParentType, ChildType>();

            ParentType resultShouldBe = new ChildType();
            var result = converter(resultShouldBe);

            Assert.IsInstanceOf<ChildType>(result);
        }

        [Test]
        public void MakingInvalidCast_UsingGenerics_ThrowsInvalidCastException()
        {
            var converter = ConverterHelpers.GetDefaultConverter<ParentType, int>();

            var resultShouldBe = new ChildType();
            Assert.Throws<InvalidCastException>(() => converter(resultShouldBe));
        }

        [Test]
        public void ConvertingFromChildTypeToParent_UsingTypes_Converts()
        {
            var converter = ConverterHelpers.GetDefaultConverter(typeof(ChildType), typeof(ParentType));

            ParentType resultShouldBe = new ChildType();
            var result = converter(resultShouldBe);

            Assert.IsInstanceOf<ParentType>(result);
        }

        [Test]
        public void ConvertingFromParentTypeToChild_UsingTypes_Converts()
        {
            var converter = ConverterHelpers.GetDefaultConverter(typeof(ParentType), typeof(ChildType));

            ParentType resultShouldBe = new ChildType();
            var result = converter(resultShouldBe);

            Assert.IsInstanceOf<ChildType>(result);
        }

        [Test]
        public void MakingInvalidCast_UsingTypes_ThrowsInvalidCastException()
        {
            var converter = ConverterHelpers.GetDefaultConverter(typeof(ParentType), typeof(int));

            var resultShouldBe = new ChildType();
            Assert.Throws<InvalidCastException>(() => converter(resultShouldBe));
        }
    }
}
