using FixtureGeneratorTests.Framework;
using FixtureGenerator;

List<Team> _teams = new List<Team>()
{
    new Team("A"),
    new Team("B"),
    new Team("C"),
    new Team("D"),
    new Team("E"),
    new Team("F")
};

List<List<Match>> result = FixtureAlgorithm.GenerateFixtures<Match, Team>(_teams, FixtureAlgorithm.Options.EHomeAway);

int matchDay = 1;
foreach (List<Match> round in result)
{
    Console.WriteLine($"Round {matchDay++}");

    foreach(Match match in round)
    {
        Console.WriteLine(match.Code);
    }
    Console.Write(Environment.NewLine);
}
