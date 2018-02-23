using MarkSFrancis.IO.DelimitedData.Maps;
using NUnit.Framework;

namespace MarkSFrancis.IO.DelimitedData.Tests.Csv
{
    [TestFixture]
    public class CsvMapTests
    {
        private class MapTest
        {
#pragma warning disable 649 // Used in tests via reflection
            public string asdf;
#pragma warning restore 649

            // ReSharper disable once UnusedAutoPropertyAccessor.Local
            public string Asdf2 { get; set; }
        }

        [Test]
        public void AssigningField_WhenMapContainsField_SetsValue()
        {
            // Arrange
            NameMap<MapTest> map = new NameMap<MapTest>()
                .Map(test => test.asdf, "asdf");

            // Act
            var mapTest = map.MapToObject(new[] { "exampleValue" }, new[] { "asdf" });

            // Assert
            Assert.AreEqual("exampleValue", mapTest.asdf);
        }

        [Test]
        public void AssigningProperty_WhenMapContainsProperty_SetsValue()
        {
            // Arrange
            NameMap<MapTest> map = new NameMap<MapTest>()
                .Map(test => test.Asdf2, "asdf");

            // Act
            var mapTest = map.MapToObject(new[] { "exampleValue" }, new[] { "asdf" });

            // Assert
            Assert.AreEqual("exampleValue", mapTest.Asdf2);
        }

        [Test]
        public void AssigningProperty_WhenMapContainsMany_SetsValue()
        {
            // Arrange
            NameMap<MapTest> map = new NameMap<MapTest>()
                .Map(test => test.Asdf2, "asdf")
                .Map(test => test.asdf, "asdf2");

            // Act
            var mapTest = map.MapToObject(new[] { "exampleValue" }, new[] { "asdf" });

            // Assert
            Assert.AreEqual("exampleValue", mapTest.Asdf2);
        }

        [Test]
        public void AssigningField_WhenMapContainsNumberedField_SetsValue()
        {
            // Arrange
            NumberMap<MapTest> map = new NumberMap<MapTest>()
                .Map(test => test.asdf, 4);

            // Act
            var mapTest = map.MapToObject(
                new[] { null, null, null, null, "exampleValue" });

            // Assert
            Assert.AreEqual("exampleValue", mapTest.asdf);
        }

        [Test]
        public void AssigningProperty_WhenMapContainsNumberedProperty_SetsValue()
        {
            // Arrange
            NumberMap<MapTest> map = new NumberMap<MapTest>()
                .Map(test => test.Asdf2, 7);

            // Act
            var mapTest = map.MapToObject(
                new[] { null, null, null, null, null, null, null, "exampleValue" });

            // Assert
            Assert.AreEqual("exampleValue", mapTest.Asdf2);
        }

        [Test]
        public void AssigningProperty_WhenMapIsAuto_SetsValue()
        {
            // Arrange
            NameMap<MapTest> map = NameMap<MapTest>.FromProperties();

            // Act
            var mapTest = map.MapToObject(new[] { "exampleValue" }, new[] { "asdf2" });

            // Assert
            Assert.AreEqual("exampleValue", mapTest.Asdf2);
        }

        [Test]
        public void AssigningField_WhenMapIsAuto_SetsValue()
        {
            // Arrange
            NameMap<MapTest> map = NameMap<MapTest>.FromFields();

            // Act
            var mapTest = map.MapToObject(new[] { "exampleValue" }, new[] { "asdf" });

            // Assert
            Assert.AreEqual("exampleValue", mapTest.asdf);
        }
    }
}