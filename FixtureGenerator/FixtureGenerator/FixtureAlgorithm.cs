namespace FixtureGenerator
{
    public static class FixtureAlgorithm
    {
        public enum Options
        {
            EHome,
            EHomeAway
        };

        private static Dictionary<string, IFixture> _scheduledMatches = new Dictionary<string, IFixture>();

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

                M match = CreateMatch<M, T>(firstTeam, awayTeam);
                matchesInRound.Add(match);

                string matchCode = match.HomeEntity.Code + match.AwayEntity.Code;

                ScheduleMatchOrSwap<M>(match);

                for (int i = 1; i < numberOfMatchesPerRound; i++)
                {
                    
                    int homeIdx = (i + round - 1) % numberOfTeamsInRound;

                    int awayIdx = (round + numberOfTeamsInRound - i - 1) % numberOfTeamsInRound;
                    M roundMatch = CreateMatch<M, T>(roundRobin.ElementAt(homeIdx), roundRobin.ElementAt(awayIdx));
                    matchesInRound.Add(roundMatch);
                }
                fixtureList.Add(matchesInRound);
            }

            return fixtureList;
        }

        private static M CreateMatch<M,T>(T homeTeam, T awayTeam) where M : IFixture, new() where T : IFixtureEntity
        {
            M match = new M();
            match.HomeEntity = homeTeam;
            match.AwayEntity = awayTeam;
            match.SetUniqueMatchCode();

            return match;
        }

        private static void ScheduleMatchOrSwap<M>(M match) where M: IFixture
        {
            if (match != null && match.Code != null && _scheduledMatches.ContainsKey(match.Code))
            {
                Swap<M>(match);
            }

            _scheduledMatches.Add(match.Code, match);
        }

        private static void Swap<M>(M match) where M : IFixture
        {
            (match.AwayEntity, match.HomeEntity) = (match.HomeEntity, match.AwayEntity);
            match.SetUniqueMatchCode();
        }
    }
}