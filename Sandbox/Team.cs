using FixtureGenerator;

namespace Sandbox
{
    public class Team : IFixtureEntity
    {
        private readonly string _name;
        public Team(string name)
        {
            _name = name;
        }

        public string Code => _name;

        public override string ToString()
        {
            return _name;
        }
    }
}
