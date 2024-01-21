namespace FixtureGenerator
{
    internal class FixtureVerifier : IFixtureVerifier
    {
        public void VerifySecondHalf<M>(List<List<M>> firstHalf, List<List<M>> secondHalf) where M : IFixture
        {
            secondHalf.ForEach(round =>
            {
                round.ForEach(fixture =>
                {
                    if (firstHalf.Any(round => round.Any(f => f.Code == fixture.Code)))
                    {
                        Swap(fixture);
                    }
                });
            });
        }

        private static void Swap<M>(M toswap) where M : IFixture
        {
            (toswap.HomeEntity, toswap.AwayEntity) = (toswap.AwayEntity, toswap.HomeEntity);
        }
    }
}
