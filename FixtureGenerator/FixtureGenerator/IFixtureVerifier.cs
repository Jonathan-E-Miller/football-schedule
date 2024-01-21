namespace FixtureGenerator
{
    public interface IFixtureVerifier
    {
        public void VerifySecondHalf<M>(List<List<M>> firstHalf, List<List<M>> secondHalf) where M : IFixture;
    }
}
