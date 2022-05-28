using FixtureGenerator;
using FixtureGeneratorTests.Framework;

namespace FixtureGeneratorTests
{
    public class FxitureGeneratorTests
    {
        private List<Team> _teams;

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
        }

        [Test]
        public void TestRoundRobin()
        {
            List<List<Match>> result = FixtureAlgorithm.GenerateFixtures<Match, Team>(_teams);

            // Three rounds of fixtures
            Assert.IsTrue(result.Count == 3);

            // Two rounds per match.
            Assert.IsTrue(result.All(r => r.Count == 2));
        }

        [Test]
        public void TestHomeAndAway()
        {
            List<List<Match>> result = FixtureAlgorithm.GenerateFixtures<Match, Team>(_teams, FixtureAlgorithm.Options.EHomeAway);

            Assert.IsTrue(result.Count == 6);

            Assert.IsTrue(result.All(r => r.Count == 2));
        }
    }
}