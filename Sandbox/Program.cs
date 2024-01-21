// See https://aka.ms/new-console-template for more information
using Sandbox;
using System.Numerics;
using System;
using FixtureGenerator;

Console.WriteLine("Fixture Generator");

var teams = new List<Team>()
{
    new("Arsenal"),
    new("Aston Villa"),
    new("Bournemouth"),
    new("Brentford"),
    new("Brighton"),
    new("Burnley"),
    new("Chelsea"),
    new("Crystal Palace"),
    new("Everton"),
    new("Fulham"),
    new("Liverpool"),
    new("Luton Town"),
    new("Man. City"),
    new("Manchester Utd"),
    new("Newcastle"),
    new("Nottingham"),
    new("Sheffield Utd"),
    new("Tottenham"),
    new("West Ham"),
    new("Wolves")
};

teams.Shuffle();
var season = new Generator().GenerateFixtures<Fixture, Team>(teams);

var week = 1;
foreach (var round in season)
{
    Console.WriteLine($"Week {week++}");
    round.ForEach(x => Console.WriteLine(x.ToString()));
    Console.WriteLine(Environment.NewLine);
}    
