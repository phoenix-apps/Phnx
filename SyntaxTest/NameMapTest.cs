using System.Collections.Generic;
using System.Linq;
using MarkSFrancis.Console;
using MarkSFrancis.IO.DelimitedData.Maps;

namespace SyntaxTest
{
    class NameMapTest
    {
        private readonly ConsoleIo _console;

        public NameMapTest(ConsoleIo console)
        {
            _console = console;
        }

        public void Run()
        {
            var writeMap = MapColumnName<ClassToMap>.AutoMap();

            var testClass = new ClassToMap
            {
                asdf12bsdf = "asdfVal",
                thisIsAnIDCamelCaseTest = "Camel Test val",
                ThisIsAPascalCaseTest = "Pascal test val"
            };

            var columns = writeMap.MappedColumnNames.ToList();
            var mappedResult = writeMap.MapFromObject(testClass, columns);
            _console.WriteCollection(mappedResult);

            MapColumnName<ClassToMap> readMap = MapColumnName<ClassToMap>.AutoMap();
            var testValues = new List<string>
            {
                "asdfVal2",
                "Camel Test val 2",
                "Pascal test val 2"
            };

            var mappedObject = readMap.MapToObject(testValues, columns);
            _console.WriteLine(mappedObject);
        }

#pragma warning disable IDE1006 // Naming Styles
        // ReSharper disable MemberCanBePrivate.Local

        private class ClassToMap
        {
            public string asdf12bsdf { get; set; }

            public string ThisIsAPascalCaseTest { get; set; }

            public string thisIsAnIDCamelCaseTest { get; set; }

            public override string ToString()
            {
                return
                    $"{nameof(asdf12bsdf)}: {asdf12bsdf}. {nameof(ThisIsAPascalCaseTest)}: {ThisIsAPascalCaseTest}. {nameof(thisIsAnIDCamelCaseTest)}: {thisIsAnIDCamelCaseTest}";
            }
        }

        // ReSharper restore MemberCanBePrivate.Local
#pragma warning restore IDE1006 // Naming Styles
    }
}
