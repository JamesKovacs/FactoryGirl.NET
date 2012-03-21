namespace FactoryGirl.NET.Specs
{
    class Dummy
    {
        public const int DefaultValue = 1;

        public Dummy() {
            Value = DefaultValue;
        }

        public int Value { get; set; }
    }
}