using FixtureGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixtureGeneratorTests.Framework
{
    public class Match : IFixture
    {
        public IFixtureEntity? HomeEntity { get; set; }
        public IFixtureEntity? AwayEntity { get; set; }

        public Match()
        {
        }

        public Match(IFixtureEntity homeEntity, IFixtureEntity awayEntity)
        {
            HomeEntity = homeEntity;
            AwayEntity = awayEntity;
        }
    }
}
