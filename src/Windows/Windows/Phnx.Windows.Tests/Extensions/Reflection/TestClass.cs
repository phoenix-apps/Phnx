namespace Phnx.Windows.Tests.Extensions.Reflection
{
    public class TestClass
    {
        public string AutoProperty { get; set; }

        public string NotAutoProperty
        {
            get => Field;
            set => Field = value;
        }

        public string Field = "asdf";
    }
}
