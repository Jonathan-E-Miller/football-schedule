namespace FixtureGenerator
{
    public class Reverser : IReverser
    {
        public List<List<M>> Reverse<M>(List<List<M>> matches, List<List<M>> toReverse) where M : IFixture
        {
            var list = new List<List<M>>();

            toReverse.ForEach(round =>
            {
                var sortedRound = new List<M>();

                round.ForEach(fixture =>
                {
                    if (matches.Any(round => round.Any(f => f.Code == fixture.Code)))
                    {
                        (fixture.HomeEntity, fixture.AwayEntity) = (fixture.AwayEntity, fixture.HomeEntity);
                    }
                    sortedRound.Add(fixture);
                });
                list.Add(sortedRound);
            });

            return list;
        }
    }
}
