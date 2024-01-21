using FixtureGenerator;
using FixtureGeneratorTests.Framework;
using FluentAssertions;

namespace FixtureGeneratorTests
{
    public class ReverserTests
    {
        private List<Team> _teams;
        private List<List<Match>> _matches;
        private Reverser _reverser;

        [SetUp]
        public void Setup()
        {
            _teams = new List<Team>()
            {
                new Team("A"),
                new Team("B"),
                new Team("C"),
                new Team("D")
            };

            _matches = new List<List<Framework.Match>>()
            {
                new List<Framework.Match>()
                {
                    new Framework.Match()
                    {
                        HomeEntity = _teams[0],
                        AwayEntity = _teams[1]
                    },
                    new Framework.Match()
                    {
                        HomeEntity = _teams[2],
                        AwayEntity = _teams[3]
                    }
                }
            };

            _reverser = new Reverser();
        }

        [Test]
        public void Reverse_WhenCalled_ReversesDuplicates()
        {
            var secondHalf = new List<List<Framework.Match>>(_matches);

            var expected = new List<List<Framework.Match>>()
            {
                new List<Framework.Match>()
                {
                    new Framework.Match()
                    {
                        HomeEntity = _teams[1],
                        AwayEntity = _teams[0]
                    },
                    new Framework.Match()
                    {
                        HomeEntity = _teams[3],
                        AwayEntity = _teams[2]
                    }
                }
            };

            var result = _reverser.Reverse(_matches, secondHalf);

            result.Should().BeEquivalentTo(expected);
        }
    }
}
