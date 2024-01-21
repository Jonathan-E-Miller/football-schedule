namespace FixtureGenerator
{
    public interface IReverser
    {
        List<List<M>> Reverse<M>(List<List<M>> matches, List<List<M>> duplicates) where M : IFixture;
    }
}