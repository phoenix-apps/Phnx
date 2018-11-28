namespace Phnx.Windows.Tests.Fakes
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
