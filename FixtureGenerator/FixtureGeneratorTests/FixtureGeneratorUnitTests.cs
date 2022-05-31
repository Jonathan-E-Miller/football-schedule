using FixtureGenerator;
using FixtureGeneratorTests.Framework;

namespace FixtureGeneratorTests
{
    public class FxitureGeneratorTests
    {
        private List<Team> _teams;

        [SetUp]
        public void Setup()
        {
            _teams = new List<Team>()
            {
                new Team("A"),
                new Team("B"),
                new Team("C"),
                new Team("D"),
                new Team("E"),
                new Team("F")
            };
        }

        [Test]
        public void TestRoundRobin()
        {
            List<List<Match>> result = FixtureAlgorithm.GenerateFixtures<Match, Team>(_teams);

            // Three rounds of fixtures
            Assert.IsTrue(result.Count == 5);

            // Two rounds per match.
            Assert.IsTrue(result.All(r => r.Count == 3));

            TestForDuplicates(result);
        }

        [Test]
        public void TestHomeAndAway()
        {
            List<List<Match>> result = FixtureAlgorithm.GenerateFixtures<Match, Team>(_teams, FixtureAlgorithm.Options.EHomeAway);

            Assert.IsTrue(result.Count == 10);

            Assert.IsTrue(result.All(r => r.Count == 3));

            TestForDuplicates(result);
        }

        private void TestForDuplicates(List<List<Match>> result)
        {
            Dictionary<string, Boolean> matchScheduled = new Dictionary<string, bool>();
            // test that each team is not duplicated in round
            foreach (List<Match> round in result)
            {
                for (int i = 0; i < round.Count; i++)
                {
                    string code = (round[i].Code ?? throw new ArgumentException());
                    if (matchScheduled.ContainsKey(code))
                    {
                        Assert.Fail("Match already played");
                    }

                    matchScheduled.Add(code, true);

                    Team h = (Team)(round[i].HomeEntity ?? throw new ArgumentNullException());
                    Team a = (Team)(round[i].AwayEntity ?? throw new ArgumentNullException());

                    for (int j = 0; j < round.Count; j++)
                    {
                        if (i == j)
                            continue;

                        if (round[j].HomeEntity == h || round[j].AwayEntity == a || round[j].HomeEntity == a || round[j].AwayEntity == h)
                            Assert.Fail("Team already in round");
                    }
                }
            }
        }
    }
}