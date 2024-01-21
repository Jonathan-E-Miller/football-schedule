using AutoFixture;
using FixtureGenerator;
using FixtureGenerator.Strategies;
using FixtureGeneratorTests.Framework;
using FluentAssertions;
using Moq;

namespace FixtureGeneratorTests;

public class GeneratorTests
{
    private Generator _generator;

    private Mock<IFixtureStrategy> _mockStrategy;
    private Mock<IReverser> _mockReverser;

    private AutoFixture.Fixture _fixture = new AutoFixture.Fixture();
    private List<Team> _teams;

    [SetUp]
    public void Setup()
    {
        _mockStrategy= new Mock<IFixtureStrategy>();
        _mockReverser= new Mock<IReverser>();
        _generator = new Generator(_mockStrategy.Object, _mockReverser.Object);

        _teams = _fixture.CreateMany<Team>(6).ToList();
    }

    [Test]
    public void GenerateFixtures_WhenCalled_CallsStrategyTwice()
    {
        _mockStrategy.Setup(x => x.GenerateFixtures<Framework.Match, Team>(It.IsAny<IEnumerable<Team>>())).Returns(TestData.GenerateRounds());
        _mockReverser.Setup(x => x.Reverse(It.IsAny<List<List<Framework.Match>>>(), It.IsAny<List<List<Framework.Match>>>())).Returns(TestData.GenerateRounds());

        _generator.GenerateFixtures<Framework.Match, Team>(_teams);

        _mockStrategy.Verify(x => x.GenerateFixtures<Framework.Match, Team>(It.IsAny<IEnumerable<Team>>()), Times.Exactly(2));
    }

    [Test]
    public void GenerateFixtures_WhenCalled_CallsReverserOnceWithCorrectArguments()
    {
        var firstHalf = TestData.GenerateRounds();
        var secondHalf = TestData.GenerateRounds();

        _mockStrategy.Setup(x => x.GenerateFixtures<Framework.Match, Team>(It.IsAny<IEnumerable<Team>>())).Returns(firstHalf);
        _mockReverser.Setup(x => x.Reverse(It.IsAny<List<List<Framework.Match>>>(), It.IsAny<List<List<Framework.Match>>>())).Returns(secondHalf);
        
        _generator.GenerateFixtures<Framework.Match, Team>(_teams);

        _mockReverser.Verify(x => x.Reverse(firstHalf, firstHalf), Times.Once());
    }

    [Test]
    public void GenerateFixtures_WhenCalled_ReturnsCorrectResult()
    {
        var firstHalf = TestData.GenerateRounds();
        var secondHalf = TestData.GenerateRounds();

        _mockStrategy.Setup(x => x.GenerateFixtures<Framework.Match, Team>(It.IsAny<IEnumerable<Team>>())).Returns(firstHalf);
        _mockReverser.Setup(x => x.Reverse(It.IsAny<List<List<Framework.Match>>>(), It.IsAny<List<List<Framework.Match>>>())).Returns(secondHalf);

        var expected = firstHalf.Concat(secondHalf).ToList();

        var result = _generator.GenerateFixtures<Framework.Match, Team>(_teams);

        result.Should().BeEquivalentTo(expected);
    }
}