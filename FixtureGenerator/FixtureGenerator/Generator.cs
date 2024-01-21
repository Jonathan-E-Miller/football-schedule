using FixtureGenerator.Strategies;

namespace FixtureGenerator
{
    public class Generator : IGenerator
    {
        private readonly IFixtureStrategy _strategy;
        private readonly IReverser _reverser;

        private Dictionary<string, IFixture> _scheduledMatches = new Dictionary<string, IFixture>();

        /// <summary>
        /// Internal and accessed via unit testing so we can mock out
        /// dependencies. AssemblyInfo.cs granting permissions.
        /// </summary>
        /// <param name="strategy"></param>
        /// <param name="reverser"></param>
        /// <exception cref="ArgumentNullException"></exception>
        internal Generator(IFixtureStrategy strategy, IReverser reverser)
        {
            _strategy = strategy ?? throw new ArgumentNullException(nameof(strategy));
            _reverser = reverser ?? throw new ArgumentNullException(nameof(reverser));
        }

        /// <summary>
        /// Should be called by consuming code.
        /// </summary>
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
