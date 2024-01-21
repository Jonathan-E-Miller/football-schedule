using FixtureGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox
{
    public class Fixture : IFixture
    {
        public string Code => $"HomeEntity.Code + AwayEntity.Code";

        public IFixtureEntity? HomeEntity { get; set; }
        public IFixtureEntity? AwayEntity { get; set; }

        public override string ToString()
        {
            return $"{HomeEntity?.ToString()} v {AwayEntity?.ToString()}";
        }
    }
}
