using FixtureGenerator.Strategies;
using FixtureGeneratorTests.Framework;
using FluentAssertions;
using FluentAssertions.Execution;

namespace FixtureGeneratorTests
{
    public class RoundRobinStrategyTests
    {
        private List<Team> _teams;
        private RoundRobinStrategy _roundRobinStrategy;

        [SetUp]
        public void Setup()
        {
            _teams = new List<Team>()
            {
                new Team("A"),
                new Team("B"),
                new Team("C"),
                new Team("D"),
                new Team("E"),
                new Team("F")
            };

            _roundRobinStrategy = new RoundRobinStrategy();
        }

        [Test]
        public void RoundRobin_WhenCalled_GeneratesCorrectNumberOfRounds()
        {
            var matches = _roundRobinStrategy.GenerateFixtures<Match, Team>(_teams);
            var expectedNumberOfROunds = _teams.Count - 1;

            matches.Count.Should().Be(expectedNumberOfROunds);
        }

        [Test]
        public void RoundRobin_WhenCalled_GeneratesCorrectNumberOfGamesPerRound()
        {
            var matches = _roundRobinStrategy.GenerateFixtures<Match, Team>(_teams);
            matches.TrueForAll(round => round.Count == _teams.Count / 2);
        }

        [Test]
        public void RoundRobin_WhenCalled_OnlyContainsUniqueMatches()
        {
            var matches = _roundRobinStrategy.GenerateFixtures<Match, Team>(_teams);
            var allMatches = matches.SelectMany(round => round);

            allMatches.Should().OnlyHaveUniqueItems(x => x.Code).And
                 .NotContain(match => allMatches.Any(c => c.Code == new string(match.Code.Reverse().ToArray())),
                    "Each match should not have a reversed version.");
        }
    }
}
