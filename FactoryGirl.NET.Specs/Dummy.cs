namespace FactoryGirl.NET.Specs
{
    class Dummy
    {
        public const int DefaultValue = 1;
        public const string DefaultString = "test";

        public Dummy() {
            Id = DefaultValue;
            String = DefaultString;
        }

        public int Id { get; set; }

        public string String { get; set; }
    }
}