namespace FactoryGirl.NET.Specs
{
    class Dummy
    {
        public const int DefaultValue = 1;

        public Dummy() {
            Value = DefaultValue;
            String = FactoryGirl.Sequence("Test: ");
        }

        public int Value { get; set; }

        public string String { get; set; }
    }
}