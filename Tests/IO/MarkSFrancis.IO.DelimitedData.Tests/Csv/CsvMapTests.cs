using MarkSFrancis.IO.DelimitedData.Maps;
using MarkSFrancis.IO.DelimitedData.Maps.Extensions;
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
            var map = new MapColumnName<MapTest>();
            map.Map(test => test.asdf, "asdf");

            // Act
            var mapTest = map.MapToObject(new[] { "exampleValue" }, new[] { "asdf" });

            // Assert
            Assert.AreEqual("exampleValue", mapTest.asdf);
        }

        [Test]
        public void AssigningProperty_WhenMapContainsProperty_SetsValue()
        {
            // Arrange
            var map = new MapColumnName<MapTest>();
            map.Map(test => test.Asdf2, "asdf");

            // Act
            var mapTest = map.MapToObject(new[] { "exampleValue" }, new[] { "asdf" });

            // Assert
            Assert.AreEqual("exampleValue", mapTest.Asdf2);
        }

        [Test]
        public void AssigningProperty_WhenMapContainsMany_SetsValue()
        {
            // Arrange
            var map = new MapColumnName<MapTest>();
            map.Map(test => test.Asdf2, "asdf");
            map.Map(test => test.asdf, "asdf2");

            // Act
            var mapTest = map.MapToObject(new[] { "exampleValue" }, new[] { "asdf" });

            // Assert
            Assert.AreEqual("exampleValue", mapTest.Asdf2);
        }

        [Test]
        public void AssigningField_WhenMapContainsNumberedField_SetsValue()
        {
            // Arrange
            var map = new MapColumnId<MapTest>();
            map.Map(test => test.asdf, 4);

            // Act
            var mapTest = map.MapToObject(
                new[] { null, null, null, null, "exampleValue" }, null);

            // Assert
            Assert.AreEqual("exampleValue", mapTest.asdf);
        }

        [Test]
        public void AssigningProperty_WhenMapContainsNumberedProperty_SetsValue()
        {
            // Arrange
            var map = new MapColumnId<MapTest>();
            map.Map(test => test.Asdf2, 7);

            // Act
            var mapTest = map.MapToObject(
                new[] { null, null, null, null, null, null, null, "exampleValue" }, null);

            // Assert
            Assert.AreEqual("exampleValue", mapTest.Asdf2);
        }

        [Test]
        public void AssigningProperty_WhenMapIsAuto_SetsValue()
        {
            // Arrange
            var map = MapColumnName<MapTest>.AutoMap(false);

            // Act
            var mapTest = map.MapToObject(new[] { "exampleValue" }, new[] { "Asdf2" });

            // Assert
            Assert.AreEqual("exampleValue", mapTest.Asdf2);
        }

        [Test]
        public void AssigningField_WhenMapIsAuto_SetsValue()
        {
            // Arrange
            MapColumnName<MapTest> map = MapColumnName<MapTest>.AutoMap(false, false, true);

            // Act
            var mapTest = map.MapToObject(new[] { "exampleValue" }, new[] { "asdf" });

            // Assert
            Assert.AreEqual("exampleValue", mapTest.asdf);
        }
    }
}