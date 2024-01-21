using FixtureGenerator.Strategies;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;

namespace FixtureGenerator
{
    public class Generator : IGenerator
    {
        private readonly IFixtureStrategy _strategy;
        private readonly IReverser _reverser;

        private Dictionary<string, IFixture> _scheduledMatches = new Dictionary<string, IFixture>();

        internal Generator(IFixtureStrategy strategy, IReverser reverser)
        {
            _strategy = strategy ?? throw new ArgumentNullException(nameof(strategy));
            _reverser = reverser ?? throw new ArgumentNullException(nameof(reverser));
        }

        public Generator() : this(new RoundRobinStrategy(), new Reverser()) { }

        public List<List<M>> GenerateFixtures<M, T>(IEnumerable<T> fixtureEntities, Fixture.Options option = Fixture.Options.EHome)
            where M : IFixture, new()
            where T : IFixtureEntity
        {
            _scheduledMatches = new Dictionary<string, IFixture>();

            // Generate first half of the season.
            var firstHalf = _strategy.GenerateFixtures<M, T>(fixtureEntities);

            // randomise the team order.
            fixtureEntities.ToList().Shuffle();

            // Generate a new half season
            var secondHalf = _strategy.GenerateFixtures<M, T>(fixtureEntities);

            // Reverse fixtures where required e.g. AvB changes to BvA
            var sorted = _reverser.Reverse(firstHalf, secondHalf);

            return firstHalf.Concat(sorted).ToList();
        }
    }
}
