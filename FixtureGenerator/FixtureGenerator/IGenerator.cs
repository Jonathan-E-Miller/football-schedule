using static FixtureGenerator.Fixture;

namespace FixtureGenerator
{
    public interface IGenerator
    {
        List<List<M>> GenerateFixtures<M, T>(IEnumerable<T> fixtureEntities, Options option = Options.EHome) where M : IFixture, new() where T : IFixtureEntity;
    }
}
