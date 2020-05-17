using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Phnx.Reflection.Tests.TestClasses
{
    class AttributesClass
    {
        public const string DisplayNameTestName = "This is a test";

        [DisplayName(DisplayNameTestName)]
        public string DisplayNameTest { get; set; }

        public const string DisplayNameTest2Name = "This is a second test";

        [Display(Name = DisplayNameTest2Name)]
        public string DisplayNameTest2 { get; set; }

        public const string DisplayNameTest3Name = "DisplayNameTest3";

        public string DisplayNameTest3 { get; set; }

        public const DebuggerBrowsableState RandomAttributeTestBrowsableState = DebuggerBrowsableState.Collapsed;

        [DebuggerBrowsable(RandomAttributeTestBrowsableState)]
        public string RandomAttributeTest { get; set; }
    }
}
