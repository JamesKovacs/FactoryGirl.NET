namespace FactoryGirl.NET.Specs
{
    class Dummy
    {
        public const int DefaultValue = 1;
        public const string DefaultString = "test";

        public Dummy() {
            Value = DefaultValue;
            String = DefaultString;
        }

        public int Value { get; set; }

        public string String { get; set; }
    }
}