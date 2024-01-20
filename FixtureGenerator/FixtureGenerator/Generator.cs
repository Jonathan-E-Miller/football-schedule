using FixtureGenerator.Strategies;

namespace FixtureGenerator
{
    public class Generator : IGenerator
    {
        private readonly IFixtureStrategy _strategy;
        private Dictionary<string, IFixture> _scheduledMatches = new Dictionary<string, IFixture>();

        public Generator(IFixtureStrategy strategy)
        {
            _strategy = strategy ?? throw new ArgumentNullException(nameof(strategy));
        }

        public List<List<M>> GenerateFixtures<M, T>(IEnumerable<T> fixtureEntities, Fixture.Options option = Fixture.Options.EHome)
            where M : IFixture, new()
            where T : IFixtureEntity
        {
            _scheduledMatches = new Dictionary<string, IFixture>();

            var firstHalf = _strategy.GenerateFixtures<M, T>(fixtureEntities);
            fixtureEntities.ToList().Shuffle();
            var secondHalf = _strategy.GenerateFixtures<M, T>(fixtureEntities);

            foreach (var matches in secondHalf)
            {
                firstHalf.Add(matches);
            }

            return firstHalf;
        }

        private static void Swap<M>(M match) where M : IFixture
        {
            (match.AwayEntity, match.HomeEntity) = (match.HomeEntity, match.AwayEntity);
            match.SetUniqueMatchCode();
        }
    }
}
