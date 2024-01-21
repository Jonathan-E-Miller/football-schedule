using AutoFixture;
using AutoFixture.Kernel;
using FixtureGenerator;
using Microsoft.VisualStudio.TestPlatform.CrossPlatEngine.TestRunAttachmentsProcessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixtureGeneratorTests
{
    public static class TestData
    {
        public static List<List<Framework.Match>> GenerateRounds()
        {
            var toReturn = new List<List<Framework.Match>>();
            var fixture = new AutoFixture.Fixture();

            fixture.Customizations.Add(new TypeRelay
                (typeof(IFixtureEntity), typeof(Framework.Team)));

            var rounds = fixture.Create<List<List<Framework.Match>>>();

            return rounds;
        }
    }
}
