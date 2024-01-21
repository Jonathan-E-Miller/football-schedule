namespace FixtureGenerator
{
    public interface IGenerator
    {
        List<List<M>> GenerateFixtures<M, T>(IEnumerable<T> fixtureEntities) where M : IFixture, new() where T : IFixtureEntity;
    }
}
