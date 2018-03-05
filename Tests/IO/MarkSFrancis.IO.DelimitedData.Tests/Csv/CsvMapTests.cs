using MarkSFrancis.IO.DelimitedData.Maps.Read;
using NUnit.Framework;

namespace MarkSFrancis.IO.DelimitedData.Tests.Csv
{
    [TestFixture]
    public class CsvMapTests
    {
        // ReSharper disable InconsistentNaming
        private class MapTest
        {
#pragma warning disable 649 // Used in tests via reflection
            public string asdf;
#pragma warning restore 649

            // ReSharper disable once UnusedAutoPropertyAccessor.Local
            public string Asdf2 { get; set; }
        }
        // ReSharper restore InconsistentNaming

        [Test]
        public void AssigningField_WhenMapContainsField_SetsValue()
        {
            // Arrange
            var map = new ReadMapColumnName<MapTest>("asdf");
            map.TryAddMap(test => test.asdf, "asdf");

            // Act
            var mapTest = map.MapToObject(new[] { "exampleValue" });

            // Assert
            Assert.AreEqual("exampleValue", mapTest.asdf);
        }

        [Test]
        public void AssigningProperty_WhenMapContainsProperty_SetsValue()
        {
            // Arrange
            var map = new ReadMapColumnName<MapTest>("asdf");
            map.TryAddMap(test => test.Asdf2, "asdf");

            // Act
            var mapTest = map.MapToObject(new[] { "exampleValue" });

            // Assert
            Assert.AreEqual("exampleValue", mapTest.Asdf2);
        }

        [Test]
        public void AssigningProperty_WhenMapContainsMany_SetsValue()
        {
            // Arrange
            var map = new ReadMapColumnName<MapTest>("asdf");
            map.TryAddMap(test => test.Asdf2, "asdf");
            map.TryAddMap(test => test.asdf, "asdf2");

            // Act
            var mapTest = map.MapToObject(new[] { "exampleValue" });

            // Assert
            Assert.AreEqual("exampleValue", mapTest.Asdf2);
        }

        [Test]
        public void AssigningField_WhenMapContainsNumberedField_SetsValue()
        {
            // Arrange
            var map = new ReadMapColumnId<MapTest>();
            map.TryAddMap(test => test.asdf, 4);

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
            var map = new ReadMapColumnId<MapTest>();
            map.TryAddMap(test => test.Asdf2, 7);

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
            var map = ReadMapColumnName<MapTest>.AutoMap(columns: "asdf2");

            // Act
            var mapTest = map.MapToObject(new[] { "exampleValue" });

            // Assert
            Assert.AreEqual("exampleValue", mapTest.Asdf2);
        }

        [Test]
        public void AssigningField_WhenMapIsAuto_SetsValue()
        {
            // Arrange
            ReadMapColumnName<MapTest> map = ReadMapColumnName<MapTest>.AutoMap(false, true, "asdf");

            // Act
            var mapTest = map.MapToObject(new[] { "exampleValue" });

            // Assert
            Assert.AreEqual("exampleValue", mapTest.asdf);
        }
    }
}