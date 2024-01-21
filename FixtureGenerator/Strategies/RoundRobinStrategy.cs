namespace FixtureGenerator.Strategies
{
    public class RoundRobinStrategy : IFixtureStrategy
    {
        public List<List<M>> GenerateFixtures<M, T>(IEnumerable<T> fixtureEntities)
            where M : IFixture, new()
            where T : IFixtureEntity
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
                T secondTeam = roundRobin.ElementAt((round + numberOfTeamsInRound - 1) % numberOfTeamsInRound);

                // We will always handle the team at the centre of the round robin first.
                // Therefore, we will alternative between home and away.
                M firstMatch;
                if (round % 2 == 0)
                {
                    firstMatch = CreateMatch<M, T>(firstTeam, secondTeam);
                }
                else
                {
                    firstMatch = CreateMatch<M, T>(secondTeam, firstTeam);
                }

                matchesInRound.Add(firstMatch);

                for (int i = 1; i < numberOfMatchesPerRound; i++)
                {
                    int homeIdx = (i + round - 1) % numberOfTeamsInRound;
                    int awayIdx = (round + numberOfTeamsInRound - i - 1) % numberOfTeamsInRound;

                    M roundMatch;
                    if (i % 2 == 0)
                    {
                        roundMatch = CreateMatch<M, T>(roundRobin.ElementAt(homeIdx), roundRobin.ElementAt(awayIdx));
                    }
                    else
                    {
                        roundMatch = CreateMatch<M, T>(roundRobin.ElementAt(awayIdx), roundRobin.ElementAt(homeIdx));
                    }

                    matchesInRound.Add(roundMatch);
                }
                fixtureList.Add(matchesInRound);
            }

            return fixtureList;
        }

        private static M CreateMatch<M, T>(T homeTeam, T awayTeam) where M : IFixture, new() where T : IFixtureEntity
        {
            M match = new M();
            match.HomeEntity = homeTeam;
            match.AwayEntity = awayTeam;

            return match;
        }
    }
}
