namespace FixtureGenerator
{
    public class FixtureGenerator
    {
        public enum EAlgorithmOption
        {
            eRoundRobin = 0,
            eBackTrack = 1
        };

        public FixtureGenerator()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fixtureEntities"></param>
        /// <param name="option"></param>
        /// <param name="constraint"></param>
        /// <returns></returns>
        public IEnumerable<IEnumerable<IFixture>> GenerateFixtureList(
            IEnumerable<IFixtureEntity> fixtureEntities, 
            EAlgorithmOption option = EAlgorithmOption.eRoundRobin, 
            List<Func<bool>>? constraint = null)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Calculate all rounds of fixtures using the round robin algorithm
        /// </summary>
        /// <typeparam name="M">The "match" object that implements IFixture interface</typeparam>
        /// <typeparam name="T">The "team" object that implements IFixtureEntity interface</typeparam>
        /// <param name="fixtureEntities">A list of teams or players to organise into a set of rounds</param>
        /// <returns>A list of lists, the inner list represents a round of matches.</returns>
        public List<List<M>> RoundRobin<M,T>(IEnumerable<T> fixtureEntities) where M : IFixture, new() where T : IFixtureEntity
        {
            List<List<M>> fixtureList = new List<List<M>>();

            int numberOfRounds = fixtureEntities.Count() - 1;
            int numberOfMatchesPerRound = fixtureEntities.Count() / 2;

            List<T> roundRobin = new List<T>();
            roundRobin.AddRange(fixtureEntities.Skip(1));

            int numberOfTeamsInRound = roundRobin.Count();
            for (int round = 0; round < numberOfRounds; round++)
            {
                List<M> matchesInRound = new List<M>();
                T firstTeam = fixtureEntities.ElementAt(0);
                T awayTeam = roundRobin.ElementAt((round + numberOfTeamsInRound - 1) % numberOfTeamsInRound);

                M match = new M();
                match.HomeEntity = firstTeam;
                match.AwayEntity = awayTeam;
                matchesInRound.Add(match);

                for (int i = 0; i < numberOfMatchesPerRound - 1; i++)
                {
                    M roundMatch = new M();
                    int homeIdx = (i + round) % numberOfTeamsInRound;
                    int awayIdx = (i + round + numberOfTeamsInRound - 2) % numberOfTeamsInRound;

                    roundMatch.HomeEntity = roundRobin.ElementAt(homeIdx);
                    roundMatch.AwayEntity = roundRobin.ElementAt(awayIdx);

                    matchesInRound.Add(roundMatch);
                }
                fixtureList.Add(matchesInRound);
            }

            return fixtureList;
        }

        private List<List<IFixture>> BackTrack(IEnumerable<IFixtureEntity> fixtureEntities, List<Func<bool>>? constraints = null)
        {
            throw new NotImplementedException();
        }
    }
}