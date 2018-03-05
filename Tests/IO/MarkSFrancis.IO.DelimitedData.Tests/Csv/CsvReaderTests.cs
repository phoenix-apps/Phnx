using System;
using System.IO;
using MarkSFrancis.IO.DelimitedData.Csv.Mapped;
using NUnit.Framework;

namespace MarkSFrancis.IO.DelimitedData.Tests.Csv
{
    [TestFixture]
    class CsvReaderTests
    {
        private class StringClass
        {
            public string NameProperty1 { get; set; }

            public string NameProperty2 { get; set; }
        }

        private class DateClass
        {
            public string Name { get; set; }

            public DateTime DateTime { get; set; }
        }

        private class CsvBuilder<T> where T : new()
        {
            private MemoryStream Stream { get; }
            public CsvReaderMapped<T> Reader { get; }

            public CsvBuilder(string testText)
            {
                Stream = new MemoryStream();
                StreamWriter writer = new StreamWriter(Stream) { AutoFlush = true };
                writer.WriteLine(testText);

                Stream.Position = 0;
                Reader = CsvReaderMapped<T>.AutoMapped(Stream);
            }
        }

        [Test]
        public void ReadCsv_WhenCsvIsStrings_ReturnsValIdCsvData()
        {
            // Arrange
            string csvData = 
                "Name Property 1,Name Property 2" + Environment.NewLine + 
                "value,value2";

            StringClass ExpectedResult = new StringClass { NameProperty1 = "value", NameProperty2 = "value2" };
            CsvReaderMapped<StringClass> reader = new CsvBuilder<StringClass>(csvData).Reader;

            // Act
            StringClass result = reader.ReadRecord();

            // Assert
            Assert.AreEqual(ExpectedResult.NameProperty1, result.NameProperty1);
            Assert.AreEqual(ExpectedResult.NameProperty2, result.NameProperty2);
        }

        [Test]
        public void ReadCsv_WhenCsvIsDatesAndStrings_ReturnsValIdCsvData()
        {
            // Arrange
            string csvData = 
                "Name,Date Time" + Environment.NewLine + 
                "value,2001/01/01";

            DateClass ExpectedResult = new DateClass { Name = "value", DateTime = new DateTime(2001, 1, 1) };

            CsvReaderMapped<DateClass> reader = new CsvBuilder<DateClass>(csvData).Reader;

            // Act
            DateClass result = reader.ReadRecord();

            // Assert
            Assert.AreEqual(ExpectedResult.Name, result.Name);
            Assert.AreEqual(ExpectedResult.DateTime, result.DateTime);
        }
    }
}