using FixtureGenerator;

namespace FixtureGeneratorTests.Framework
{
    public class Team : IFixtureEntity
    {
        public string Code { get; }

        public Team(string code)
        {
            Code = code;
        }

        public override string ToString()
        {
            return Code;
        }
    }
}
