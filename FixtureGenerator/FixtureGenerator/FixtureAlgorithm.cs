namespace FixtureGenerator
{
    public static class FixtureAlgorithm
    {
        public enum Options
        {
            EHome,
            EHomeAway
        };

        private static Dictionary<string, IFixture> _scheduledMatches;

        public static List<List<M>> GenerateFixtures<M, T>(IEnumerable<T> fixtureEntities, Options option = Options.EHome) where M : IFixture, new() where T : IFixtureEntity
        {
            _scheduledMatches = new Dictionary<string, IFixture>();

            if (option == Options.EHome)
            {
                return RoundRobin<M,T>(fixtureEntities);
            }

            List<List<M>> firstHalf = RoundRobin<M,T>(fixtureEntities);
            fixtureEntities.ToList().Shuffle();
            List<List<M>> secondHalf = RoundRobin<M, T>(fixtureEntities);

            foreach(List<M> matches in secondHalf)
            {
                firstHalf.Add(matches);
            }

            return firstHalf;
        }

        private static List<List<M>> RoundRobin<M, T>(IEnumerable<T> fixtureEntities) where M : IFixture, new() where T : IFixtureEntity
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

                string matchCode = match.HomeEntity.Code + match.AwayEntity.Code;

                if (_scheduledMatches.GetValueOrDefault(matchCode) != null)
                {
                    T spare = firstTeam;
                    firstTeam = awayTeam;
                    awayTeam = spare;

                    match.HomeEntity = firstTeam;
                    match.AwayEntity = awayTeam;
                    matchCode = match.HomeEntity.Code + match.AwayEntity.Code;
                }
                
                _scheduledMatches.Add(matchCode, match);

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
    }
}